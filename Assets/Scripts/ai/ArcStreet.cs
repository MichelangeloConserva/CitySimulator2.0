using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcStreet {

    public NodeStreet startNode;
    public NodeStreet arrivalNode;

    public float lenght
    {
        get
        {
            return Vector3.Distance(startNode.nodePosition, arrivalNode.nodePosition);
        }
    }

    public ArcStreet(NodeStreet startNode, NodeStreet arrivalNode)
    {
        this.startNode = startNode;
        this.arrivalNode = arrivalNode;
    }

    public void RemoveNode(int index)
    {
        arrivalNode = null;
    }




}
