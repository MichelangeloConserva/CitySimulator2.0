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


    float outLanesWidth = 10f;
    float innerLanesWidth = 4f;
    



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
                    curCross.GetComponent<CrossNodeHandler>().InitializeNode();
                    var colls = Physics.OverlapSphere(curCross.transform.position, 1, LayerMask.GetMask("crossSphere"));
                    if (colls.Length > 0)
                        foreach(Collider c in colls)
                            if (c.gameObject != curCross)
                            {
                                curCross.transform.position = (curCross.transform.position + c.gameObject.transform.position) / 2f;
                                curCross.transform.position -= curCross.transform.position.y * Vector3.up;
                                Debug.Log(c.gameObject.GetComponent<CrossNodeHandler>().crossNode);
                                curCross.GetComponent<CrossNodeHandler>().crossNode = c.gameObject.GetComponent<CrossNodeHandler>().crossNode;
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
                        left *= outLanesWidth;
                        curStreet.Add(wayPoints[snap].transform.position - left / 2);
                    }
                    else
                    {
                        Vector3 forward = wayPoints[snap].transform.position - wayPoints[snap - 1].transform.position;
                        Vector3 left = new Vector3(-forward.z, 0, forward.x);
                        left *= outLanesWidth;
                        curStreet.Add(wayPoints[snap].transform.position - left / 2);
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
                left *= outLanesWidth;
                curStreet[i] = curStreet[i] - left;
            }
            else
            {
                Vector3 forward = curStreet[i - 1] - curStreet[i - 2];
                Vector3 left = new Vector3(-forward.z, 0, forward.x);
                left *= outLanesWidth;
                curStreet[i] = curStreet[i] - left;
            }
        }

        foreach (Vector3 v in curStreet)
        {
            Instantiate(sferettaBlu, v, Quaternion.identity, transform);
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

        // Creating the network
        CreateNetwork();


        // A* calculation
        nb.spawn = true;

    }

    NodeStreet curStarter;
    public void CreateNetwork()
    {
        foreach (List<Vector3> street in listOfStreets)
        {
            curStarter = new NodeStreet(street[0]);
            LinkToNext(curStarter,1,street);
        }
    }

    public void LinkToNext(NodeStreet lastNode, int index, List<Vector3> street)
    {
        if (index >= street.Count)
        {
            var newNode = curStarter;
            var curStreet = new ArcStreet(lastNode.nodePosition, newNode.nodePosition);
            curStreet.lenght = Vector3.Distance(lastNode.nodePosition, newNode.nodePosition);
            curStreet.AddNode(newNode);
            lastNode.AddStreet(curStreet);

            // adding to the network
            nb.networkNodes.Add(lastNode);
            nb.arcStreets.Add(curStreet);

            nb.networkNodes.Add(lastNode);
            nb.arcStreets.Add(curStreet);
            return;
        }
    


        Collider[] checkForCross = new Collider[0];
        if (index > 0)
        {
            Vector3 forward = street[index] - street[index - 1];
            checkForCross = Physics.OverlapSphere(street[index] + forward.normalized/2, 0.5f, LayerMask.GetMask("crossSphere"));
        }

        // No cross found
        if (checkForCross.Length == 0)
        {
            var newNode = new NodeStreet(street[index]);
            var curStreet = new ArcStreet(lastNode.nodePosition, newNode.nodePosition);
            curStreet.lenght = Vector3.Distance(lastNode.nodePosition, newNode.nodePosition);
            curStreet.AddNode(newNode);
            lastNode.AddStreet(curStreet);

            // adding to the network
            nb.networkNodes.Add(lastNode);
            nb.arcStreets.Add(curStreet);


            nb.networkNodes.Add(lastNode);
            nb.arcStreets.Add(curStreet);

            if (index + 1 < street.Count)
                LinkToNext(newNode, index + 1, street);
            else
            {   // adding the last to the first
                nb.networkNodes.Add(newNode);

                lastNode = newNode;
                newNode = curStarter;
                curStreet = new ArcStreet(lastNode.nodePosition, newNode.nodePosition);
                curStreet.lenght = Vector3.Distance(lastNode.nodePosition, newNode.nodePosition);
                curStreet.AddNode(newNode);
                lastNode.AddStreet(curStreet);

                // adding to the network
                nb.arcStreets.Add(curStreet);

            }
        }
        else
        {

            // Connecting the last one to the current node
            var newNode = new NodeStreet(street[index]);
            var curStreet = new ArcStreet(lastNode.nodePosition, newNode.nodePosition);
            curStreet.lenght = Vector3.Distance(lastNode.nodePosition, newNode.nodePosition);
            curStreet.AddNode(newNode);
            lastNode.AddStreet(curStreet);

            // adding to the network
            nb.networkNodes.Add(lastNode);
            nb.arcStreets.Add(curStreet);

            nb.networkNodes.Add(lastNode);
            nb.arcStreets.Add(curStreet);



            // Connecting the current to the node in the cross
            var crossNode = checkForCross[0].GetComponent<CrossNodeHandler>().crossNode;
            curStreet = new ArcStreet(newNode.nodePosition, crossNode.nodePosition);
            curStreet.lenght = Vector3.Distance(newNode.nodePosition, crossNode.nodePosition);
            curStreet.AddNode(crossNode);
            newNode.AddStreet(curStreet);

            // adding to the network
            nb.networkNodes.Add(newNode);
            nb.arcStreets.Add(curStreet);


            // giving the reference of the node in the cross
            LinkToNext(crossNode, index + 1, street);
            

        }
    }






}
