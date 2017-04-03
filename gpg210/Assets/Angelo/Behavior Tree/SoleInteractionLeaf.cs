using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoleInteractionLeaf : Node
{
    public SoleInteractionLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        if(this.NodeHandler.ScreamAndRun == false)
        {
            SetNodeStatus(NodeStates.running);
        }

        else if(this.NodeHandler.ScreamAndRun == true)
        {
            SetNodeStatus(NodeStates.success);
        }
    }
}
