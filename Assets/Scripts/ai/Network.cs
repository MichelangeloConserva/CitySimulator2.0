using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadSpawn))]
public class Network : MonoBehaviour
{

    [Header("Testing variables")]
    public Transform testDestination;
    public Transform testStart;
    public LineRenderer tripPlanner;
    public bool spawn;

    [Header("SETTINGS")]
    public float minDistFromNode;

    [Header("GAME OBJECTS REQUIRED")]
    public CarsManager carsManager;
    public RoadSpawn roadSpawn;
    public GameObject sphere;

    [Header("Storage for nodes and arcs")]
    public List<NodeStreet> nodeStreets;

    private NodeStreet startNode;
    private NodeStreet endNode;


    void Start()
    {
        // Initialize the List which A* uses to calculate the path
        nodeStreets = new List<NodeStreet>();

        // Linking to the components
        roadSpawn = GetComponent<RoadSpawn>();

        // Testing for the line renderer
        tripPlanner.startColor = Color.green;
        tripPlanner.endColor = Color.cyan;
        tripPlanner.positionCount = 0;
    }

    void Update()
    {

        // Testing
        if (spawn)
        {
            startNode = GetNearestNode(testStart.position);
            Instantiate(sphere, startNode.nodePosition + Vector3.up * 10f, Quaternion.identity);

            endNode = GetNearestNode(testDestination.position);
            Instantiate(sphere, endNode.nodePosition + Vector3.up * 10f, Quaternion.identity);


            var pathFinder = new AStar(startNode, endNode);
            var found = pathFinder.PathFinder();
            var path = new List<Vector3>();
            foreach (NodeStreet n in pathFinder.path)
                path.Add(n.nodePosition);

            foreach (Vector3 v in path)
                tripPlanner.SetPosition(++tripPlanner.positionCount - 1, v + Vector3.up * 2f);

            Debug.Log(path.Count);

            if (path.Count > 0)
                carsManager.SpawnCar(startNode.nodePosition, path);

            spawn = false;
        }
    }

    /// <summary>
    /// Called by button "Done This chunk" to store the nodes and arc of the street
    /// </summary>
    public void GetCompletedRoad()
    {
        // Complete the network on the side of the roads
        roadSpawn.CompleteRoadNetwork();

        // Destroy streetPoint which collide with a cross
        var toRemove = new List<GameObject>();
        var crosses = GameObject.FindGameObjectsWithTag("crossPoint");
        foreach (GameObject cross in crosses)
        {
            // Destroying overlapping streetPoint  TODO : they shouldn't be there
            var colls = Physics.OverlapSphere(cross.transform.position, 4f, LayerMask.GetMask("network"));
            for (int i = 0; i < colls.Length; i++)
                if (colls[i].gameObject.tag == "streetPoint")
                    Destroy(colls[i].gameObject);
        }

        // The creation of the network must be done a frame later since we are deleting the 
        // unncessary streetPoints
        StartCoroutine(NetworkCreation());

    }

    IEnumerator NetworkCreation()
    {
        yield return new WaitForFixedUpdate();

        var pointsOfNetwork = GameObject.FindGameObjectsWithTag("streetPoint");
        foreach (GameObject streetPoint in pointsOfNetwork)
            // Finding the street I can reach from the streetPoint
            FromStreetPointNodesCreation(streetPoint);

        var crosses = GameObject.FindGameObjectsWithTag("crossPoint");
        foreach (GameObject cross in crosses)
            // Finding the street I can reach from the cross
            FromCrossNodesCreation(cross);

        // Network Debbugging 
        foreach (GameObject g in pointsOfNetwork)
            foreach (ArcStreet a in g.GetComponent<NodeHandler>().node.availableStreets)
                DrawArrow.ForDebug(a.beginPos + Vector3.up, 
                                   (a.endPos + Vector3.up) - (a.beginPos + Vector3.up),
                                   Color.white,
                                   Mathf.Infinity);
        foreach (GameObject g in crosses)
            foreach (ArcStreet a in g.GetComponent<NodeHandler>().node.availableStreets)
                DrawArrow.ForDebug(a.beginPos + Vector3.up,
                                   (a.endPos + Vector3.up) - (a.beginPos + Vector3.up),
                                   Color.white,
                                   Mathf.Infinity);
    }

    private void FromStreetPointNodesCreation(GameObject streetPoint)
    {
        var curNode = streetPoint.GetComponent<NodeHandler>().node;

        // Checking for nodes in front of the current node
        var colls = Physics.OverlapSphere(streetPoint.transform.position + (streetPoint.transform.forward.normalized * 14f), 2f, LayerMask.GetMask("network"));
        if (colls.Length > 0)
            CheckAtPositionForNodesFromStreetPoint(colls, streetPoint, curNode);
        else
        {   // I m at the end of the street since not nodes in front so I add to possibility to make a u turn
            colls = Physics.OverlapSphere(streetPoint.transform.position - 4 * streetPoint.transform.right, 3f, LayerMask.GetMask("network"));
            CheckAtPositionForNodesFromStreetPoint(colls, streetPoint, curNode);
        }

        nodeStreets.Add(curNode);
    }

    private void CheckAtPositionForNodesFromStreetPoint(Collider[] colls, GameObject streetPoint, NodeStreet curNode)
    {
        foreach (Collider c in colls)
        {
            if (c.gameObject == streetPoint)
                continue;
            var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
            var curStreet = new ArcStreet(curNode.nodePosition, nextNode.nodePosition);
            curStreet.AddNode(nextNode);
            curNode.AddStreet(curStreet);
        }
    }


    private void FromCrossNodesCreation(GameObject cross)
    {
        var curNode = cross.GetComponent<NodeHandler>().node;

        // all the four positions to check from a cross
        Vector3[] checkPositions = {
            cross.transform.position + (Vector3.forward * 14 + Vector3.right * 3f),  
            cross.transform.position + (-Vector3.forward * 14 - Vector3.right * 3f),
            cross.transform.position + (-Vector3.forward * 3 + Vector3.right * 14f),
            cross.transform.position + (Vector3.forward * 3 - Vector3.right * 14f)
        };

        foreach (Vector3 checkPos in checkPositions)
            CheckAtPositionForNodesFromCross(checkPos, curNode);
    }

    private void CheckAtPositionForNodesFromCross(Vector3 checkPos, NodeStreet curNode, float radius = 2f)
    {
        var colls = Physics.OverlapSphere(checkPos, radius, LayerMask.GetMask("network"));
        foreach (Collider c in colls)
        {
            var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
            var curStreet = new ArcStreet(curNode.nodePosition, nextNode.nodePosition);
            curStreet.AddNode(nextNode);
            curNode.AddStreet(curStreet);
        }
        nodeStreets.Add(curNode);
    }


    /// <summary>
    /// Returns the nearest node with a simple loop search
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public NodeStreet GetNearestNode(Vector3 pos)
    {
        NodeStreet minDistNode = nodeStreets[0];
        for (int i = 1; i < nodeStreets.Count; i++)
        {
            var node = nodeStreets[i];

            var dist = Vector3.Distance(node.nodePosition, pos);
            if (dist < Vector3.Distance(minDistNode.nodePosition, pos))
                minDistNode = node;

            if (dist < minDistFromNode)
                break;
        }
        if (minDistNode == null) { Debug.LogError("No node found", this.gameObject); }

        return minDistNode;
    }


    public void SpawnCar()
    {
        spawn = true;
    }


}
