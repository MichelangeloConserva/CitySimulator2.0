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

    void Start()
    {
        if (node == null)
            InitializeNode();

    }

    void Update()
    {
        foreach (ArcStreet a in node.availableStreets)
            DrawArrow.ForDebug(a.startNode.nodePosition + Vector3.up,
                                (a.arrivalNode.nodePosition + Vector3.up) - (a.startNode.nodePosition + Vector3.up),
                                Color.white);
    }

    public void UpdateNodePos(Vector3 pos)
    {
        node.nodePosition = pos - (Vector3.up * pos.y);
    }

}
