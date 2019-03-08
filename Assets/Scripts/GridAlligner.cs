using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAlligner : MonoBehaviour
{
    void Start()
    {
        transform.position = Utils.GetNearestPointOnGrid(transform.position, transform.position.y);
    }
}