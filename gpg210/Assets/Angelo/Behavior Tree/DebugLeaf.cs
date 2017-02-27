using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLeaf : Node
{
    public DebugLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        BoolCheckNodeState(NodeStates.success);
    }
}
