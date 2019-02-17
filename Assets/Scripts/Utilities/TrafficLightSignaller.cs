using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;


public class TrafficLightSignaller : MonoBehaviour
{

    public TrafficLightLights curLight;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "vehicle")
        {
            if (curLight == TrafficLightLights.red || curLight == TrafficLightLights.yellow)
            {
                var stopPos = transform.parent.position + transform.parent.right * 2f - transform.parent.up ;
                col.gameObject.GetComponent<VehicleAIController>().StopAtTrafficLight(stopPos, true);
            }
            else if (curLight == TrafficLightLights.green)
            {
                col.gameObject.GetComponent<VehicleAIController>().StopAtTrafficLight(Vector3.zero,false);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "vehicle")
            col.gameObject.GetComponent<VehicleAIController>().StopAtTrafficLight(Vector3.zero, false);
    }



}
