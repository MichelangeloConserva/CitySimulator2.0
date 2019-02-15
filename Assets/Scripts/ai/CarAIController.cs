using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIController : MonoBehaviour
{
    [Header("Path variables")]
    [Space]
    public List<Vector3> waypoints;
    

    [Header("Debug variables")]
    [Space]
    public float rbSpeed;
    public bool stopAtTrafficLight;

    [Header("Settings")]
    [Space]
    public float turningForce;
    public float frontForce;
    public float minTurn;
    public float minDistanceToCompleteCheck;
    public float raySensorLength;
    public float securityDistance;

    public float maxSpeed;

    public Dictionary<GameObject, bool> trafficLightInfo;

    private Vector3 stopPosForTrafficLight;
    private MotorSimulator motor;
    private List<GameObject> nearbyCars;

    private bool aboutToTurn;


    void Start()
    {
        aboutToTurn = false;
        stopAtTrafficLight = false;
        nearbyCars = new List<GameObject>();
        maxSpeed = Mathf.Infinity;

        motor = GetComponent<MotorSimulator>();
    }

    void FixedUpdate()
    {
        bool goNoProblem = true;
        bool otherCarInFront = false;
        bool otherCarNearby= false;
        var sensorLength = raySensorLength;

        if (waypoints.Count == 0)
        {
            return;
        }

        // Checking arrival at waypoint
        var carPos = transform.position - Vector3.up * transform.position.y;
        var wayPos = waypoints[0] - Vector3.up * waypoints[0].y;
        var distance = Vector3.Distance(carPos, wayPos);
        if (distance < minDistanceToCompleteCheck)
            StartCoroutine(Recalculating());

        // Checking speed km/h
        rbSpeed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;

        // Checking how much to turn
        var turning = AngleToTurn();

        // Checking for cars in front 
        var frontDirection = motor.wheel[0].transform.forward;
        var frontPos = transform.position + transform.forward;


        // we have a situation to handle
        aboutToTurn = IsAboutToTurn();
        if (stopAtTrafficLight || aboutToTurn)
            goNoProblem = false;


        if (stopAtTrafficLight)
            if (Vector3.Distance(transform.position, stopPosForTrafficLight) < securityDistance + 2)
                sensorLength /= 3;



        // Debug sensors
        DrawArrow.ForDebug(frontPos, frontDirection * sensorLength, Color.blue);
        DrawArrow.ForDebug(frontPos , frontDirection  * sensorLength/4 + transform.right , Color.blue);
        DrawArrow.ForDebug(frontPos , frontDirection  * sensorLength/4 - transform.right , Color.blue);


        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(frontPos,
                           frontDirection,
                           out hit,
                           sensorLength,
                           LayerMask.GetMask("car")))
        {
            otherCarInFront = true;
            goNoProblem = false;
        }
        // checking cars at right
        else if (Physics.Raycast(frontPos,
                                 frontDirection * sensorLength / 4 + transform.right,
                                 out hit,
                                 sensorLength/4,
                                 LayerMask.GetMask("car")))
        {
            otherCarNearby = true;
            goNoProblem = false;
            turning -= 0.1f;
        }
        // checking cars at left
        else if (Physics.Raycast(frontPos,
                                 frontDirection * sensorLength / 4 - transform.right,
                                 out hit,
                                 sensorLength / 4,
                                 LayerMask.GetMask("car")))
        {
            otherCarNearby = true;
            goNoProblem = false;
            turning += 0.1f;
        }







        // Moving forward
        if (goNoProblem)
        {
            // Boosting acceleration if speed is low
            if (rbSpeed < 20)
            {
                frontForce *= 1.1f;
            }

            turningForce = turning * motor.turnPower;
            var force = frontForce * motor.enginePower;
            motor.Move(force, turningForce);
        }
        else
        {
            // Other car in front
            if (otherCarInFront)
            {
                var dist = Vector3.Distance(transform.position, hit.point);
                StoppingProcedure(dist, turning);
            }

            // Red traffic light
            else if (stopAtTrafficLight)
            {
                var dist = Vector3.Distance(transform.position, stopPosForTrafficLight);
                StoppingProcedure(dist-1, turning);
            }

            // Slowing since I'm about to turn
            else if (aboutToTurn)
            {
                var dist = Vector3.Distance(transform.position, wayPos);
                SlowingProcedure(dist, turning);
            }

            // Slowing and little turning to avoid collision
            else if (otherCarNearby)
            {
                var dist = Vector3.Distance(transform.position, hit.point);
                SlowingProcedure(dist, turning);
            }
        }

        // Checking for speed limit
        if (rbSpeed > maxSpeed)
            GetComponent<Rigidbody>().velocity *= 0.99f;


    }

    private void SlowingProcedure(float dist, float turning)
    {
        if (dist > securityDistance && rbSpeed > 20)
            motor.Brake(dist * 100000 * Mathf.Pow(rbSpeed + 1, 10) + 3);
        else 
        {
            turningForce = turning * motor.turnPower;
            var force = 1 * motor.enginePower;
            motor.Move(force, turningForce);
        }
    }


    /// <summary>
    /// A calibrated stopping procedure made to look realistic
    /// </summary>
    /// <param name="dist"></param>
    /// <param name="turning"></param>
    private void StoppingProcedure(float dist, float turning)
    {
        if (dist > securityDistance && rbSpeed > 20)
            motor.Brake(dist * 100000000 * Mathf.Pow(rbSpeed + 1, 6) + 3);
        else if (dist > securityDistance)
        {
            turningForce = turning * motor.turnPower;
            var force = 1 * motor.enginePower;
            motor.Move(force, turningForce);
        }
        else
        {
            motor.Brake(dist * 100000000 * Mathf.Pow(rbSpeed + 1, 6) + 3);
        }
    }


    /// <summary>
    /// Called by the traffic light zone to inform the car if it has to stop
    /// </summary>
    /// <param name="stopPos"></param>
    public void StopAtTrafficLight(Vector3 stopPos, bool stop)
    {
        if (stop)
        {
            stopAtTrafficLight = true;
            stopPosForTrafficLight = stopPos;
        }
        else
        {
            stopAtTrafficLight = false;
        }
    }

    /// <summary>
    /// Positive value: turn right, Negative value: turn left
    /// </summary>
    /// <returns></returns>
    private float AngleToTurn()
    {
        if(waypoints.Count == 0) { return 0; }
        var heading = waypoints[0] - transform.position;
        var cross = Vector3.Cross(transform.forward, heading.normalized);
        return cross.y;
    }

    /// <summary>
    /// Check if the car is about to turn so that I can slow down
    /// </summary>
    /// <returns></returns>
    private bool IsAboutToTurn()
    {
        if (waypoints.Count <= 1)
            return false;

        var heading = waypoints[1] - transform.position;
        var cross = Vector3.Cross(transform.forward, heading.normalized).y;

        if (Mathf.Abs(cross) < minTurn)
            return false;

        return true;
    }

    /// <summary>
    /// Recalculating the next target after arrival at one
    /// if the path is completed then autodestruction is performed
    /// </summary>
    /// <returns></returns>
    IEnumerator Recalculating()
    {
        var lastWaypoint = waypoints[0];
        waypoints.Remove(waypoints[0]);

        // checking for destination
        if (waypoints.Count == 0)
        {
            Destroy(gameObject);
        }
        yield return new WaitForEndOfFrame();
    }

}
