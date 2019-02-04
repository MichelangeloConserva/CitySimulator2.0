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

    public float[] sensors;




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
        }

        // Front Sensors
        SensorActivation(transform.forward);        
        // Back Sensors
        //var otherBack = SensorActivation(-transform.forward);

        if (rbSpeed > 30f & sensors[0] + sensors[1] + sensors[2] != 0)
        {
            force = -1000000000 * Mathf.Pow(rbSpeed + 1, 4); // emergency braking
        }
        else if (rbSpeed > 10f & sensors[0]+ sensors[1]+ sensors[2] != 0)
        {
            float dist = Mathf.Infinity;
            foreach (float f in sensors)
                if (f != 0)
                {
                    if (f < dist)
                        dist = f;
                }
            force = -(minDistanceForSlowingDown - (dist-1)) * Mathf.Pow(rbSpeed+1,4); // braking
        } else if (rbSpeed > 0f & sensors[0] + sensors[1] + sensors[2] != 0)
        {
            float dist = Mathf.Infinity;
            foreach (float f in sensors)
                if (f != 0)
                {
                    if(f<dist)
                        dist = f;
                }
            if (dist < 3)
                force = -Mathf.Pow(rbSpeed + 1, 4); // braking
            else
                force = 1;
            Debug.Log("last step force " + force + " " + dist);

        }


        // braking before arriving to a waypoint where I need to turn
        if ((IsAboutToTurn() && distance < minDistanceForSlowingDown))
        {
            if (rbSpeed > 20f)
            {
                force = -(minDistanceForSlowingDown - distance)*Mathf.Sqrt(rbSpeed); // braking
            }
        }

        // giving power to the car
        if  (rbSpeed > 50) // speed limit TODO : set automatic speed limits
            force = -Mathf.Sqrt(rbSpeed);

        turningForce = turning * motor.turnPower;
        frontForce = force * motor.enginePower;
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

        sensors = new float[3] { 0,0,0 }; 

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
                        sensors[2] = dist;
                    }
                    else if (v == coneMiddleLeft)
                    {
                        sensors[0] = dist;
                    }
                    else if (v == coneMiddle)
                    {
                        sensors[1] = dist;
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
