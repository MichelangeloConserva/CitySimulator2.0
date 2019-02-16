using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAIController : VehicleAIController
{

    public List<NodeStreet> destinations;


    public override IEnumerator Recalculating()
    {
        Debug.Log("Recalculating");

        var lastWaypoint = waypoints[0];
        waypoints.Remove(waypoints[0]);

        // checking for destination
        if (waypoints.Count == 0)
        {
            if (destinations.Count == 0)
                Destroy(this.gameObject);

            Debug.DrawLine(destinations[0].nodePosition, Vector3.up * 100, Color.red, Mathf.Infinity);

            RealcluatePath(NearestNode(), destinations[0]);
            destinations.Remove(destinations[0]);
        }

        yield return new WaitForEndOfFrame();
    }


    private void RealcluatePath(NodeStreet startNode, NodeStreet endNode)
    {
        waypoints = AStar.PathFromTo(startNode, endNode);
    }

    private NodeStreet NearestNode()
    {
        NodeStreet nearNode = null;

        float minDist = Mathf.Infinity;
        var colls = Physics.OverlapSphere(transform.position, 14, LayerMask.GetMask("network"));

        foreach (Collider c in colls)
        {
            if ( Vector3.Distance(transform.position, c.gameObject.transform.position) < minDist)
            {
                minDist = Vector3.Distance(transform.position, c.gameObject.transform.position);
                nearNode = c.gameObject.GetComponent<NodeHandler>().node;
            }
        }

        return nearNode;
    }



}
