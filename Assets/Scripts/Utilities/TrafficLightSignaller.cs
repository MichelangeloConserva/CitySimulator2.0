using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Settings;


public class TrafficLightSignaller : MonoBehaviour
{


    public TrafficLightLights curLight;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "vehicle")
        {
            if (curLight == TrafficLightLights.red || curLight == TrafficLightLights.yellow)
            {
                col.gameObject.GetComponent<CarAIController>().StopAtTrafficLight(transform.parent.position + transform.parent.right * 3.5f, true);
            }
            else if (curLight == TrafficLightLights.green)
            {
                col.gameObject.GetComponent<CarAIController>().StopAtTrafficLight(Vector3.zero,false);
            }
        }
    }

}
