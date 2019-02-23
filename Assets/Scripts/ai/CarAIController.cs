using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIController : VehicleAIController
{

    // TODO : Check if we can add something



    public override IEnumerator Recalculating()
    {

        // checking for destination
        if (waypoints[0] == arrivalNode)
            Destroy(gameObject);
        else
        {
            if (waypoints.Count <= 3)
            {
                var oldWaypoint = nextWaypoint;
                waypoints.RemoveAt(0);
                List<NodeStreet> nearFuturePath;
                if (waypoints.Count >= 2)
                    nearFuturePath = AStar.PathFromTo(oldWaypoint, waypoints[1], gameObject);
                else
                    nearFuturePath = AStar.PathFromTo(oldWaypoint, arrivalNode, gameObject);

                waypoints[0] = nearFuturePath[0];
                nextWaypoint = waypoints[0];
            }
            else
            {
                var oldWaypoint = nextWaypoint;
                waypoints.Remove(nextWaypoint);
                var nearFuturePath = AStar.PathFromTo(oldWaypoint, waypoints[2], gameObject);
                waypoints[0] = nearFuturePath[0];
                waypoints[1] = nearFuturePath[1];
                nextWaypoint = waypoints[0];
            }

        }
        

        yield return new WaitForEndOfFrame();
    }



}
