using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLeaf : Node
{
    /*
        The chase leaf class will be used to make the A.I chase the player 
    */

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

        //Lerp the rotation of the NodeHandler towards the lookAt quaternion using the specified RotationSpeed
        this.NodeHandler.transform.rotation = Quaternion.Lerp(this.NodeHandler.transform.rotation, lookAt, this.NodeHandler.RotationSpeed);

        //PLACEHOLDER MOVEMENT
        this.NodeHandler.transform.position = Vector3.MoveTowards(this.NodeHandler.transform.position, this.NodeHandler.Player.transform.position, 0.3f * Time.deltaTime);

        //Set the node status to running
        SetNodeStatus(NodeStates.running);

        //Check if the distance to the player is under 2f
        if (this.NodeHandler.DistanceToPlayer <= 2f)
        {
            //Set the nodestatus to success
            SetNodeStatus(NodeStates.success);
        }
    }
}
