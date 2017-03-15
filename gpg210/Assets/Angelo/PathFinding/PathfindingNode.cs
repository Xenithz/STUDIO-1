using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode
{
    public int gCost;
    public int hCost;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public List<PathfindingNode> linkedNodes = new List<PathfindingNode>();

    public Transform nodeTransform;

    public PathfindingNode parent;

    public PathfindingNode(Transform setNodeTransform)
    {
        this.nodeTransform = setNodeTransform;
    }

    public void AddLinkedNode(PathfindingNode nodeToAdd)
    {
        this.linkedNodes.Add(nodeToAdd);
    }
}
