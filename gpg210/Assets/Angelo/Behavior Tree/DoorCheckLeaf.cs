using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheckLeaf : Node
{
    public DoorCheckLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {

    }
}
