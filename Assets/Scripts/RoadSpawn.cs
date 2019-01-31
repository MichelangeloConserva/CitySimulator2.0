using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadSpawn : MonoBehaviour {

    [Header("Useful Settings")]
    public bool editMode;
    public int block = 0;

    [Header("Links required")]
    public GameObject Cross;
    public GameObject StreetPoint;
    public GameObject NetworkPoints;
    public GameObject roadChunk;
    public GameObject crossChunk;
    public GameObject leftCrossChunk;
    public GameObject rightCrossChunk;
    public GameObject sphere;
    public Button chunkSpawner;
    public Button networkCompleter;
    public Network net;
    public GameObject chunkGarage;
    public GameObject crossGarage;

    [Header("Storage for the network")]
    public List<GameObject> allBlocks;
    public List<GameObject> allCrosses;
    public List<GameObject> curBlocks;
    public List<GameObject> spheres;




    float outLanesWidth = 4;
    float innerLanesWidth = 1.8f;



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

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (-0.5f <= chunketto.transform.rotation.eulerAngles.y && chunketto.transform.rotation.y <= 0.5f)
                    chunketto.transform.Rotate(Vector3.up * 90f);
                else if (89.5f <= chunketto.transform.rotation.eulerAngles.y && chunketto.transform.rotation.eulerAngles.y <= 90.5f)
                    chunketto.transform.Rotate(Vector3.up * -90f);
            }

            //chunketto.GetComponent<BoxCollider>().enabled = false;
            //var coll = Physics.OverlapSphere(chunketto.transform.position, 2f, LayerMask.GetMask("street"));
            //if (coll.Length != 0 && chunketto.transform.position != Vector3.zero)
            //    Debug.Log(coll[0].gameObject.name);
            //chunketto.GetComponent<BoxCollider>().enabled = true;
        }
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
        var cur = Instantiate(roadChunk);
        cur.GetComponent<GridSnapping>().enabled = true;
        curBlocks.Add(cur);
    }

    public void UpdateBlocks()
    {
        curBlocks = new List<GameObject>();
        var traces = GameObject.FindGameObjectsWithTag("Trace");
        foreach (GameObject g in traces)
        {
            var curTransform = g.transform;
            Destroy(g);
            var curRoadChunk = Instantiate(roadChunk, curTransform.position, curTransform.rotation, chunkGarage.transform);
            StartCoroutine(DeleteIfColliding(curRoadChunk, block));
            curBlocks.Add(curRoadChunk);
            if (!allBlocks.Contains(curRoadChunk))
                allBlocks.Add(curRoadChunk);
        }
    }

    IEnumerator DeleteIfColliding(GameObject curRoadChunk, int curBlock)
    {
        yield return new WaitForFixedUpdate();

        if (curBlocks.Count != 1 & curRoadChunk.GetComponent<CollisionChecking>().isColliding)
            Destroy(curRoadChunk);
    }

    public void CompleteRoadNetwork()
    {
        // exiting edit mode
        editMode = false;
        chunkSpawner.interactable = false;
        networkCompleter.interactable = false;


        foreach (GameObject g in allBlocks.ToArray())
        {
            if (g == null)
            {
                allBlocks.Remove(g);
                continue;
            }

            // checking for crosses
            var colls = Physics.OverlapSphere(g.transform.position, 0.1f, LayerMask.GetMask("street"));
            if (colls.Length >= 2)
            {
                var cross = Instantiate(Cross, g.transform.position + Vector3.up * 5f, Quaternion.identity, NetworkPoints.transform);
                cross.GetComponent<NodeHandler>().InitializeNode();

                // Destroying the chunks 
                foreach (Collider c in colls)
                {
                    allBlocks.Remove(c.gameObject);
                    Destroy(c.gameObject);
                }
                allBlocks.Remove(g);


                // Placing the cross prefab
                // Checking number and direction of other roads
                var leftColl = Physics.OverlapSphere(g.transform.position + Vector3.left * 14, 0.1f, LayerMask.GetMask("street"));
                var forwardColl = Physics.OverlapSphere(g.transform.position + Vector3.forward * 14, 0.1f, LayerMask.GetMask("street"));
                var rightColl = Physics.OverlapSphere(g.transform.position + Vector3.right * 14, 0.1f, LayerMask.GetMask("street"));
                var backColl = Physics.OverlapSphere(g.transform.position + Vector3.back * 14, 0.1f, LayerMask.GetMask("street"));

                if (leftColl.Length == 1 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 1)
                {
                    var curCross = Instantiate(crossChunk, g.transform.position, Quaternion.identity, crossGarage.transform);
                }

                if (leftColl.Length == 0 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 1)
                {
                    var leftCross = Instantiate(leftCrossChunk, g.transform.position, Quaternion.identity,crossGarage.transform);
                }

                if (leftColl.Length == 1 && rightColl.Length == 0 && forwardColl.Length == 1 && backColl.Length == 1)
                {
                    var leftCross = Instantiate(rightCrossChunk, g.transform.position, Quaternion.identity, crossGarage.transform);
                }

                if (leftColl.Length == 1 && rightColl.Length == 1 && forwardColl.Length == 0 && backColl.Length == 1)
                {
                    var leftCross = Instantiate(leftCrossChunk, g.transform.position, Quaternion.Euler(0,90,0), crossGarage.transform);
                }

                if (leftColl.Length == 1 && rightColl.Length == 1 && forwardColl.Length == 1 && backColl.Length == 0)
                {
                    var leftCross = Instantiate(rightCrossChunk, g.transform.position, Quaternion.Euler(0, 90, 0), crossGarage.transform);
                }
            } else
            {
                // Vertical street
                Vector3 dir;
                if (g.transform.eulerAngles == Vector3.zero)
                {
                    dir = Vector3.right;
                    var lane1 = Instantiate(StreetPoint, g.transform.position + Vector3.up * 5f + dir * outLanesWidth, Quaternion.identity, NetworkPoints.transform);
                    lane1.GetComponent<NodeHandler>().InitializeNode();

                    var lane2 = Instantiate(StreetPoint, g.transform.position + Vector3.up * 5f + dir * innerLanesWidth, Quaternion.identity, NetworkPoints.transform);
                    lane2.GetComponent<NodeHandler>().InitializeNode();

                    var lane3 = Instantiate(StreetPoint, g.transform.position + Vector3.up * 5f - dir * outLanesWidth, Quaternion.identity, NetworkPoints.transform);
                    lane3.transform.Rotate(Vector3.up * -180f);
                    lane3.GetComponent<NodeHandler>().InitializeNode();

                    var lane4 = Instantiate(StreetPoint, g.transform.position + Vector3.up * 5f - dir * innerLanesWidth, Quaternion.identity, NetworkPoints.transform);
                    lane4.transform.Rotate(Vector3.up * -180f);
                    lane4.GetComponent<NodeHandler>().InitializeNode();

                }
                else // Horizontal street
                {
                    dir = Vector3.forward;

                    var lane1 = Instantiate(StreetPoint, g.transform.position + Vector3.up * 5f + dir * outLanesWidth, Quaternion.identity, NetworkPoints.transform);
                    lane1.transform.Rotate(Vector3.up * 270f);
                    lane1.GetComponent<NodeHandler>().InitializeNode();

                    var lane2 = Instantiate(StreetPoint, g.transform.position + Vector3.up * 5f + dir * innerLanesWidth, Quaternion.identity, NetworkPoints.transform);
                    lane2.transform.Rotate(Vector3.up * 270f);
                    lane2.GetComponent<NodeHandler>().InitializeNode();

                    var lane3 = Instantiate(StreetPoint, g.transform.position + Vector3.up * 5f - dir * outLanesWidth, Quaternion.identity, NetworkPoints.transform);
                    lane3.transform.Rotate(Vector3.up * 90f);
                    lane3.GetComponent<NodeHandler>().InitializeNode();

                    var lane4 = Instantiate(StreetPoint, g.transform.position + Vector3.up * 5f - dir * innerLanesWidth, Quaternion.identity, NetworkPoints.transform);
                    lane4.transform.Rotate(Vector3.up * 90f);
                    lane4.GetComponent<NodeHandler>().InitializeNode();

                }

                // deactivating the components used in construction mode
                g.GetComponent<GridSnapping>().enabled = false;
                //g.GetComponent<BoxCollider>().enabled = false;
                g.GetComponent<CollisionChecking>().enabled = false;

            }
        }
    }
}



