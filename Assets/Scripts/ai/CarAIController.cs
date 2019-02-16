using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIController : VehicleAIController
{

    // TODO : Check if we can add something




    public override IEnumerator Recalculating()
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
