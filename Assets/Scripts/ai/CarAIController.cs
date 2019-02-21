using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIController : VehicleAIController
{

    // TODO : Check if we can add something



    public override IEnumerator Recalculating()
    {

        // checking for destination
        if (waypoints.Count <= 1)
            Destroy(gameObject);
        else
        {
            // Getting new references
            var oldWaypoint = nextWaypoint;
            waypoints.Remove(nextWaypoint);

            var nearFuturePath = AStar.PathFromTo(oldWaypoint, waypoints[1]);
            Debug.Log(nearFuturePath.Count);

            waypoints[0] = nearFuturePath[0];
            waypoints[1] = nearFuturePath[1];
            nextWaypoint = waypoints[0];

        }
        

        yield return new WaitForEndOfFrame();
    }



}
