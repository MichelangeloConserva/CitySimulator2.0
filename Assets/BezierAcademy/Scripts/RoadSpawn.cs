using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadSpawn : MonoBehaviour {

    [Header("Settings for crossroads")]
    public int pointsToDelete;
    public List<Vector3> crossPoints = new List<Vector3>();
    public SpawningCross spawningCross;

    public bool isEditing = true;
    public GameObject anchorPoint;
    public GameObject ducetto;
    public Button roadButton;
    List<GameObject> anchorList;
    LineRenderer lineRenderer;
    PathCreatorAndSettings pathCreator;
    RoadProceduralMeshCreator roadCreator;


    private float snapSpacing = 1f;
    private BoxCollider bCol;

    public GameObject snapPoint;            //sferette rosse
    public List<GameObject> snapPointList = new List<GameObject>();


    // to get back in the network creation
    public List<GameObject> backWaypoints;
    public bool back = false;




    private void Start()
    {
        anchorList = new List<GameObject>();
        lineRenderer = GetComponent<LineRenderer>();
        pathCreator = GetComponent<PathCreatorAndSettings>();
        roadCreator = GetComponent<RoadProceduralMeshCreator>();
        bCol = GetComponent<BoxCollider>();
        spawningCross = GameObject.FindGameObjectWithTag("manager").GetComponent<SpawningCross>();
        SpawnAnchor();
    }

    private void Update()
    {
        if (isEditing)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
            {
                SpawnAnchor();
            }

            if (anchorList.Count > 1)
            {
                for (int j = 0; j < anchorList.Count; j++)
                {
                    Vector3 adjustedPos = new Vector3(anchorList[j].transform.position.x, 0, anchorList[j].transform.position.z);
                    pathCreator.path.MovePoint(j * 3, adjustedPos);
                    roadCreator.UpdateRoad();

                }
            }
        }

        if (bCol.enabled)
            bCol.center = anchorList[0].transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sferetta" && anchorList.Count == 1)
        {

            StartCoroutine(SnapStay(other));
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "sferetta" && anchorList.Count == 1)
        {

            var cur = col.gameObject.GetComponentInParent<RoadSpawn>().snapPointList;
            if (Input.GetMouseButton(0) && (col.gameObject.Equals(cur[0]) || col.gameObject.Equals(cur[cur.Count - 1])))
            {
                col.gameObject.GetComponentInParent<RoadSpawn>().OnEnableEditing();

                Destroy(this.gameObject);
            }

            

        }

    }
    //roadCreator.roadMesh.vertices[((j + 1) * 4) - 1];


    public void CrossRoadFinder(GameObject otherRoad, GameObject sferettaDiCollisione)
    {
        /*Debug.DrawLine(otherRoad.GetComponent<RoadProceduralMeshCreator>().roadMesh.vertices[0], 
            otherRoad.GetComponent<RoadProceduralMeshCreator>().roadMesh.vertices[otherRoad.GetComponent<RoadProceduralMeshCreator>().roadMesh.vertices.Length - 1],
            Color.red, Mathf.Infinity);*/
        float curDistance = 0;
        float oldDistance = Mathf.Infinity;

        for (int i = 0; i < roadCreator.points.Length; i++)
        {
            curDistance = Vector3.Distance(roadCreator.points[i], sferettaDiCollisione.transform.position);
            if ((curDistance > oldDistance))
            {
                for (int j = i - pointsToDelete; j <= i + pointsToDelete; j++)
                {
                    roadCreator.pointsToDelete.Add(j);
                }
                if (0 <= (((i - pointsToDelete) + 1) * 4) - 4)
                {
                    crossPoints.Add(roadCreator.roadMesh.vertices[(((i - pointsToDelete) + 1) * 4) - 4]);
                    crossPoints.Add(roadCreator.roadMesh.vertices[(((i - pointsToDelete) + 1) * 4) - 3]);
                    crossPoints.Add(roadCreator.roadMesh.vertices[(((i - pointsToDelete) + 1) * 4) - 2]);
                    crossPoints.Add(roadCreator.roadMesh.vertices[(((i - pointsToDelete) + 1) * 4) - 1]);
                }

                if (roadCreator.roadMesh.vertices.Length > ((((i + pointsToDelete) + 2) * 4) - 1))
                {
                    crossPoints.Add(roadCreator.roadMesh.vertices[(((i + pointsToDelete) + 2) * 4) - 4]);
                    crossPoints.Add(roadCreator.roadMesh.vertices[(((i + pointsToDelete) + 2) * 4) - 3]);
                    crossPoints.Add(roadCreator.roadMesh.vertices[(((i + pointsToDelete) + 2) * 4) - 2]);
                    crossPoints.Add(roadCreator.roadMesh.vertices[(((i + pointsToDelete) + 2) * 4) - 1]);

                }
                break;

            }
            else
            {
                oldDistance = curDistance;
            }
        }

        roadCreator.UpdateRoad();
        spawningCross.CrossroadCreator(this.gameObject);
    }



    public void OnDisableEditing()  
    {
        //Disabilitiamo gli anchorpoints
        foreach (GameObject g in anchorList)
            g.SetActive(false);

        //Disabilitiamo l'editing
        isEditing = false;

        Vector3[] snapMask = pathCreator.path.CalculateEvenlySpacedPoints(snapSpacing);
        if (snapPointList.Count > 0) { return; }
        foreach (Vector3 p in snapMask)
            snapPointList.Add(Instantiate(snapPoint, p + new Vector3(0,0.2f,0), Quaternion.identity, transform));

    }

    public void OnEnableEditing()
    {
        //Abilitiamo gli anchorpoints
        foreach (GameObject g in anchorList)
            g.SetActive(true);

        //eliminiamo la snap Mask
        foreach (GameObject g in snapPointList)
            Destroy(g);
        snapPointList.Clear();

        //abilitiamo l'editing
        isEditing = true;


    }


    public void DisableSnaps()
    {
        foreach (GameObject g in snapPointList)
        {
            g.GetComponent<SphereCollider>().enabled = false;
            g.GetComponent<MeshRenderer>().enabled = false;
            g.GetComponent<IsCollidingScript>().enabled = false;
        }
    }




    public void cleanList()
    {
        lineRenderer.positionCount = 0;
        anchorList.Clear();
    }


    public void SpawnAnchor()
    {
        
        var cur = Instantiate(anchorPoint,transform);
        anchorList.Add(cur);

        if (anchorList.Count == 1)
            StartCoroutine(FirstSpawn());
        else if (anchorList.Count > 1)
        {
            pathCreator.path.AddSegment(cur.transform.position);
        }
    }


    public void RemoveWaypoint(int index)
    {
        if (index+1 < snapPointList.Count)
        {
            Destroy(snapPointList[index]);
            snapPointList.RemoveAt(index);
        }
    }

    public GameObject NextWaypoint(GameObject waypoint)
    {
        for (int i = 0; i < snapPointList.Count-1; i++)  // TODO : Handle this in a better way
            if (snapPointList[i] == waypoint)
                return snapPointList[i + 1];

        return null;
    }

    public void FillBack()
    {
        for (int i = snapPointList.Count - 1; i >= 0; i--)
            snapPointList.Add(snapPointList[i]);
    }







    IEnumerator FirstSpawn()
    {
        if (transform.GetChild(1).GetComponent<DragMovement>().IsDragging)
        {

            yield return new WaitForFixedUpdate();
        }
        else
        {
            var cur = new Vector3(anchorList[0].transform.position.x, 0, anchorList[0].transform.position.z);
            pathCreator.CreatePath(cur);
            //SpawnAnchor();
            yield return null;
        }
    }

    IEnumerator SnapStay(Collider col)
    {
        while (anchorList[0].GetComponent<DragMovement>().IsDragging)
        {
            anchorList[0].transform.position = col.transform.position;
            RaycastHit hit;
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast, out hit, 100.0f))
                if (Vector3.Distance(hit.point, col.transform.position) < 1f)
                {
                    anchorList[0].GetComponent<DragMovement>().IsSnapping = true;
                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    anchorList[0].GetComponent<DragMovement>().IsSnapping = false;
                    anchorList[0].transform.position = hit.point;

                    break;                
                }
            
        }
        yield return null;
    }
    



}
