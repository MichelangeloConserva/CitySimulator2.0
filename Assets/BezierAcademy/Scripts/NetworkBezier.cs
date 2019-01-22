using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBezier : MonoBehaviour {

    [Header("Testing variables")]
    public Transform testDestination;
    public Transform testStart;
    public LineRenderer tripPlanner;
    public bool spawn;

    [Header("SETTINGS")]
    public float minDistFromNode;

    [Header("GAME OBJECTS REQUIRED")]
    public CarsManagerBezier carsManager;
    public GameObject sphere;

    [Header("Storage for nodes and arcs")]
    public List<NodeStreet> networkNodes;
    public List<ArcStreet> arcStreets;

    public NodeStreet startNode;
    public NodeStreet endNode;

    public NodeStreet prova;


	void Start () {
        networkNodes = new List<NodeStreet>();
        arcStreets = new List<ArcStreet>();

        // Testing
        tripPlanner.startColor = Color.green;
        tripPlanner.endColor = Color.cyan;
        tripPlanner.positionCount = 0;
    }

    void Update()
    {
        // Testing
        if (spawn)
        {
            Debug.Log(networkNodes.Count);
            foreach (NodeStreet n in networkNodes)
                Debug.DrawLine(Vector3.up*5, n.nodePosition, Color.red, Mathf.Infinity);


            //Debug.Log(prova.availableStreets.Count);
            startNode = GetNearestNode(testStart.position);
            Instantiate(sphere, startNode.nodePosition + Vector3.up * 2f, Quaternion.identity);


            endNode = GetNearestNode(testDestination.position);
            Instantiate(sphere, endNode.nodePosition + Vector3.up * 2f, Quaternion.identity);

            var pathFinder = new AStar(startNode, endNode);
            var found = pathFinder.PathFinder();
            var path = new List<Vector3>();
            foreach (NodeStreet n in pathFinder.path)
                path.Add(n.nodePosition);


            foreach (Vector3 v in path)
                tripPlanner.SetPosition(++tripPlanner.positionCount-1, v + Vector3.up*2f);

            carsManager.SpawnCar(startNode.nodePosition, path);

            spawn = false;

        }
    }
	
    /// <summary>
    /// Returns the nearest node with a simple loop search
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public NodeStreet GetNearestNode(Vector3 pos)
    {
        NodeStreet minDistNode = networkNodes[0];
        for(int i=1; i<networkNodes.Count; i++) 
        {
            var node = networkNodes[i];

            var dist = Vector3.Distance(node.nodePosition, pos);
            if (dist < Vector3.Distance(minDistNode.nodePosition, pos) )
                minDistNode = node;

            if (dist < minDistFromNode)
                break;
        }
        if (minDistNode == null) { Debug.LogError("No node found",this.gameObject); }

        return minDistNode;
    }


    public void spawnCar()
    {
        spawn = true;
    }


}
