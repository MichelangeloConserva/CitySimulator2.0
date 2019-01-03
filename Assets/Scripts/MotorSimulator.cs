using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MotorSimulator : MonoBehaviour {

    public Transform centerOfMass;
    public float enginePower = 400f;
    public float turnPower = 10f;

    public Wheel[] wheel;

    Rigidbody rbody;

    void Awake () {
        rbody = GetComponent<Rigidbody>();
	}

    private void Start()
    {
        rbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate () {
        float torque = Input.GetAxis("Vertical") * enginePower;
        float turnSpeed = Input.GetAxis("Horizontal") * turnPower;

        if (torque != 0)
        {
            //front wheel drive
            wheel[0].Move(torque);
            wheel[1].Move(torque);
        }

        //front wheel steering
        wheel[0].Turn(turnSpeed);
        wheel[1].Turn(turnSpeed);
    }
}
