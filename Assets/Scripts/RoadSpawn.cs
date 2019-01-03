using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadSpawn : MonoBehaviour {

    [Header("Useful Settings")]
    public bool editMode;
    public int block = 0;

    [Header("Links required")]
    public GameObject roadChunk;
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
        {
            Destroy(curRoadChunk);
        }
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

            // Setting up the node in the position
            var pos = g.transform.position;
            net.nodeStreets.Add(new NodeStreet(pos));
            spheres.Add(Instantiate(sphere, pos + Vector3.up * 2f , Quaternion.identity));


            // checking for crosses
            var colls = Physics.OverlapSphere(g.transform.position, 0.1f, LayerMask.GetMask("street"));
            if (colls.Length >= 2)
            {
                
                // Destroying the chunks 
                foreach (Collider c in colls)
                    Destroy(c.gameObject);

                // Placing the cross prefab
                // TODO : create the cross prefab
                var curCross = Instantiate(roadChunk, pos, Quaternion.identity, crossGarage.transform);
                g.GetComponent<GridSnapping>().enabled = false;
                //g.GetComponent<BoxCollider>().enabled = false;
                g.GetComponent<CollisionChecking>().enabled = false;
            } else
            {
                // deactivating the components used in construction mode
                g.GetComponent<GridSnapping>().enabled = false;
                g.GetComponent<BoxCollider>().enabled = false;
                g.GetComponent<CollisionChecking>().enabled = false;

            }
        }
    }




}



