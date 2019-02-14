using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightSignaller : MonoBehaviour
{
    public int curLight;


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "vehicle")
        {
            if (curLight == 0 || curLight == 1)
            {
                col.gameObject.GetComponent<CarAIController>().StopAtTrafficLight(transform.parent.position + transform.parent.right * 3.5f, true);
            }
            else if (curLight == 2)
            {
                col.gameObject.GetComponent<CarAIController>().StopAtTrafficLight(Vector3.zero,false);
            }
        }
    }

}
