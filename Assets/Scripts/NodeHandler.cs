using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHandler : MonoBehaviour
{
    public NodeStreet node;

    public int conn;

    public void InitializeNode()
    {
        node = new NodeStreet(transform.position - (Vector3.up * transform.position.y));
    }

    void Update()
    {
        conn = node.availableStreets.Count;
    }
}
