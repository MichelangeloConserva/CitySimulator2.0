﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeStreet {

    public List<ArcStreet> availableStreets;
    public Vector3 nodePosition;
    public NodeStreet parent;

    public float gCost;
    public float hCost;
    public float fCost => gCost + hCost;


    public NodeStreet(Vector3 nodePosition)
    {
        availableStreets = new List<ArcStreet>();

        this.nodePosition = nodePosition;
    }

    public void AddStreet(ArcStreet street)
    {
        availableStreets.Add(street);
    }

    public void RemoveStreet(ArcStreet street)
    {
        availableStreets.Remove(street);
    }


    // A* START
    public Dictionary<NodeStreet,float> GetNeighborsAndDistance()
    {
        var list = new Dictionary<NodeStreet, float>();
        foreach (ArcStreet a in availableStreets)
        {
            list.Add(a.arrivalNode, a.lenght);
        }
         
        return list;
    }
    // A* END

    


}
