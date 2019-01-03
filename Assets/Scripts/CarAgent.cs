using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAgent : MonoBehaviour {

    public MotorSimulator motor;

    public List<Vector3> waypoints;

    public bool isArrived;

	void Start () {

        // **** testing start ****


        var wayP = GameObject.FindGameObjectWithTag("waypoints");
        var n = wayP.transform.childCount;

        waypoints = new List<Vector3>();
        for (int i=0; i<n; i++)
            waypoints.Add(wayP.transform.GetChild(i).transform.position - Vector3.up * 3f);

        transform.position = waypoints[0];
        waypoints.Remove(waypoints[0]);
        isArrived = false;


        // **** testing end ****
    }

    void Update () {

        // **** testing start ****
        Debug.Log(angleToTurn());





        // **** testing end ****

    }



    private float angleToTurn()
    {
        var heading = waypoints[0] - transform.position;
        var cross = Vector3.Cross(transform.forward , heading.normalized );
        return cross.y;
    }









}
