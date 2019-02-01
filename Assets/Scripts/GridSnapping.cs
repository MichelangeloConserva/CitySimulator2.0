using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnapping : MonoBehaviour {


    public bool isDragging;
    public bool isColliding;
    public GridProperties grid;
    public RoadSpawn roadSpawn;
    Quaternion rotate90Degrees;
    public GameObject roadChunkTrace;



	// Use this for initialization
	void Awake ()
    {
        grid = FindObjectOfType<GridProperties>();
        roadSpawn = FindObjectOfType<RoadSpawn>();
        rotate90Degrees = Quaternion.Euler(0, 90, 0);
	}

    public void GetNearestPointOnGrid(Vector3 mousePos)
    {
        int xCount = Mathf.RoundToInt(mousePos.x / grid.gridSize);
        int zCount = Mathf.RoundToInt(mousePos.z / grid.gridSize);

        Vector3 result = new Vector3(
            (float)xCount * grid.gridSize,
            transform.position.y,
            (float)zCount * grid.gridSize
            );

        transform.position = result;
    }
    public Vector3 GetNearestPointOnGrid(float dist, bool isVertical)
    {
        if (isVertical)
        {
            int Count = Mathf.RoundToInt(dist / grid.gridSize);

            Vector3 result = new Vector3(
                transform.position.x,
                transform.position.y,
                (float)Count * grid.gridSize
                );
            return result;
        }
        else
        {
            int Count = Mathf.RoundToInt(dist / grid.gridSize);

            Vector3 result = new Vector3(
                (float)Count * grid.gridSize,
                transform.position.y,
                transform.position.z
                );
            return result;
        }
    }

    private void OnMouseDown()
    {
        if (isDragging == true)
            isDragging = false;

        //StartCoroutine(StopDragging());
    }

    private bool IsVertical()
    {
        if (Mathf.FloorToInt(transform.rotation.eulerAngles.y + 0.5f) == 0 || Mathf.FloorToInt(transform.rotation.eulerAngles.y + 0.5f) == 180)
            return true;
        else
            return false;
    }

    private void OnMouseDrag()
    {
        if (IsVertical())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                var curToDelete = GameObject.FindGameObjectsWithTag("Trace");
                foreach (GameObject g in curToDelete)
                    Destroy(g);

                int cur = -1;
                if (hit.point.z - transform.position.z > 0)
                    cur = 1;

                for(int j=0; j < Mathf.Abs(hit.point.z - transform.position.z); j+=Mathf.RoundToInt(grid.gridSize) )
                    Instantiate<GameObject>(roadChunkTrace,GetNearestPointOnGrid(transform.position.z + j * cur, IsVertical()), transform.rotation, this.transform);
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                var curToDelete = GameObject.FindGameObjectsWithTag("Trace");
                foreach (GameObject g in curToDelete)
                    Destroy(g);

                int cur = -1;
                if (hit.point.x - transform.position.x > 0)
                    cur = 1;

                for (int j = 0; j < Mathf.Abs(hit.point.x - transform.position.x); j += Mathf.RoundToInt(grid.gridSize))
                    Instantiate<GameObject>(roadChunkTrace, GetNearestPointOnGrid(transform.position.x + j * cur, IsVertical()), transform.rotation, this.transform);
            }
        }
    }

    private void OnMouseUp()
    {
        roadSpawn.UpdateBlocks();
        Destroy(this.gameObject);
    }

    void Update ()
    {
		if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                GetNearestPointOnGrid(hit.point);
            //if (Input.GetMouseButtonDown(1))
            //    transform.Rotate(0, 90, 0);
        }
	}
}
