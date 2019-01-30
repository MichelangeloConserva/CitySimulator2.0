using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcStreet {

    public NodeStreet arrivalNode;

    public Vector3 beginPos;
    public Vector3 endPos;

    public float lenght;

    public ArcStreet(Vector3 beginPos, Vector3 endPos)
    {
        this.beginPos = beginPos;
        this.endPos = endPos;

        lenght = Vector3.Distance(beginPos, endPos);
    }

    public void AddNode(NodeStreet node)
    {
        arrivalNode = node;
    }

    public void RemoveNode(int index)
    {
        arrivalNode = null;
    }




}
