using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHandler : MonoBehaviour
{
    public NodeStreet node;

    public int conn;

    public void InitializeNode()
    {
        if (node == null)
            node = new NodeStreet(transform.position - (Vector3.up * transform.position.y));
        else
        {
            node.nodePosition = transform.position - (Vector3.up * transform.position.y);
            node.availableStreets.Clear();
        }
    }

    void Start()
    {
        if (node == null)
            InitializeNode();
    }

    void Update()
    {
        conn = node.availableStreets.Count;
        for (int i=0; i<conn; i++)
            DrawArrow.ForDebug(node.availableStreets[i].startNode.nodePosition + Vector3.up,
                    (node.availableStreets[i].arrivalNode.nodePosition + Vector3.up*(i+1)) - (node.availableStreets[i].startNode.nodePosition + Vector3.up),
                    Color.white);

    }

    public void UpdateNodePos(Vector3 pos)
    {
        node.nodePosition = pos - (Vector3.up * pos.y);
    }


    public void UpdateConnections()
    {

    }


}
