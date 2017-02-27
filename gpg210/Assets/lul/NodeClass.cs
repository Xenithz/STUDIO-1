using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeClass
{

    public int xAxis;
    public int yAxis;

    public float hCost;
    public float gCost;

    public NodeClass parent;
    public Vector3 worldPostion;


    public NodeClass(int x, int y, float h, float g, NodeClass parent, Vector3 worldPos)
    {
        this.xAxis = x; 
        this.yAxis = y; 
        this.hCost = h; 
        this.gCost = g; 
        this.parent = parent; 
        this.worldPostion = worldPos; 
    }


    // Movement Calculation
    public virtual float fcost()
    {
        return hCost + gCost; 
    }


}
