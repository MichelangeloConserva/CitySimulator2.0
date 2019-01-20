using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MotorSimulator : MonoBehaviour {

    [Header("Settings")]
    public float enginePower = 400f;
    public float turnPower = 10f;
    public bool humanControll;
    public float maxSpeed;

    public Wheel[] wheel;
    public Transform centerOfMass;
    public Rigidbody rbody;

    void Awake () {
        rbody = GetComponent<Rigidbody>();
        humanControll = false;
	}

    private void Start()
    {
        rbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate () {
        if (humanControll)
        {
            float torque = Input.GetAxis("Vertical") * enginePower;
            float turnSpeed = Input.GetAxis("Horizontal") * turnPower;
            MotorControlling(torque, turnSpeed);
        }

    }

    public void MotorControlling(float torque, float turnSpeed)
    {
        if (GetComponent<Rigidbody>().velocity.sqrMagnitude > maxSpeed)
        {
            torque = -0.1f;
        }

        if (torque != 0)
        {
            Debug.Log(GetComponent<Rigidbody>().velocity.sqrMagnitude);
            //front wheel drive
            wheel[0].Move(torque);
            wheel[1].Move(torque);
        }

        //front wheel steering
        wheel[0].Turn(turnSpeed);
        wheel[1].Turn(turnSpeed);
    }


}
