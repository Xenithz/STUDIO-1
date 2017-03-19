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
        if (this.NodeHandler.AgentHasSightOfPlayer == true && this.NodeHandler.IsInTrigger == false && this.NodeHandler.DistanceToPlayer < 5f)
        {
            Debug.Log("chasing");
            this.NodeHandler.Anim.SetBool("iswWalking", true);

            this.NodeHandler.DesiredVelocityForChasing = Vector3.Normalize(this.NodeHandler.Player.transform.position - this.NodeHandler.transform.position) * this.NodeHandler.MaxVelocityForChase;

            this.NodeHandler.SteeringForChase = this.NodeHandler.DesiredVelocityForChasing - this.NodeHandler.AgentRigidBody.velocity;

            this.NodeHandler.SteeringForChase = Vector3.ClampMagnitude(this.NodeHandler.SteeringForChase, this.NodeHandler.MaxForceForChase);

            this.NodeHandler.SteeringForChase = this.NodeHandler.SteeringForChase / this.NodeHandler.AgentRigidBody.mass;

            this.NodeHandler.AgentRigidBody.velocity = Vector3.ClampMagnitude(this.NodeHandler.AgentRigidBody.velocity + this.NodeHandler.SteeringForChase, this.NodeHandler.MaxSpeedForChase);

            this.NodeHandler.transform.position = this.NodeHandler.transform.position + this.NodeHandler.AgentRigidBody.velocity;

            this.NodeHandler.DirectionForRotation = this.NodeHandler.Player.transform.position - this.NodeHandler.transform.position;

            this.NodeHandler.RotationSpeed = 2f * Time.deltaTime;

            this.NodeHandler.DirectionForRotationToSet = Vector3.RotateTowards(this.NodeHandler.transform.forward, this.NodeHandler.DirectionForRotation, this.NodeHandler.RotationSpeed, 0);

            Quaternion lookAt = Quaternion.LookRotation(this.NodeHandler.DirectionForRotationToSet);

            this.NodeHandler.transform.rotation = lookAt;

            //Set the node status to running
            SetNodeStatus(NodeStates.running);

            //Check if the distance to the player is under 2f
            if (this.NodeHandler.DistanceToPlayer <= 2f)
            {
                //Set the nodestatus to success
                SetNodeStatus(NodeStates.success);
            }

            if(this.NodeHandler.AgentHasSightOfPlayer == true && this.NodeHandler.IsInTrigger == true)
            {
                SetNodeStatus(NodeStates.success);
            }
        }

        else
        {
            SetNodeStatus(NodeStates.failed);
        }
    }
}
