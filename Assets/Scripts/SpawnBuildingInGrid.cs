using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildingInGrid : MonoBehaviour
{
    private GridProperties grid;
    private RoadSpawn roadSpawn;

    [HideInInspector]
    public bool isDragging = true; 

    private void Awake()
    {
        {
            grid = FindObjectOfType<GridProperties>();
            roadSpawn = FindObjectOfType<RoadSpawn>();
        }
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

    void Update()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                GetNearestPointOnGrid(hit.point);
        }
    }

    private void OnMouseDown()
    {
        isDragging = false;
    }
}
