using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadSpawn : MonoBehaviour {

    [Header("Useful Settings")]
    [Space]
    public bool editMode;

    [Header("Links required")]
    [Space]
    public GameObject Cross;
    public GameObject StreetPoint;
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
    public float outLanesWidth = 4;
    public float innerLanesWidth = 1.8f;

    private GameObject curPointer;

    void Start()
    {
        allBlocks = new List<GameObject>();
        allCrosses = new List<GameObject>();
        spheres = new List<GameObject>();

        editMode = true;
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
        if (Input.GetMouseButtonDown(1) && curBlocks.Count==0)
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
        curPointer.transform.position += Vector3.up;
        curBlocks.Add(curPointer);
    }

    public void UpdateBlocks()
    {
        curBlocks = new List<GameObject>();
        var traces = GameObject.FindGameObjectsWithTag("Trace");
        foreach (GameObject g in traces)
        {
            Destroy(g);
            var curRoadChunk = Instantiate(roadChunk, g.transform.position, g.transform.rotation, chunkGarage.transform);
            StartCoroutine(DeleteIfColliding(curRoadChunk));
            curBlocks.Add(curRoadChunk);
            if (!allBlocks.Contains(curRoadChunk))
                allBlocks.Add(curRoadChunk);
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
        net.GetCompletedRoad();
    }

    IEnumerator DeleteIfColliding(GameObject curRoadChunk)
    {
        yield return new WaitForFixedUpdate();

        if (curBlocks.Count != 1 & curRoadChunk.GetComponent<CollisionChecking>().isColliding)
            Destroy(curRoadChunk);
    }

    public void CompleteRoadNetwork()
    {
        // exiting edit mode
        //editMode = false;


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
                var cross = Instantiate(Cross, block.transform.position + Vector3.up * 5f, Quaternion.identity, NetworkPoints.transform);
                cross.GetComponent<NodeHandler>().InitializeNode();

                // Destroying the chunks 
                foreach (Collider c in colls)
                    Destroy(c.gameObject);

                // Placing the cross prefab
                InstantiateCrossPoint(block);

                allBlocks.Remove(block);
            }
            else
            {
                // Vertical street
                Vector3 dir;
                if (block.transform.eulerAngles == Vector3.zero)
                {
                    dir = Vector3.right;
                    float[] angles = { 0f, 180f };
                    InstantiateStreetPoints(block, dir, angles);
                }
                else // Horizontal street
                {
                    dir = Vector3.forward;
                    float[] angles = { 270f, 90f };
                    InstantiateStreetPoints(block, dir, angles);
                }
            }
            

            allBlocks.Remove(block);
        }
    }

    private void InstantiateCrossPoint(GameObject block)
    {
        // Checking number and direction of other roads
        var leftColl = Physics.OverlapSphere(block.transform.position + Vector3.left * 14, 0.1f, LayerMask.GetMask("street"));
        var forwardColl = Physics.OverlapSphere(block.transform.position + Vector3.forward * 14, 0.1f, LayerMask.GetMask("street"));
        var rightColl = Physics.OverlapSphere(block.transform.position + Vector3.right * 14, 0.1f, LayerMask.GetMask("street"));
        var backColl = Physics.OverlapSphere(block.transform.position + Vector3.back * 14, 0.1f, LayerMask.GetMask("street"));

        if (leftColl.Length == 1 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 1)
            Instantiate(crossChunk, block.transform.position, Quaternion.identity, crossGarage.transform);

        if (leftColl.Length == 0 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 1)
            Instantiate(leftCrossChunk, block.transform.position, Quaternion.identity, crossGarage.transform);

        if (leftColl.Length == 1 && rightColl.Length == 0 && forwardColl.Length == 1 && backColl.Length == 1)
            Instantiate(rightCrossChunk, block.transform.position, Quaternion.identity, crossGarage.transform);

        if (leftColl.Length == 1 && rightColl.Length == 1 && forwardColl.Length == 0 && backColl.Length == 1)
            Instantiate(leftCrossChunk, block.transform.position, Quaternion.Euler(0, 90, 0), crossGarage.transform);

        if (leftColl.Length == 1 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 0)
            Instantiate(rightCrossChunk, block.transform.position, Quaternion.Euler(0, 90, 0), crossGarage.transform);

        //Curve
        // back-left
        if (leftColl.Length == 1 && rightColl.Length == 0 && forwardColl.Length == 0 && backColl.Length == 1)
            Instantiate(rightCurve, block.transform.position, Quaternion.Euler(0, 180, 0), crossGarage.transform);
        //back-right
        if (leftColl.Length == 0 && rightColl.Length == 1 && forwardColl.Length == 0 && backColl.Length == 1)
            Instantiate(leftCurve, block.transform.position, Quaternion.identity, crossGarage.transform);
        //forward-left
        if (leftColl.Length == 1 && rightColl.Length == 0 && forwardColl.Length == 1 && backColl.Length == 0)
            Instantiate(leftCurve, block.transform.position, Quaternion.Euler(0, 180, 0), crossGarage.transform);
        //forward-right
        if (leftColl.Length == 0 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 0)
            Instantiate(rightCurve, block.transform.position, Quaternion.identity, crossGarage.transform);
    }

    private void InstantiateStreetPoints(GameObject block, Vector3 dir, float[] angles)
    {
        var lane1 = Instantiate(StreetPoint, block.transform.position + Vector3.up * 5f + dir * outLanesWidth, Quaternion.identity, NetworkPoints.transform);
        lane1.transform.Rotate(Vector3.up * angles[0]);
        lane1.GetComponent<NodeHandler>().InitializeNode();

        var lane2 = Instantiate(StreetPoint, block.transform.position + Vector3.up * 5f + dir * innerLanesWidth, Quaternion.identity, NetworkPoints.transform);
        lane2.transform.Rotate(Vector3.up * angles[0]);
        lane2.GetComponent<NodeHandler>().InitializeNode();

        var lane3 = Instantiate(StreetPoint, block.transform.position + Vector3.up * 5f - dir * outLanesWidth, Quaternion.identity, NetworkPoints.transform);
        lane3.transform.Rotate(Vector3.up * angles[1]);
        lane3.GetComponent<NodeHandler>().InitializeNode();

        var lane4 = Instantiate(StreetPoint, block.transform.position + Vector3.up * 5f - dir * innerLanesWidth, Quaternion.identity, NetworkPoints.transform);
        lane4.transform.Rotate(Vector3.up * angles[1]);
        lane4.GetComponent<NodeHandler>().InitializeNode();
    }
}



