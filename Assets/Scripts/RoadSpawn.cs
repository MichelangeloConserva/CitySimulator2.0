using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoadSpawn : MonoBehaviour {

    [Header("Useful Settings")]
    [Space]
    public bool editMode;

    [Header("Links required")]
    [Space]
    public GameObject NetworkPoints;
    public GameObject roadChunk;
    public GameObject crossChunk;
    public GameObject leftCrossChunk;
    public GameObject rightCrossChunk;
    public GameObject leftCurve;
    public GameObject rightCurve;
    public Network net;
    public GameObject chunkGarage;
    public GameObject crossGarage;

    [Header("Storage for the network")]
    [Space]
    public List<GameObject> allBlocks;
    public List<GameObject> allCrosses;
    public List<GameObject> curBlocks;
    public List<GameObject> spheres;

    [Header("Settings for the lane size")]
    [Space]
    public float outLanesWidth = 4.2f;
    public float innerLanesWidth = 1.8f;

    private GameObject curPointer;

    public List<GameObject> streetPointsToUpdate;
    public List<GameObject> crossPointsToUpdate;


    void Start()
    {
        allBlocks = new List<GameObject>();
        allCrosses = new List<GameObject>();
        spheres = new List<GameObject>();

        editMode = true;


        streetPointsToUpdate = GameObject.FindGameObjectsWithTag("streetPoint").ToList();
        crossPointsToUpdate = GameObject.FindGameObjectsWithTag("crossPoint").ToList();
        foreach(GameObject curvePoint in GameObject.FindGameObjectsWithTag("curvePoint"))
            crossPointsToUpdate.Add(curvePoint);

        UpdateNetwork();
    }

    void Update()
    {
        if (curBlocks.Count == 1)
        {
            var chunketto = curBlocks[0];

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0)
            {
                if (-0.5f <= chunketto.transform.rotation.eulerAngles.y && chunketto.transform.rotation.y <= 0.5f)
                    chunketto.transform.Rotate(Vector3.up * 90f);
            }
            else if (scroll < 0)
            {
                if (89.5f <= chunketto.transform.rotation.eulerAngles.y && chunketto.transform.rotation.eulerAngles.y <= 90.5f)
                    chunketto.transform.Rotate(Vector3.up * -90f);
            }
        }

        // Spawning the initial chunk
        if (Input.GetMouseButtonDown(1) && curBlocks.Count == 0)
            InitialSpawn();
    }

    void OnGUI()
    {
        var style = new GUIStyle();
        style.normal.textColor = Color.red;
        GUI.Label(new Rect(10, 10, 300, 100), string.Format("Edit mode: {0}", editMode), style);
    }

    /// <summary>
    /// Called by the "Spawn chunk" button to create a new chunk
    /// </summary>
    public void InitialSpawn()
    {
        curBlocks = new List<GameObject>();
        curPointer = Instantiate(roadChunk);
        curPointer.GetComponent<GridSnapping>().enabled = true;
        curPointer.transform.position += Vector3.up * 1;
        curBlocks.Add(curPointer);
    }

    public void UpdateBlocks()
    {
        streetPointsToUpdate = new List<GameObject>();
        crossPointsToUpdate = new List<GameObject>();

        curBlocks = new List<GameObject>();
        var traces = GameObject.FindGameObjectsWithTag("Trace");
        foreach (GameObject g in traces)
        {
            Destroy(g);
            var curRoadChunk = Instantiate(roadChunk, g.transform.position - Vector3.up * g.transform.position.y, g.transform.rotation, chunkGarage.transform);
            StartCoroutine(DeleteIfColliding(curRoadChunk));
            curBlocks.Add(curRoadChunk);
            if (!allBlocks.Contains(curRoadChunk))
                allBlocks.Add(curRoadChunk);

            for (int i = 0; i < curRoadChunk.transform.childCount; i++)
                streetPointsToUpdate.Add(curRoadChunk.transform.GetChild(i).gameObject);
        }

        curPointer.transform.position -= Vector3.up;

        // Complete the road after the street is fully created
        StartCoroutine(CompleteRoad());

        // Cleaning
        curBlocks.Clear();
    }

    IEnumerator CompleteRoad()
    {
        yield return new WaitForFixedUpdate();
        foreach (GameObject block in allBlocks.ToArray())
        {
            if (block == null)
            {
                allBlocks.Remove(block);
                continue;
            }

            // Checking for the presence of a cross or streetpoint
            var colls = Physics.OverlapSphere(block.transform.position + Vector3.up * 5, 1f, LayerMask.GetMask("network"));
            bool stop = false;
            foreach (Collider c in colls)
                if (c.gameObject.tag == "crossPoint")
                    stop = true;
            if (stop)
                continue;

            // checking for crosses
            colls = Physics.OverlapSphere(block.transform.position, 0.1f, LayerMask.GetMask("street"));
            if (colls.Length >= 2)
            {
                // Destroying the chunks 
                foreach (Collider c in colls)
                {
                    for (int i = 0; i < c.gameObject.transform.childCount; i++)
                        streetPointsToUpdate.Remove(c.gameObject.transform.GetChild(i).gameObject);
                    Destroy(c.gameObject);
                }

                // Placing the cross prefab
                InstantiateCrossPoint(block);

                // Updating the nodes surrounding the cross
                colls = Physics.OverlapSphere(block.transform.position, 15f, LayerMask.GetMask("network"));
                for (int i = 0; i < colls.Length; i++)
                    if (colls[i].gameObject.tag == "streetPoint")
                        streetPointsToUpdate.Add(colls[i].gameObject);


                allBlocks.Remove(block);
            }

            allBlocks.Remove(block);
        }

        yield return new WaitForFixedUpdate();

        UpdateNetwork();

    }

    private void UpdateNetwork()
    {
        foreach (GameObject streetPoint in streetPointsToUpdate)
            if (streetPoint != null)
                FromStreetPointNodesCreation(streetPoint);
        foreach (GameObject crossPoint in crossPointsToUpdate)
            if (crossPoint != null)
                FromCrossNodesCreation(crossPoint);
    }


    IEnumerator DeleteIfColliding(GameObject curRoadChunk)
    {
        yield return new WaitForFixedUpdate();

        if (curBlocks.Count != 1 & curRoadChunk.GetComponent<CollisionChecking>().isColliding)
            Destroy(curRoadChunk);
    }


    private void InstantiateCrossPoint(GameObject block)
    {
        // Checking number and direction of other roads
        var leftColl = Physics.OverlapSphere(block.transform.position + Vector3.left * 14, 0.1f, LayerMask.GetMask("street"));
        var forwardColl = Physics.OverlapSphere(block.transform.position + Vector3.forward * 14, 0.1f, LayerMask.GetMask("street"));
        var rightColl = Physics.OverlapSphere(block.transform.position + Vector3.right * 14, 0.1f, LayerMask.GetMask("street"));
        var backColl = Physics.OverlapSphere(block.transform.position + Vector3.back * 14, 0.1f, LayerMask.GetMask("street"));

        GameObject cross = null;

        if (leftColl.Length == 1 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 1)
            cross = Instantiate(crossChunk, block.transform.position, Quaternion.identity, crossGarage.transform);

        if (leftColl.Length == 0 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 1)
            cross = Instantiate(leftCrossChunk, block.transform.position, Quaternion.identity, crossGarage.transform);

        if (leftColl.Length == 1 && rightColl.Length == 0 && forwardColl.Length == 1 && backColl.Length == 1)
            cross = Instantiate(rightCrossChunk, block.transform.position, Quaternion.identity, crossGarage.transform);

        if (leftColl.Length == 1 && rightColl.Length == 1 && forwardColl.Length == 0 && backColl.Length == 1)
            cross = Instantiate(leftCrossChunk, block.transform.position, Quaternion.Euler(0, 90, 0), crossGarage.transform);

        if (leftColl.Length == 1 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 0)
            cross = Instantiate(rightCrossChunk, block.transform.position, Quaternion.Euler(0, 90, 0), crossGarage.transform);

        //Curve
        // back-left
        if (leftColl.Length == 1 && rightColl.Length == 0 && forwardColl.Length == 0 && backColl.Length == 1)
            cross = Instantiate(rightCurve, block.transform.position, Quaternion.Euler(0, 180, 0), crossGarage.transform);
        //back-right
        if (leftColl.Length == 0 && rightColl.Length == 1 && forwardColl.Length == 0 && backColl.Length == 1)
            cross = Instantiate(leftCurve, block.transform.position, Quaternion.identity, crossGarage.transform);
        //forward-left
        if (leftColl.Length == 1 && rightColl.Length == 0 && forwardColl.Length == 1 && backColl.Length == 0)
            cross = Instantiate(leftCurve, block.transform.position, Quaternion.Euler(0, 180, 0), crossGarage.transform);
        //forward-right
        if (leftColl.Length == 0 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 0)
            cross = Instantiate(rightCurve, block.transform.position, Quaternion.identity, crossGarage.transform);

        // Adding the cross point 
        for (int i = 0; i < cross.transform.childCount; i++)
            crossPointsToUpdate.Add(cross.transform.GetChild(i).gameObject);
    }


    private void FromCrossNodesCreation(GameObject cross)
    {
        var curNode = cross.GetComponent<NodeHandler>().node;

        // all the four positions to check from a cross
        List<Vector3> checkPositions = new List<Vector3> {
            cross.transform.position + (cross.transform.forward * 10),
            cross.transform.position + (cross.transform.right  *  -7 ),
            cross.transform.position + (cross.transform.right  *  -7 - cross.transform.forward * 7),
        };
        if (cross.tag != "crossPoint")
        {
            checkPositions.RemoveAt(1);
            checkPositions.RemoveAt(1);
        }

        foreach (Vector3 checkPos in checkPositions)
        {
            CheckAtPositionForNodesFromCross(checkPos, curNode);
            Debug.DrawLine(cross.transform.position, checkPos, Color.blue, Mathf.Infinity);
        }

    }

    private void CheckAtPositionForNodesFromCross(Vector3 checkPos, NodeStreet curNode, float radius = 2f)
    {
        var colls = Physics.OverlapSphere(checkPos, radius, LayerMask.GetMask("network"));
        foreach (Collider c in colls)
        {
            var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
            var curStreet = new ArcStreet(curNode, nextNode);
            curNode.AddStreet(curStreet);
        }
    }


    private void FromStreetPointNodesCreation(GameObject streetPoint)
    {
        streetPoint.GetComponent<NodeHandler>().InitializeNode();
        var curNode = streetPoint.GetComponent<NodeHandler>().node;

        // Checking for nodes in front of the current node
        var colls = Physics.OverlapSphere(streetPoint.transform.position + (streetPoint.transform.forward.normalized * 14f), 2f, LayerMask.GetMask("network"));
        if (colls.Length > 0)
        {
            CheckAtPositionForNodesFromStreetPoint(colls, streetPoint, curNode);
        }
        else
        {
            colls = Physics.OverlapSphere(streetPoint.transform.position + (streetPoint.transform.forward.normalized * 10f), 2f, LayerMask.GetMask("network"));
            if (colls.Length > 0)
            {
                CheckAtPositionForNodesFromStreetPoint(colls, streetPoint, curNode);
            }
            else
            {   // I m at the end of the street since not nodes in front so I add to possibility to make a u turn
                colls = Physics.OverlapSphere(streetPoint.transform.position - 4 * streetPoint.transform.right, 3f, LayerMask.GetMask("network"));
                CheckAtPositionForNodesFromStreetPoint(colls, streetPoint, curNode);
            }
        }
        
    }

    private void CheckAtPositionForNodesFromStreetPoint(Collider[] colls, GameObject streetPoint, NodeStreet curNode)
    {
        foreach (Collider c in colls)
        {
            var nextNode = c.gameObject.GetComponent<NodeHandler>().node;
            var curStreet = new ArcStreet(curNode, nextNode);
            curNode.AddStreet(curStreet);
        }
    }


}



