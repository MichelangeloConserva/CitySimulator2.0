using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "vehicle")
            col.gameObject.GetComponent<VehicleAIController>().maxSpeed = Settings.speedLimit;
    }
}
