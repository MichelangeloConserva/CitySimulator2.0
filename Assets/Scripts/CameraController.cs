using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float ScreenEdgeBorderThickness = 5.0f; // distance from screen edge. Used for mouse movement

    [Header("Camera Mode")]
    [Space]
    public bool RTSMode = true;
    public bool FlyCameraMode = false;

    [Header("Movement Speeds")]
    [Space]
    public float minPanSpeed;
    public float maxPanSpeed;
    public float secToMaxSpeed; //seconds taken to reach max speed;
    public float zoomSpeed;

    [Header("Movement Limits")]
    [Space]
    public bool enableMovementLimits;
    public Vector2 heightLimit;
    public Vector2 lenghtLimit;
    public Vector2 widthLimit;

    private float panSpeed;
    private Vector3 initialPos;
    private Vector3 panMovement;
    private Vector3 pos;
    private Quaternion rot;
    private Vector3 lastMousePosition;
    private Quaternion initialRot;
    private float panIncrease = 0.0f;

    [Header("Rotation")]
    [Space]
    public bool rotationEnabled;
    public float rotateSpeed;



    // Use this for initialization
    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
    }


    void Update()
    {

        //check that ony one mode is choosen
        if (RTSMode == true) FlyCameraMode = false;
        if (FlyCameraMode == true) RTSMode = false;


        panMovement = Vector3.zero;

        if (Input.GetKey(KeyCode.W) )
            panMovement += Vector3.forward * panSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            panMovement -= Vector3.forward * panSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            panMovement += Vector3.left * panSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            panMovement += Vector3.right * panSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q))
            panMovement += Vector3.up * panSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
            panMovement += Vector3.down * panSpeed * Time.deltaTime;

        if (RTSMode) transform.Translate(panMovement, Space.World);
        else if (FlyCameraMode) transform.Translate(panMovement, Space.Self);


        //increase pan speed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q))
        {
            panIncrease += Time.deltaTime / secToMaxSpeed;
            panSpeed = Mathf.Lerp(minPanSpeed, maxPanSpeed, panIncrease);
        }
        else
        {
            panIncrease = 0;
            panSpeed = minPanSpeed;
        }

        if (rotationEnabled)
        {
            // Mouse Rotation
            if (Input.GetMouseButton(0))
            {
                Vector3 mouseDelta;
                if (lastMousePosition.x >= 0 &&
                    lastMousePosition.y >= 0)
                    mouseDelta = Input.mousePosition - lastMousePosition;
                else
                {
                    mouseDelta = Vector3.zero;
                }
                var rotation = Vector3.up * Time.deltaTime * rotateSpeed * mouseDelta.x;
                rotation += Vector3.left * Time.deltaTime * rotateSpeed * mouseDelta.y;

                transform.Rotate(rotation, Space.World);

                // Make sure z rotation stays locked
                rotation = transform.rotation.eulerAngles;
                rotation.z = 0;
                transform.rotation = Quaternion.Euler(rotation);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (RTSMode) transform.rotation = Quaternion.Slerp(transform.rotation, initialRot, 0.5f * Time.time);
            }

            lastMousePosition = Input.mousePosition;
        }

        if (enableMovementLimits == true)
        {
            //movement limits
            pos = transform.position;
            pos.y = Mathf.Clamp(pos.y, heightLimit.x, heightLimit.y);
            pos.z = Mathf.Clamp(pos.z, lenghtLimit.x, lenghtLimit.y);
            pos.x = Mathf.Clamp(pos.x, widthLimit.x, widthLimit.y);
            transform.position = pos;
        }

    }
}
