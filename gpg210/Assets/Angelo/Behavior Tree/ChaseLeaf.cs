using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLeaf : Node
{
    public ChaseLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        //Continously update the speed
        this.NodeHandler.RotationSpeed = 3f * Time.deltaTime;

        //Create a new quaternion with the specified direction
        Quaternion lookAt = Quaternion.LookRotation(this.NodeHandler.DirectionBetweenEnemyAndPlayer);

        this.NodeHandler.transform.rotation = Quaternion.Lerp(this.NodeHandler.transform.rotation, lookAt, this.NodeHandler.RotationSpeed);

        this.NodeHandler.transform.position = Vector3.MoveTowards(this.NodeHandler.transform.position, this.NodeHandler.Player.transform.position, 0.3f * Time.deltaTime);

        SetNodeStatus(NodeStates.running);

        if (this.NodeHandler.DistanceToPlayer <= 2f)
        {
            SetNodeStatus(NodeStates.success);
        }
    }
}
