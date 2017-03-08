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
        if (this.NodeHandler.AgentHasSightOfPlayer == true && this.NodeHandler.IsInTrigger == false)
        {
            Debug.Log("CAN SEE");
            Debug.Log(this.NodeHandler.AgentHasSightOfPlayer);
            //Continously update the speed
            this.NodeHandler.RotationSpeed = 3f * Time.deltaTime;

            //Create a new quaternion with the specified direction
            Quaternion lookAt = Quaternion.LookRotation(this.NodeHandler.DirectionBetweenEnemyAndPlayer);

            //Lerp the rotation of the NodeHandler towards the lookAt quaternion using the specified RotationSpeed
            this.NodeHandler.transform.rotation = Quaternion.Lerp(this.NodeHandler.transform.rotation, lookAt, this.NodeHandler.RotationSpeed);

            //The desired velocity is calculated by normalizing the magnitude and multiplying it by the maximum velocity
            this.NodeHandler.DesiredVelocityForPatrolling = Vector3.Normalize(this.NodeHandler.Player.transform.position - this.NodeHandler.transform.position) * this.NodeHandler.MaxVelocityForPatrol;

            //Create the steering force by minusing the current velocity from the desired velocity
            this.NodeHandler.SteeringForPatrol = this.NodeHandler.DesiredVelocityForPatrolling - this.NodeHandler.AgentRigidBody.velocity;

            //Add the force to the rigid body to get movemement
            this.NodeHandler.AgentRigidBody.AddForce(this.NodeHandler.SteeringForPatrol);

            //Clamp the magnitude of the velocity so that it does not exceed the maximum velocity
            this.NodeHandler.AgentRigidBody.velocity = Vector3.ClampMagnitude(this.NodeHandler.AgentRigidBody.velocity, this.NodeHandler.MaxVelocityForPatrol);

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
                Debug.Log("using this");
                SetNodeStatus(NodeStates.success);
            }
        }

        else
        {
            Debug.Log("this is failing");
            SetNodeStatus(NodeStates.failed);
        }
    }
}
