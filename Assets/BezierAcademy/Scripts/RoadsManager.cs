using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsManager : MonoBehaviour {

    public GameObject road;
    Dictionary<GameObject, bool> roads = new Dictionary<GameObject, bool>();
    GameObject toRemove;


    public NetworkBezier nb;
    
    public void SpawnRoad()
    {
        roads.Add(Instantiate(road), true);     
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject g in roads.Keys)
                if (roads[g] && g)
                    g.GetComponent<RoadSpawn>().OnDisableEditing();
                else
                    toRemove = g;
            if (toRemove)
                roads.Remove(toRemove);
        }
    }

    public void CompleteNetwork()
    {

        int k = 0;
        foreach(GameObject g in roads.Keys)
        {
            var curSpawner = g.GetComponent<RoadSpawn>();
            if (k++ == 0)
                CrossMergeProva(curSpawner);
        }


        // calling the line render and A* calculation
        nb.spawn = true;
    }


    public void CrossMergeProva(RoadSpawn rs)
    {
        var startNode = new NodeStreet(rs.snapPointList[0].transform.position);
        LinkToNext(rs.snapPointList[0], startNode);
    }

    public void LinkToNext(GameObject waypoint, NodeStreet lastNode)
    {
        var curRs = waypoint.GetComponentInParent<RoadSpawn>();


        // At the cross
        if (waypoint.GetComponent<IsCollidingScript>().isColliding)
        {
            var otherSh = waypoint.GetComponent<IsCollidingScript>().otherSphere;
            var otherRoadSpawn = otherSh.GetComponentInParent<RoadSpawn>();
            Debug.DrawLine(Vector3.zero, otherRoadSpawn.NextWaypoint(otherSh).transform.position, Color.black, Mathf.Infinity);
            Debug.DrawLine(Vector3.zero, otherRoadSpawn.NextWaypoint(otherRoadSpawn.NextWaypoint(otherSh)).transform.position, Color.green, Mathf.Infinity);

            // getting the next waypoint
            var nextOneToLink = otherRoadSpawn.NextWaypoint(otherSh);

            if (otherRoadSpawn.snapPointList.Count > 1 && nextOneToLink != null)  // at least two nodes to link
            {

                // Creating network links
                var newNode = new NodeStreet(nextOneToLink.transform.position);
                var curStreet = new ArcStreet(lastNode.nodePosition, newNode.nodePosition);
                curStreet.AddNode(newNode);
                lastNode.AddStreet(curStreet);

                // adding to the network
                nb.networkNodes.Add(lastNode);
                nb.arcStreets.Add(curStreet);


                //waypoint.transform.position += Vector3.up;
                LinkToNext(nextOneToLink, newNode);  // making the next waypoint link
            }
            else if (otherRoadSpawn.snapPointList.Count > 0)
            {
            }






            // getting the next waypoint
            nextOneToLink = curRs.NextWaypoint(waypoint);

            if (curRs.snapPointList.Count > 1 && nextOneToLink != null)  // at least two nodes to link
            {

                // Creating network links
                var newNode = new NodeStreet(nextOneToLink.transform.position);
                var curStreet = new ArcStreet(lastNode.nodePosition, newNode.nodePosition);
                curStreet.AddNode(newNode);
                lastNode.AddStreet(curStreet);

                // adding to the network
                nb.networkNodes.Add(lastNode);
                nb.arcStreets.Add(curStreet);


                //waypoint.transform.position += Vector3.up;
                LinkToNext(nextOneToLink, newNode);  // making the next waypoint link
            }
            else if (curRs.snapPointList.Count > 0)
            {
                curRs.snapPointList.Remove(waypoint); // removing the current waypoint which is the last one
            }

        }  // just straight
        else
        {

            // getting the next waypoint
            var nextOneToLink = curRs.NextWaypoint(waypoint);

            if (curRs.snapPointList.Count > 1 && nextOneToLink != null)  // at least two nodes to link
            {

                // Creating network links
                var newNode = new NodeStreet(nextOneToLink.transform.position);
                var curStreet = new ArcStreet(lastNode.nodePosition, newNode.nodePosition);
                curStreet.AddNode(newNode);
                lastNode.AddStreet(curStreet);

                // adding to the network
                nb.networkNodes.Add(lastNode);
                nb.arcStreets.Add(curStreet);


                //waypoint.transform.position += Vector3.up;
                curRs.snapPointList.Remove(waypoint); // removing the current waypoint
                LinkToNext(nextOneToLink, newNode);  // making the next waypoint link
            }
            else if (curRs.snapPointList.Count > 0)
            {
                curRs.snapPointList.Remove(waypoint); // removing the current waypoint which is the last one
            }
        }
    }








    /*var otherSh = waypoint.GetComponent<IsCollidingScript>().otherSphere;
            var otherWaypoints = otherSh.GetComponent<IsCollidingScript>().BeforeAndAfterTheCollisionPoint();


            if (curRs.snapPointList.Count > 1 && otherWaypoints!=null)  // at least two nodes to link
            {

                // getting the next waypoint
                var nextOnesToLink = new List<GameObject>
                {
                    curRs.NextWaypoint(waypoint),
                    //otherWaypoints[0],
                    //otherWaypoints[1]
                };

                // Creating network links
                foreach (GameObject nextOneToLink in nextOnesToLink)
                {
                    var newNode = new NodeStreet(nextOneToLink.transform.position);
                    var curStreet = new ArcStreet(lastNode.nodePosition, newNode.nodePosition);
                    curStreet.AddNode(lastNode);
                    lastNode.AddStreet(curStreet);

                    // adding to the network
                    nb.networkNodes.Add(lastNode);
                    nb.arcStreets.Add(curStreet);


                    //waypoint.transform.position += Vector3.up;
                    nextOneToLink.GetComponentInParent<RoadSpawn>().snapPointList.Remove(waypoint); // removing the current waypoint
                    LinkToNext(nextOneToLink, newNode);  // making the next waypoint link
                }

            }
            else if (curRs.snapPointList.Count > 0)
            {
                curRs.snapPointList.Remove(waypoint); // removing the current waypoint which is the last one
            }*/



}
