using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLeaf : Node
{
    /*
         
    */

    public PatrolLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
    }

    public override void NodeBehavior(Handler agent, bool isInTrigger, Collider doorCollider, float doorOpenTimer, GameObject player, float distanceToPlayer, float angle, float enemyFieldOfView, float rotationSpeed, Vector3 directionBetweenEnemyAndPlayer)
    {

    }
}
