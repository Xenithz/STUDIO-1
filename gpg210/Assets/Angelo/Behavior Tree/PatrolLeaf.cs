using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLeaf : Node
{
    /*
         The patrol leaf will handle the patrolling behavior of the A.I. It will access the designated list of waypoints. The A.I makes use of the steering behavior seek
         to patrol between the points.
    */

    public PatrolLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        //this.NodeHandler.Anim.SetBool("isWalking", true);

        //Play the scream
        if(this.NodeHandler.scream.isPlaying != true)
        {
            this.NodeHandler.ScreamAudioCue = true;
        }
        if (this.NodeHandler.ScreamAudioCue == true)
        {
            this.NodeHandler.scream.PlayOneShot(this.NodeHandler.scream.clip, 1);
            this.NodeHandler.ScreamAudioCue = false;
        }

        this.NodeHandler.Anim.SetFloat("walkSpeed", 1);
        this.NodeHandler.Anim.SetBool("isWalking", true);

        //Set the current target transform according to the incrementer's value relative to the index of the patrol waypoints array
        this.NodeHandler.PatrolTargetTransform = this.NodeHandler.patrolWaypoints[this.NodeHandler.PatrolIncrementer];

        //Create a path to the patrol target transform by using the pathfinder's create a path function
        this.NodeHandler.PatrolTargetTransform = this.NodeHandler.PathHnd.CreateAPath(this.NodeHandler.transform, this.NodeHandler.PatrolTargetTransform);

        //Set the distance between the agent and the target waypoint
        this.NodeHandler.DistanceFromAgentToWaypoint = Vector3.Distance(this.NodeHandler.transform.position, this.NodeHandler.PatrolTargetTransform.position);

        //Set the desired velocity by getting the direction via normalization, and then multiply it into the max velocity as this is the optimal velocity
        this.NodeHandler.DesiredVelocityForPatrolling = Vector3.Normalize(this.NodeHandler.PatrolTargetTransform.position - this.NodeHandler.transform.position) * this.NodeHandler.MaxVelocityForPatrol;

        //Calculate the steering force by subtracting the current velocity from the desired velocity
        this.NodeHandler.SteeringForPatrol = this.NodeHandler.DesiredVelocityForPatrolling - this.NodeHandler.AgentRigidBody.velocity;

        //Clamp the steering force so that it does not exceed the max force for patroling
        this.NodeHandler.SteeringForPatrol = Vector3.ClampMagnitude(this.NodeHandler.SteeringForPatrol, this.NodeHandler.MaxForceForPatrol);

        //Divide the steering force by mass so that mass is considered
        this.NodeHandler.SteeringForPatrol = this.NodeHandler.SteeringForPatrol / this.NodeHandler.AgentRigidBody.mass;

        //Clamp the velocity of the agent so that it does not go past the maximum speed
        this.NodeHandler.AgentRigidBody.velocity = Vector3.ClampMagnitude(this.NodeHandler.AgentRigidBody.velocity + this.NodeHandler.SteeringForPatrol, this.NodeHandler.MaxSpeedForPatrol);

        //Make use of euler integration to apply the steering force
        this.NodeHandler.transform.position = this.NodeHandler.transform.position + this.NodeHandler.AgentRigidBody.velocity;

        //Get the direction to the patrol target by normalizing
        this.NodeHandler.DirectionForRotation = Vector3.Normalize(this.NodeHandler.PatrolTargetTransform.position - this.NodeHandler.transform.position);

        //Update the rotation speed by using delta time for smooth movement
        this.NodeHandler.RotationSpeed = 3f * Time.deltaTime;

        ////Create a vector 3 for the agent to rotate towards
        //this.NodeHandler.DirectionForRotationToSet = Vector3.RotateTowards(this.NodeHandler.transform.forward, this.NodeHandler.DirectionForRotation, this.NodeHandler.RotationSpeed, 0);

        //Vector3 temp = this.NodeHandler.DirectionForRotation;

        //if(temp.y > 0)
        //{
        //    temp.y = 0;
        //}

        ////Quaternion to turn to
        //Quaternion lookAt = Quaternion.LookRotation(temp);

        Quaternion lookAt = Quaternion.LookRotation(this.NodeHandler.DirectionForRotation);

        this.NodeHandler.transform.rotation = Quaternion.Lerp(this.NodeHandler.transform.rotation, lookAt, this.NodeHandler.RotationSpeed);

        //Make the rotation set itself to lookAt
        //this.NodeHandler.transform.rotation = lookAt;

        //Check if the Agent is close to the waypoint
        if (this.NodeHandler.DistanceFromAgentToWaypoint < 0.5f)
        {
            //Move to the next waypoint
            this.NodeHandler.PatrolIncrementer++;
        }

        //Check if the incrementer is at the last waypoint
        if(this.NodeHandler.PatrolIncrementer >= this.NodeHandler.patrolWaypoints.Count)
        {
            //Reset it
            this.NodeHandler.PatrolIncrementer = 0;
        }

        //Set the node state to running whilst patrolling
        this.SetNodeStatus(NodeStates.running);
    }
}
