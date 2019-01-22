using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossNodeHandler : MonoBehaviour
{
    public NodeStreet crossNode;

    public int conn;

    public void InitializeNode()
    {
        crossNode = new NodeStreet(transform.position - (Vector3.up * transform.position.y));
    }

    void Update()
    {
        conn = crossNode.availableStreets.Count;
    }

}
