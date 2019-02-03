using System.Collections;
using System.Collections.Generic;
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



    void Start () {

        isArrived = false;

        aboutToCrash = false;
    }

    void FixedUpdate()
    {
        // checking speed km/h
        rbSpeed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;


        force = speed;
        turning = AngleToTurn();
        var carPos = transform.position - Vector3.up * transform.position.y;
        var wayPos = waypoints[0]       - Vector3.up * waypoints[0].y;
        distance = Vector3.Distance(carPos, wayPos);


        // Arriving to the waypoint and deleting it
        if (distance < minDistanceToCompleteCheck)
        {
            lastWaypoint = waypoints[0];
            waypoints.Remove(waypoints[0]);

            // checking for destination
            if (waypoints.Count == 0)
            {
                isArrived = true;
                Destroy(this.gameObject);
            }
            aboutToCrash = false;
        }



        // Front Sensors
        SensorActivation(transform.forward);        
        // Back Sensors
        SensorActivation(-transform.forward);





        // braking before arriving to a waypoint where I need to turn
        if ((IsAboutToTurn() && distance < minDistanceForSlowingDown))
        {
            if (rbSpeed > 20f)
            {
                force = -(minDistanceForSlowingDown - distance)*Mathf.Sqrt(rbSpeed); // braking
            }
        }
        else
        {
            
        }


        // giving power to the car
        turningForce = turning * motor.turnPower;
        frontForce = force * motor.enginePower ;
        if (turning > minTurn)
            motor.MotorControlling(frontForce, turningForce);
        else if (turning < -minTurn)
            motor.MotorControlling(frontForce, turningForce);
        else
            motor.MotorControlling(2 * frontForce, 0);


    }



    private void SensorActivation(Vector3 fromPos)
    {
        var coneMiddleLeft = Vector3.RotateTowards(fromPos, -transform.right, Mathf.PI / 6 / 2, 2 * Mathf.PI) * distVision;
        var coneMiddleRight = Vector3.RotateTowards(fromPos, transform.right, Mathf.PI / 6 / 2, 2 * Mathf.PI) * distVision;
        var coneMiddle = fromPos * distVision;
        var allCones = new Vector3[] { coneMiddleLeft, coneMiddle, coneMiddleRight };

        RaycastHit[] hitsManual = new RaycastHit[5];
        foreach (Vector3 v in allCones)
            if (Physics.Raycast(transform.position + transform.forward * 1.7f, v, out RaycastHit hit, distVision, LayerMask.GetMask("car")))
            {
                if (!aboutToCrash && hit.collider.gameObject != gameObject)
                {
                    // getting the position for the new waypoint
                    var otherPos = hit.collider.gameObject.transform.position;
                    var dir = otherPos - transform.position;
                    var maxDist = Mathf.Max(hit.collider.gameObject.transform.localScale.z, hit.collider.gameObject.transform.localScale.x) / 2 + 2f;
                    Vector3 left = Vector3.Cross(dir, Vector3.up).normalized * maxDist;

                    // if the object is on the right i need to turn left
                    if (v == coneMiddleRight)
                    {
                        //Time.timeScale = 0;
                    }
                    else if (v == coneMiddleLeft)
                    {
                        //Time.timeScale = 0;
                    }
                    else if (v == coneMiddle)
                    {
                        //Time.timeScale = 0;
                    }
                    aboutToCrash = true;
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
