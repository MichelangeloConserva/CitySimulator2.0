using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CarAgent : MonoBehaviour {


    [Header("Settings")]
    [Space]
    public float minDistanceToCompleteCheck;
    public float minTurn;
    public float minDistanceForSlowingDown;
    public float speed;
    public float distVision;

    [Header("Path variables")]
    [Space]
    public List<Vector3> waypoints;
    public Vector3 lastWaypoint;
    public bool isArrived;
    public bool aboutToCrash;

    [Header("Debug variables")]
    [Space]
    public float turning;
    public float distance;
    public float force;
    public float turningForce;
    public float frontForce;
    public float rbSpeed;

    [Header("Links required")]
    [Space]
    public MotorSimulator motor;
    public GameObject debug;

    public float[] frontSensors;

    public NodeStreet endNode;

    public bool stopAtTrafficLight = false;

    bool passingCross = false;


    Vector3 stopPos;


    void Start ()
    {
        isArrived = false;
        aboutToCrash = false;




    }

    void FixedUpdate() // TODO : refactor
    {

        // checking speed km/h
        rbSpeed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;


        force = speed;
        turning = AngleToTurn();
        var carPos = transform.position - Vector3.up * transform.position.y;
        var wayPos = waypoints[0]       - Vector3.up * waypoints[0].y;
        distance = Vector3.Distance(carPos, wayPos);

        //DrawArrow.ForDebug(transform.position, waypoints[0] + Vector3.up*3 - transform.position, Color.blue);

        // Arriving to the waypoint and deleting it
        if (distance < minDistanceToCompleteCheck)
        {
            StartCoroutine(Recalculating());
        }

        // Front Sensors
        SensorActivation(transform.forward);        
        // Back Sensors
        //var otherBack = SensorActivation(-transform.forward);

        if ((rbSpeed > 30f & frontSensors[1] != 0) || (frontSensors[1] < 4 & frontSensors[1] > 0))
        {
            force = -1000000000000 * Mathf.Pow(rbSpeed + 1, 4); // emergency braking
        }
        else if (rbSpeed > 10f & frontSensors[1] != 0)
        {
            float dist = frontSensors[1];
            force = -Mathf.Pow(rbSpeed + 1, 4);
            //force = -(minDistanceForSlowingDown - (dist-1)) * Mathf.Pow(rbSpeed+1,4); // braking
        } else if (rbSpeed > 0f & frontSensors[1] != 0)
        {
            float dist = frontSensors[1];
            if (dist < 8)
                force = -999; // braking
            else
                force = 1;
        }


        // Cheking for traffic lights
        if (stopAtTrafficLight)
        {
            if (Vector3.Distance(stopPos, transform.position) < 7)
                force = -Mathf.Pow(rbSpeed + 1, 3);
            else if (Vector3.Distance(stopPos, transform.position) < 2)
                GetComponent<Rigidbody>().velocity *= 0; // braking
        }


        // braking before arriving to a waypoint where I need to turn
        if ((IsAboutToTurn() && distance < minDistanceForSlowingDown))
        {
            if (rbSpeed > 20f)
            {
                force = -(minDistanceForSlowingDown - distance)*Mathf.Sqrt(rbSpeed); // braking
            }
        }

        if (rbSpeed > 30)
        {
            //smoothness of the slowdown is controlled by the 0.99f, 
            //0.5f is less smooth, 0.9999f is more smooth
            GetComponent<Rigidbody>().velocity *= 0.99f;
        }


        turningForce = turning * motor.turnPower;
        frontForce = force * motor.enginePower;
        motor.MotorControlling(frontForce, turningForce);

    }

    public void StopAtTrafficLight(Vector3 stopPos)
    {
        stopAtTrafficLight = true;
        this.stopPos = stopPos;
    }

    public void StopAtTrafficLight()
    {
        stopAtTrafficLight = false;
    }



    IEnumerator StoppingAtTrafficLight(GameObject trafficLight)
    {
        passingCross = true;
        stopAtTrafficLight = true;
        for (int i = 0; i < trafficLight.transform.childCount; i++)
        {
            if (trafficLight.transform.GetChild(i).gameObject.activeInHierarchy)
                if (trafficLight.transform.GetChild(i).gameObject.name == "Rosso")
                {
                    yield return new WaitForSeconds(trafficLight.GetComponentInParent<TrafficLightManagement>().timeForLightChange);
                    stopAtTrafficLight = false;
                }
                else if (trafficLight.transform.GetChild(i).gameObject.name == "Giallo")
                {
                    yield return new WaitForSeconds(trafficLight.GetComponentInParent<TrafficLightManagement>().timeForLightChange * 2);
                    stopAtTrafficLight = false;
                }
        }

        yield return new WaitForSeconds(20);
        passingCross = false;
    }


    IEnumerator Recalculating()
    {
        lastWaypoint = waypoints[0];
        waypoints.Remove(waypoints[0]);

        // checking for destination
        if (waypoints.Count == 0)
        {
            Destroy(gameObject);
        }
        yield return new WaitForEndOfFrame();
    }


    IEnumerator DestructionTimer()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Destroy(this.gameObject);
    }


    private void SensorActivation(Vector3 fromPos)
    {
        var coneMiddleLeft = Vector3.RotateTowards(fromPos, -transform.right, Mathf.PI / 6 / 2, 2 * Mathf.PI) * distVision/6;
        var coneMiddleRight = Vector3.RotateTowards(fromPos, transform.right, Mathf.PI / 6 / 2, 2 * Mathf.PI) * distVision/6;
        var coneMiddle = fromPos * distVision;
        var allCones = new Vector3[] { coneMiddleLeft, coneMiddle, coneMiddleRight };

        frontSensors = new float[3] { 0,0,0 }; 

        RaycastHit[] hitsManual = new RaycastHit[5];
        foreach (Vector3 v in allCones)
            if (Physics.Raycast(transform.position + fromPos * 1.7f, v, out RaycastHit hit, distVision, LayerMask.GetMask("car")))
            {
                if (hit.collider.gameObject != gameObject)
                {
                    // getting the position for the new waypoint
                    var otherPos = hit.collider.gameObject.transform.position;
                    var dir = otherPos - transform.position;
                    var maxDist = Mathf.Max(hit.collider.gameObject.transform.localScale.z, hit.collider.gameObject.transform.localScale.x) / 2 + 2f;
                    Vector3 left = Vector3.Cross(dir, Vector3.up).normalized * maxDist;

                    var dist = Vector3.Distance(transform.position, hit.point);
                    // if the object is on the right i need to turn left
                    if (v == coneMiddleRight)
                    {
                        frontSensors[1] += dist;
                    }
                    else if (v == coneMiddleLeft)
                    {
                        frontSensors[1] += dist;
                    }
                    else if (v == coneMiddle)
                    {
                        frontSensors[1] += dist;
                    }
                }
                else if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Colliding with myself");
                }
            }

        foreach (Vector3 v in allCones)
            DrawArrow.ForDebug(transform.position + fromPos, v - fromPos, Color.green);
    }



    /// <summary>
    /// Positive value: turn right, Negative value: turn left
    /// </summary>
    /// <returns></returns>
    private float AngleToTurn()
    {
        var heading = waypoints[0] - transform.position;
        var cross = Vector3.Cross(transform.forward , heading.normalized );
        return cross.y;
    }

    private bool IsAboutToTurn()
    {
        if (waypoints.Count <= 1)
            return false;

        var heading = waypoints[1] - transform.position;
        var cross = Vector3.Cross(transform.forward, heading.normalized).y;

        if (Mathf.Abs(cross) < minTurn)
            return false;

        return true;
    }


}
