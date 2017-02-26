using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLeaf : Node
{
    public ChaseLeaf(int desiredPriority, string desiredName) : base(desiredPriority, desiredName)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
    }

    public override void NodeBehavior(Handler agent, bool isInTrigger, Collider doorCollider, float doorOpenTimer, GameObject player, float distanceToPlayer)
    {
        
    }
}
