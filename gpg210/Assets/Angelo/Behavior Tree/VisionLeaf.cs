using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionLeaf : Node
{
    /*
        
    */

    public VisionLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        //Calculate the magnitude and then normalize to get the direction
        this.NodeHandler.DirectionBetweenEnemyAndPlayer = (this.NodeHandler.Player.transform.position - this.NodeHandler.transform.position).normalized;

        //calculate the angle between the direction of the agent to the target, and the forward direction of the agent
        this.NodeHandler.Angle = Vector3.Angle(this.NodeHandler.DirectionBetweenEnemyAndPlayer, this.NodeHandler.transform.forward);

        //Set the distance of the player and update accordingly
        this.NodeHandler.DistanceToPlayer = Vector3.Distance(this.NodeHandler.gameObject.transform.position, this.NodeHandler.Player.transform.position);

        if (this.NodeHandler.Angle < this.NodeHandler.EnemyFieldOfView * 0.5)
        {
            Debug.Log("NOW MOVE");
            SetNodeStatus(NodeStates.success);
        }
        else
        {
            SetNodeStatus(NodeStates.running);
        }
    }
}
