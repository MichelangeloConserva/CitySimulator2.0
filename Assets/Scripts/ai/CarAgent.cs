using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarAgent : MonoBehaviour {

    public MotorSimulator motor;

    public List<Vector3> waypoints;

    public Vector3 lastWaypoint;


    public float minDistanceToCompleteCheck;
    public float minTurn;
    public float minDistanceForSlowingDown;
    public float speed;
    public float distVision;

    public bool isArrived;
    public bool aboutToCrash;

    public GameObject debug;

    [Header("Debug variables")]
    public float turning;
    public float distance;
    public float force;
    public float turningForce;
    public float frontForce;
    public float rbSpeed;

    void Start () {

        // **** testing start **
        isArrived = false;

        aboutToCrash = false;
        // **** testing end ****
    }

    void FixedUpdate()
    {
        // checking speed km/h
        rbSpeed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;


        force = speed;
        turning = AngleToTurn();
        var car = transform.position;
        var wayPos = waypoints[0];
        wayPos.y = 0;
        car.y = 0;
        distance = Vector3.Distance(car, wayPos);


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



        // Raycast hit manual way
        var coneLeft = Vector3.RotateTowards(transform.forward, -transform.right, Mathf.PI / 6, 2 * Mathf.PI) * distVision;
        var coneMiddleLeft = Vector3.RotateTowards(transform.forward, -transform.right, Mathf.PI / 6 / 2, 2 * Mathf.PI) * distVision;
        var coneRight = Vector3.RotateTowards(transform.forward, transform.right, Mathf.PI / 6, 2 * Mathf.PI) * distVision;
        var coneMiddleRight = Vector3.RotateTowards(transform.forward, transform.right, Mathf.PI / 6 / 2, 2 * Mathf.PI) * distVision;
        var coneMiddle = transform.forward * distVision;
        var allCones = new Vector3[] { coneLeft, coneMiddleLeft, coneMiddle, coneMiddleRight, coneRight };

        RaycastHit[] hitsManual = new RaycastHit[5];
        foreach (Vector3 v in allCones)
            if (Physics.Raycast(transform.position + transform.forward * 1.7f, v, out RaycastHit hit, distVision))
            {
                if (hit.collider.gameObject.tag == gameObject.tag && !aboutToCrash)
                {

                    // getting the position for the new waypoint
                    var otherPos = hit.collider.gameObject.transform.position;
                    var dir = otherPos - transform.position;
                    var maxDist = Mathf.Max(hit.collider.gameObject.transform.localScale.z, hit.collider.gameObject.transform.localScale.x) / 2 + 2f;
                    Vector3 left = Vector3.Cross(dir, Vector3.up).normalized * maxDist;

                    // if the object is on the right i need to turn left
                    if (v == coneRight || v == coneMiddleRight)
                    {
                        Instantiate(debug, -left + hit.collider.gameObject.transform.position + Vector3.up, Quaternion.identity);
                        waypoints.Insert(0, -left + hit.collider.gameObject.transform.position);
                    }
                    else if (v == coneMiddleLeft || v == coneLeft)
                    {
                        Instantiate(debug, left + hit.collider.gameObject.transform.position + Vector3.up, Quaternion.identity);
                        waypoints.Insert(0, left + hit.collider.gameObject.transform.position);
                    }
                    else if (v == coneMiddle)
                    {
                        if (Vector3.Distance(left + hit.collider.gameObject.transform.position, waypoints[0]) >
                            Vector3.Distance(-left + hit.collider.gameObject.transform.position, waypoints[0]))
                        {
                            Instantiate(debug, -left + hit.collider.gameObject.transform.position + Vector3.up, Quaternion.identity);
                            waypoints.Insert(0, -left + hit.collider.gameObject.transform.position);
                        }
                        else
                        {
                            Instantiate(debug, left + hit.collider.gameObject.transform.position + Vector3.up, Quaternion.identity);
                            waypoints.Insert(0, left + hit.collider.gameObject.transform.position);
                        }
                    }
                    aboutToCrash = true;
                }
            }


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
