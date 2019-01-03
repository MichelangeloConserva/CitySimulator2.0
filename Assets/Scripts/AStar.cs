﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar {

    public NodeStreet startNode;
    public NodeStreet endNode;

    public List<NodeStreet> openedNodes;
    public HashSet<NodeStreet> closedNodes;

    public List<NodeStreet> path;

    public AStar(NodeStreet startNode, NodeStreet endNode)
    {
        // Initialize the set of nodes and the path
        openedNodes = new List<NodeStreet>();
        closedNodes = new HashSet<NodeStreet>();
        path = new List<NodeStreet>();

        // Setting a reference for the start and the end node
        this.startNode = startNode;
        this.endNode = endNode;

        // adding the start node to the opened ones list
        openedNodes.Add(startNode);
    }

    public bool PathFinder()
    {
        while (openedNodes.Count > 0)
        {
            
            // Finding the best node from all the opened nodes
            NodeStreet currentNode = openedNodes[0];
            for (int i=1; i<openedNodes.Count; i++)
            {
                var node = openedNodes[i];
                if (node.fCost < currentNode.fCost | (node.fCost == currentNode.fCost && node.hCost < currentNode.hCost))
                    currentNode = node;
            }
            openedNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            // cheching for the finding path condition
            if (currentNode.Equals(endNode))
            {
                RetracePath();
                return true;
            }
                

            // Analyzing neighbors
            var neighbours = currentNode.GetNeighbors();
            foreach (NodeStreet neighbour in neighbours)
            {

                // the neighbor has already been explored
                if (closedNodes.Contains(neighbour))
                    continue;

                float newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost | !openedNodes.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if (!openedNodes.Contains(neighbour))
                        openedNodes.Add(neighbour);
                }

            }

        }
        return false;
    }

    private void RetracePath()
    {
        var curr = endNode;

        while (!curr.Equals(startNode))
        {
            path.Add(curr);
            curr = curr.parent;
        }
        path.Reverse();
    }

    private float GetDistance(NodeStreet a, NodeStreet b)
    {

        // Checking if nodes are connected
        ArcStreet connectingStreet = null;
        bool isConnected = false;
        foreach(ArcStreet fromA in a.availableStreets)
        {
            if (fromA.arrivalNode.Equals(b))
            {
                isConnected = true;
                connectingStreet = fromA;
                break;
            }
        }
        if (!isConnected)
            return Vector3.Distance(a.nodePosition,b.nodePosition);
            //return Mathf.Infinity;

        return connectingStreet.lenght;
    }



}
