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
            // checking for null node
            foreach (NodeStreet n in networkNodes)
                if (n.availableStreets.Count == 0)
                {
                    Instantiate(sphere, n.nodePosition + Vector3.up * 3f, Quaternion.identity);
                } else
                {
                    foreach(ArcStreet a in n.availableStreets)
                    Debug.DrawLine(n.nodePosition, a.arrivalNode.nodePosition + Vector3.up/2, Color.black, Mathf.Infinity);

                }

            spawn = false;


            //Debug.Log(prova.availableStreets.Count);
            startNode = GetNearestNode(testStart.position);

            endNode = GetNearestNode(testDestination.position);

            var pathFinder = new AStar(startNode, endNode);
            var found = pathFinder.PathFinder();
            var path = new List<Vector3>();
            foreach (NodeStreet n in pathFinder.path)
                path.Add(n.nodePosition);

            Debug.Log(found);
            Debug.Log(path.Count);


            foreach (Vector3 v in path)
                tripPlanner.SetPosition(++tripPlanner.positionCount-1, v + Vector3.up*2f);

            //carsManager.SpawnCar(startNode.nodePosition, path);

            

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
            if (dist < Vector3.Distance(minDistNode.nodePosition, pos)  && node.availableStreets.Count > 0 )
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
