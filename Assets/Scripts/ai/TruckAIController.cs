using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAIController : VehicleAIController
{
    [Header("Settings")]
    [Space]
    public float garbageCollectionTime;

    
    public List<NodeStreet> destinations;
    

    public override IEnumerator Recalculating()
    {
        if (waypoints.Count == 0)
            waypoints = AStar.PathFromTo(Physics.OverlapSphere(Utils.Down(transform.position), 5, LayerMask.GetMask("network"))[0].gameObject.GetComponent<NodeHandler>().node,
                                arrivalNode, gameObject);

        // checking for destination
        if (waypoints[0] == arrivalNode)
        {
            if (destinations.Count == 0)
            {
                Destroy(this.gameObject, 1);
                yield return null;
            }
            else
            {
                // Recalculate path
                waypoints = AStar.PathFromTo(destinations[0], destinations[1], gameObject);

                destinations.RemoveAt(0);
                arrivalNode = destinations[0];
                StartCoroutine(GarbageCollectionProcedure());
            }
        }
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


    private IEnumerator GarbageCollectionProcedure()
    {
        stopped = true;

        yield return new WaitForSeconds(garbageCollectionTime);

        // TODO : implement animation activation now

        stopped = false;
    }




}
