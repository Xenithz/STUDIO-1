using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLeaf : Node
{
    public ChaseLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
    }

    public override void NodeBehavior(Handler agent, bool isInTrigger, Collider doorCollider, float doorOpenTimer, GameObject player, float distanceToPlayer, float angle, float enemyFieldOfView, float rotationSpeed, Vector3 directionBetweenEnemyAndPlayer)
    {
        //#TODO Create object in node which allows access to the handler class
        if(angle < enemyFieldOfView * 0.5)
        {

        }
    }
}
