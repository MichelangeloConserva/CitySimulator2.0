﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAgent : MonoBehaviour {

    public MotorSimulator motor;

    public List<Vector3> waypoints;

    public Vector3 lastWaypoint;

    public float minDistance;
    public float minTurn;
    public float minDistanceForSlowingDown;
    public float speed;
    public float distVision;
    public bool isArrived;

    public bool aboutToCrash;

    public GameObject debug;


    void Start () {

        // **** testing start ****

        var wayP = GameObject.FindGameObjectWithTag("waypoints");
        var n = wayP.transform.childCount;

        waypoints = new List<Vector3>();
        for (int i=0; i<n; i++)
            waypoints.Add(wayP.transform.GetChild(i).transform.position - Vector3.up * 3f);

        transform.position = waypoints[0]+Vector3.up*2;
        waypoints.Remove(waypoints[0]);
        isArrived = false;

        aboutToCrash = false;


        // **** testing end ****
    }

    void FixedUpdate()
    {
        var turning = AngleToTurn();
        var car = transform.position;
        var wayPos = waypoints[0];
        wayPos.y = 0;
        car.y = 0;
        var distance = Vector3.Distance(car, wayPos);
        float force;

        // Arriving to the waypoint and deleting it
        if (distance < minDistance)
        {
            lastWaypoint = waypoints[0];
            waypoints.Remove(waypoints[0]);
            Debug.DrawLine(transform.position, waypoints[0], Color.red, Mathf.Infinity);
            aboutToCrash = false;
        }

        
       
        // checking condition for slowing down
        bool shouldSlow = (IsAboutToTurn() && distance < minDistanceForSlowingDown) ||  // getting near to a turning point
                          (Mathf.Abs(turning) > 0.2f);                                  // completely wrong direction

        if (shouldSlow)
            force = speed / 3;
        else
            force = speed;

        // Raycast hit manual way
        var coneLeft = Vector3.RotateTowards(transform.forward, -transform.right, Mathf.PI / 6, 2 * Mathf.PI) * distVision;
        var coneMiddleLeft = Vector3.RotateTowards(transform.forward, -transform.right, Mathf.PI / 6 / 2, 2 * Mathf.PI) * distVision;
        var coneRight = Vector3.RotateTowards(transform.forward, transform.right, Mathf.PI / 6, 2 * Mathf.PI) * distVision;
        var coneMiddleRight = Vector3.RotateTowards(transform.forward, transform.right, Mathf.PI / 6 / 2, 2 * Mathf.PI) * distVision;
        var coneMiddle = transform.forward * distVision;
        var allCones = new Vector3[] { coneLeft, coneMiddleLeft, coneMiddle, coneMiddleRight, coneRight };

        RaycastHit[] hitsManual = new RaycastHit[5];
        RaycastHit hit;
        foreach (Vector3 v in allCones)
            if (Physics.Raycast(transform.position + transform.forward * 1.7f, v, out hit, distVision))
            {
                if (hit.collider.gameObject.tag == gameObject.tag && !aboutToCrash)
                {
                    Debug.DrawLine(transform.position, hit.collider.gameObject.transform.position, Color.blue);

                    // getting the position for the new waypoint
                    var otherPos = hit.collider.gameObject.transform.position;
                    var dir = otherPos - transform.position;
                    var maxDist = Mathf.Max(hit.collider.gameObject.transform.localScale.z, hit.collider.gameObject.transform.localScale.x) / 2 + 2f;
                    Vector3 left = Vector3.Cross(dir, Vector3.up).normalized * maxDist;

                    // if the object is on the right i need to turn left
                    if (v == coneRight || v == coneMiddleRight)
                    {
                        Instantiate(debug, -left + hit.collider.gameObject.transform.position + Vector3.up, Quaternion.identity);
                        waypoints.Insert(0,-left + hit.collider.gameObject.transform.position);
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



        // giving power to the car
        //return;
        if (turning > minTurn)
            motor.MotorControlling(force * motor.enginePower, turning * motor.turnPower);
        else if (turning < -minTurn)
            motor.MotorControlling(force * motor.enginePower, turning * motor.turnPower);



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
