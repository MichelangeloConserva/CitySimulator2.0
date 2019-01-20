using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadSpawn))]
public class Network : MonoBehaviour {

    [Header("Testing variables")]
    public Transform testDestination;
    public Transform testStart;
    public LineRenderer tripPlanner;
    public bool first;
    public bool done;

    [Header("SETTINGS")]
    public float minDistFromNode;

    [Header("GAME OBJECTS REQUIRED")]
    public RoadSpawn roadSpawn;
    public GameObject sphere;

    [Header("Storage for nodes and arcs")]
    public List<NodeStreet> nodeStreets;
    public List<ArcStreet> arcStreets;

    public NodeStreet startNode;
    public NodeStreet endNode;


	void Start () {
        nodeStreets = new List<NodeStreet>();
        arcStreets = new List<ArcStreet>();

        roadSpawn = GetComponent<RoadSpawn>();

        // Testing
        tripPlanner.startColor = Color.green;
        tripPlanner.endColor = Color.cyan;
        tripPlanner.positionCount = 0;
        first = false;
        done = false;
    }

    void Update()
    {

        // Testing
        if (first & Time.time > 15f )
        {
            Debug.Log("Beginning search");
            first = false;

            startNode = GetNearestNode(testStart.position);
            Instantiate(sphere, startNode.nodePosition + Vector3.up * 2f, Quaternion.identity);

            endNode = GetNearestNode(testDestination.position);
            Instantiate(sphere, endNode.nodePosition + Vector3.up * 2f, Quaternion.identity);

            var pathFinder = new AStar(startNode, endNode);
            var found = pathFinder.PathFinder();
            var path = pathFinder.path;

            foreach (NodeStreet node in path)
                tripPlanner.SetPosition(++tripPlanner.positionCount-1, node.nodePosition+ Vector3.up*2f);

            done = true;
            Debug.Log("Finished search");
            

        }
    }
	
    /// <summary>
    /// Called by button "Done This chunk" to store the nodes and arc of the street
    /// </summary>
    public void GetCompletedRoad()
    {
        roadSpawn.CompleteRoadNetwork();

        // TODO : Setting the streets faster
        foreach (NodeStreet node in nodeStreets)
        {
            var nearNodes = new List<NodeStreet>();
            var colls = Physics.OverlapSphere(node.nodePosition + Vector3.up * 2f, 1.1f);
            foreach (Collider c in colls)
            {
                if (c.gameObject.transform.position != node.nodePosition + Vector3.up * 2f)
                {
                    var near = GetNearestNode(c.gameObject.transform.position);
                    var arc = new ArcStreet(node.nodePosition, near.nodePosition);
                    arc.arrivalNode = near;
                    node.availableStreets.Add(arc);
                }
            }
        }
        foreach (GameObject g in roadSpawn.spheres.ToArray())
            Destroy(g);
        

        //// instantiating nodes and arc of the new street
        //var nodeBegin = new NodeStreet(curRoad[0]);
        //var nodeEnd = new NodeStreet(curRoad[1]);
        //var arc = new ArcStreet(nodeBegin.nodePosition, nodeEnd.nodePosition);

        //// Giving references to node and arc
        //nodeBegin.AddStreet(arc);
        //arc.AddNode(nodeEnd);

        //// Storing the references
        //nodeStreets.Add(nodeBegin);
        //nodeStreets.Add(nodeEnd);
        //arcStreets.Add(arc);

        //Instantiate(sphere, nodeBegin.nodePosition + Vector3.up*2, Quaternion.identity);
        //Instantiate(sphere, nodeEnd.nodePosition + Vector3.up * 2, Quaternion.identity);
    }

    /// <summary>
    /// Returns the nearest node with a simple loop search
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public NodeStreet GetNearestNode(Vector3 pos)
    {
        NodeStreet minDistNode = nodeStreets[0];
        for(int i=1; i<nodeStreets.Count; i++) 
        {
            var node = nodeStreets[i];

            var dist = Vector3.Distance(node.nodePosition, pos);
            if (dist < Vector3.Distance(minDistNode.nodePosition, pos))
                minDistNode = node;

            if (dist < minDistFromNode)
                break;
        }
        if (minDistNode == null) { Debug.LogError("No node found",this.gameObject); }

        return minDistNode;
    }


}
