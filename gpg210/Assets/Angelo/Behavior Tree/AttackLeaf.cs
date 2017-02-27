using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLeaf : Node
{
    /*
         
    */

    public AttackLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        if(this.NodeHandler.DistanceToPlayer < 2f)
        {
            this.NodeHandler.Player.GetComponent<PlayerControl>().Die();
            base.BoolCheckNodeState(NodeStates.success);
        }
        else
        {
            base.BoolCheckNodeState(NodeStates.failed);
            return;
        }
    }
}
