using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadSpawn))]
public class Network : MonoBehaviour {

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
                tripPlanner.SetPosition(++tripPlanner.positionCount-1, v + Vector3.up*2f);

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
        roadSpawn.CompleteRoadNetwork();

        // Destroy streetPoint which collide with a cross
        var toRemove = new List<GameObject>();
        var crosses = GameObject.FindGameObjectsWithTag("crossPoint");
        foreach(GameObject cross in crosses)
        {
            // Destroying overlapping streetPoint  TODO : they shouldn't be there
            var colls = Physics.OverlapSphere(cross.transform.position, 4f, LayerMask.GetMask("network"));
            for (int i = 0; i < colls.Length; i++)
                if (colls[i].gameObject.tag == "streetPoint")
                    Destroy(colls[i].gameObject);
        }
        StartCoroutine(NetworkCreation());

    }

    IEnumerator NetworkCreation()
    {
        yield return new WaitForFixedUpdate();

        var pointsOfNetwork = GameObject.FindGameObjectsWithTag("streetPoint");

        foreach (GameObject g in pointsOfNetwork)
        {
            var curNode = g.GetComponent<NodeHandler>().node;

            var colls = Physics.OverlapSphere(g.transform.position + (g.transform.forward.normalized * 14f), 2f, LayerMask.GetMask("network"));
            if (colls.Length > 0)
                foreach (Collider c in colls)
                {
                    if (c.gameObject == g)
                        continue;
                    var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
                    var curStreet = new ArcStreet(curNode.nodePosition, nextNode.nodePosition);
                    curStreet.AddNode(nextNode);
                    curNode.AddStreet(curStreet);

                    arcStreets.Add(curStreet);
                }
            else
            {
                colls = Physics.OverlapSphere(g.transform.position - 2 * g.transform.right, 2f, LayerMask.GetMask("network"));
                foreach (Collider c in colls)
                {
                    if (c.gameObject == g)
                        continue;
                    var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
                    var curStreet = new ArcStreet(curNode.nodePosition, nextNode.nodePosition);
                    curStreet.AddNode(nextNode);
                    curNode.AddStreet(curStreet);

                    arcStreets.Add(curStreet);
                }
            }

            nodeStreets.Add(curNode);
        }


        var crosses = GameObject.FindGameObjectsWithTag("crossPoint");
        foreach (GameObject cross in crosses)
        {
            var curNode = cross.GetComponent<NodeHandler>().node;

            // Finding the street I can reach from the cross
            var checkPos = cross.transform.position + (Vector3.forward * 14 + Vector3.right * 3f);
            Debug.DrawLine(cross.transform.position, checkPos + Vector3.up, Color.blue, Mathf.Infinity);
            var colls = Physics.OverlapSphere(checkPos, 2f, LayerMask.GetMask("network"));
            foreach (Collider c in colls)
            {
                var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
                var curStreet = new ArcStreet(curNode.nodePosition, nextNode.nodePosition);
                curStreet.AddNode(nextNode);
                curNode.AddStreet(curStreet);

                arcStreets.Add(curStreet);
            }
            nodeStreets.Add(curNode);

            checkPos = cross.transform.position + ( - Vector3.forward * 14 - Vector3.right * 3f);
            Debug.DrawLine(cross.transform.position, checkPos + Vector3.up, Color.blue, Mathf.Infinity);
            colls = Physics.OverlapSphere(checkPos, 2f, LayerMask.GetMask("network"));
            foreach (Collider c in colls)
            {
                var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
                var curStreet = new ArcStreet(curNode.nodePosition, nextNode.nodePosition);
                curStreet.AddNode(nextNode);
                curNode.AddStreet(curStreet);

                arcStreets.Add(curStreet);
            }
            nodeStreets.Add(curNode);


            checkPos = cross.transform.position + ( - Vector3.forward * 3 + Vector3.right * 14f);
            Debug.DrawLine(cross.transform.position, checkPos + Vector3.up, Color.blue, Mathf.Infinity);
            colls = Physics.OverlapSphere(checkPos, 2f, LayerMask.GetMask("network"));
            foreach (Collider c in colls)
            {
                var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
                var curStreet = new ArcStreet(curNode.nodePosition, nextNode.nodePosition);
                curStreet.AddNode(nextNode);
                curNode.AddStreet(curStreet);

                arcStreets.Add(curStreet);
            }
            nodeStreets.Add(curNode);


            checkPos = cross.transform.position + (Vector3.forward * 3 - Vector3.right * 14f);
            Debug.DrawLine(cross.transform.position, checkPos + Vector3.up, Color.blue, Mathf.Infinity);
            colls = Physics.OverlapSphere(checkPos, 2f, LayerMask.GetMask("network"));
            foreach (Collider c in colls)
            {
                var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
                var curStreet = new ArcStreet(curNode.nodePosition, nextNode.nodePosition);
                curStreet.AddNode(nextNode);
                curNode.AddStreet(curStreet);

                arcStreets.Add(curStreet);
            }
            nodeStreets.Add(curNode);

        }


        foreach (GameObject g in pointsOfNetwork)
        {
            var curNode = g.GetComponent<NodeHandler>().node;
            foreach (ArcStreet a in curNode.availableStreets)
            {
                Debug.DrawLine(a.beginPos, a.endPos + Vector3.up, Color.white, Mathf.Infinity);
            }
        }
        foreach (GameObject g in crosses)
        {
            var curNode = g.GetComponent<NodeHandler>().node;
            foreach (ArcStreet a in curNode.availableStreets)
            {
                Debug.DrawLine(a.beginPos, a.endPos + Vector3.up, Color.white, Mathf.Infinity);
            }
        }
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


    public void spawnCar()
    {
        spawn = true;
    }


}
