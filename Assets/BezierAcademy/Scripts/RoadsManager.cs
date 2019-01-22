using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsManager : MonoBehaviour {


    public GameObject sferettaBlu;
    public GameObject crossSphere;


    public GameObject road;
    Dictionary<GameObject, bool> roads = new Dictionary<GameObject, bool>();
    GameObject toRemove;

    NodeStreet startNode;

    int lenghtCurStreet;
    Vector3[] curArray;
    List<Vector3> curList = new List<Vector3>();
    List<List<Vector3>> listOfStreets = new List<List<Vector3>>();
    List<Vector3> curStreet = new List<Vector3>();

    List<NodeStreet> nodes = new List<NodeStreet>();






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

    public void CreateNavMeshPoints()
    {
        listOfStreets = new List<List<Vector3>>();
        curStreet = new List<Vector3>();
        foreach (GameObject road in roads.Keys)
        {
            var wayPoints = road.GetComponent<RoadSpawn>().snapPointList;
            for (int snap = 0; snap < wayPoints.Count; snap++)
            {
                if (wayPoints[snap].GetComponent<IsCollidingScript>().isColliding)
                {

                    // Instantiating the ball who represents the cross
                    var curCross = Instantiate(crossSphere, wayPoints[snap].transform.position+Vector3.up, Quaternion.identity);
                    var colls = Physics.OverlapSphere(curCross.transform.position, 1, LayerMask.GetMask("cross"));
                    if (colls.Length > 0)
                        foreach(Collider c in colls)
                            if (c.gameObject != curCross)
                            {
                                curCross.transform.position = (curCross.transform.position + c.gameObject.transform.position) / 2f;
                                curCross.transform.position -= curCross.transform.position.y * Vector3.up;
                                Destroy(c.gameObject);
                            }


                    FinishCurStreet();
                }
                else
                {
                    if (snap + 1 < wayPoints.Count)
                    {
                        Vector3 forward = wayPoints[snap + 1].transform.position - wayPoints[snap].transform.position;
                        Vector3 left = new Vector3(-forward.z, 0, forward.x);
                        curStreet.Add(wayPoints[snap].transform.position - left.normalized / 2);
                    }
                    else
                    {
                        Vector3 forward = wayPoints[snap].transform.position - wayPoints[snap - 1].transform.position;
                        Vector3 left = new Vector3(-forward.z, 0, forward.x);
                        curStreet.Add(wayPoints[snap].transform.position - left.normalized / 2);
                    }
                }
            }
            FinishCurStreet();
        }
    }


    public void FinishCurStreet()
    {
        var wayPoints = road.GetComponent<RoadSpawn>().snapPointList;
        lenghtCurStreet = curStreet.Count;
        for (int i = lenghtCurStreet - 1; i >= 0; i--)
        {
            curStreet.Add(curStreet[i]);
        }
        for (int i = lenghtCurStreet; i < curStreet.Count; i++)
        {
            if (i + 1 < curStreet.Count)
            {
                Vector3 forward = curStreet[i + 1] - curStreet[i];
                Vector3 left = new Vector3(-forward.z, 0, forward.x);
                curStreet[i] = curStreet[i] - left.normalized;
            }
            else
            {
                Vector3 forward = curStreet[i - 1] - curStreet[i - 2];
                Vector3 left = new Vector3(-forward.z, 0, forward.x);
                curStreet[i] = curStreet[i] - left.normalized;
            }
        }

        foreach (Vector3 v in curStreet)
        {
            Instantiate(sferettaBlu, v, Quaternion.identity);
        }

        curArray = new Vector3[curStreet.Count];
        curStreet.CopyTo(curArray);
        curList = new List<Vector3>();
        foreach (Vector3 v in curArray)
        {
            curList.Add(v);
        }
        listOfStreets.Add(curList);
        curStreet.Clear();
    }



    public void CompleteNetwork()
    {

       // Create the navmesh
       CreateNavMeshPoints();

        // Disabling the snaps used for the nav mesh creation
        foreach (GameObject g in roads.Keys)
            if (roads[g] && g)
                g.GetComponent<RoadSpawn>().DisableSnaps();

    }












    /*          SECOND PART TO BE IMPLEMENTED AS SOON AS WE FINISH WITH THE NAV MESH           */

    //public void CompleteNetwork()
    //{

    //    int k = 0;
    //    foreach(GameObject g in roads.Keys)
    //    {
    //        var curSpawner = g.GetComponent<RoadSpawn>();
    //        curSpawner.FillBack();
    //        if (k++ == 0)
    //            CrossMergeProva(curSpawner);
    //    }


    //    // calling the line render and A* calculation
    //    nb.spawn = true;
    //}


    public void CrossMergeProva(RoadSpawn rs)
    {
        startNode = new NodeStreet(rs.snapPointList[0].transform.position);
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

                curRs.snapPointList.Remove(waypoint); // removing the current waypoint
                LinkToNext(nextOneToLink, newNode);  // making the next waypoint link
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


                LinkToNext(nextOneToLink, newNode);  // making the next waypoint link
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
                var curStreet = new ArcStreet(lastNode.nodePosition, startNode.nodePosition);
                curStreet.AddNode(startNode);
                lastNode.AddStreet(curStreet);

                // adding to the network
                nb.networkNodes.Add(lastNode);
                nb.arcStreets.Add(curStreet);


                //waypoint.transform.position += Vector3.up;
                curRs.snapPointList.Remove(waypoint); // removing the current waypoint

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
