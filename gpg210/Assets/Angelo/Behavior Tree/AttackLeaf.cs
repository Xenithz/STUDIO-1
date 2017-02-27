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
    }

    public override void NodeBehavior(Handler agent, bool isInTrigger, Collider doorCollider, float doorOpenTimer, GameObject player, float distanceToPlayer, float angle, float enemyFieldOfView, float rotationSpeed, Vector3 directionBetweenEnemyAndPlayer)
    {
        if(distanceToPlayer < 2f)
        {
            player.GetComponent<PlayerControl>().Die();
            base.BoolCheckNodeState(NodeStates.success);
        }
        else
        {
            base.BoolCheckNodeState(NodeStates.failed);
            return;
        }
    }
}
