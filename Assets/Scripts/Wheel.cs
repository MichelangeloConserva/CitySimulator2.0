using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour {
    const string TIRE_NAME = "tire";

    [SerializeField]Transform tire;
    [SerializeField]WheelCollider wc;

    // Use this for initialization
    private void Awake()
    {
        wc = GetComponent<WheelCollider>();

        if (tire == null)
            tire = transform.Find(TIRE_NAME);
    }


	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(float torque)
    {
        wc.motorTorque = torque;
    }

    public void Turn(float turnSpeed)
    {
        wc.steerAngle = turnSpeed;
        tire.localEulerAngles = new Vector3(0f, wc.steerAngle, 90f);
    }
}
