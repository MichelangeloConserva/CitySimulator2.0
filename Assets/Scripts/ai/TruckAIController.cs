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

        var lastWaypoint = waypoints[0];
        waypoints.Remove(waypoints[0]);

        // checking for destination
        if (waypoints.Count == 0)
        {
            if (destinations.Count == 0)
            {
                Destroy(this.gameObject,1);
                yield return null;
            }

            StartCoroutine(GarbageCollectionProcedure());

            waypoints = AStar.PathFromTo(Utils.NearestNode(transform.position), destinations[0]);
            destinations.Remove(destinations[0]);
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
