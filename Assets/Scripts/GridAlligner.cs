using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAlligner : MonoBehaviour
{

    // Testing
    private GridProperties grid;
    public Vector3 GetNearestPointOnGrid(Vector3 pos)
    {
        grid = FindObjectOfType<GridProperties>();

        int xCount = Mathf.RoundToInt(pos.x / grid.gridSize);
        int zCount = Mathf.RoundToInt(pos.z / grid.gridSize);

        Vector3 result = new Vector3(
            (float)xCount * grid.gridSize,
            transform.position.y,
            (float)zCount * grid.gridSize
            );

        return result;
    }


    void Start()
    {
        transform.position = GetNearestPointOnGrid(transform.position);
    }


}
