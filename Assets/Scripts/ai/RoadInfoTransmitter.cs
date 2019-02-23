using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadInfoTransmitter : MonoBehaviour
{
    public Transform markerPos;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "vehicle")
        {
            // Setting speed limit
            col.gameObject.GetComponent<VehicleAIController>().maxSpeed = Settings.speedLimit;

            // Setting the lane direction
            col.gameObject.GetComponent<VehicleAIController>().roadLaneDir = transform.forward ;
            //Utils.DrawDebugArrow(transform.position + Vector3.up*5, transform.forward, Color.red, Mathf.Infinity);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "vehicle")
        {
            // Setting the lane direction
            col.gameObject.GetComponent<VehicleAIController>().roadLaneDir = Vector3.zero;
        }
    }


}
