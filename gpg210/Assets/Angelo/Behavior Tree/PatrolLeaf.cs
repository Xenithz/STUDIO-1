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

        this.NodeHandler.PatrolTargetTransform = this.NodeHandler.patrolWaypoints[this.NodeHandler.PatrolIncrementer];

        this.NodeHandler.DistanceFromAgentToWaypoint = Vector3.Distance(this.NodeHandler.transform.position, this.NodeHandler.PatrolTargetTransform.position);

        this.NodeHandler.DesiredVelocityForPatrolling = Vector3.Normalize(this.NodeHandler.PatrolTargetTransform.position - this.NodeHandler.transform.position) * this.NodeHandler.MaxVelocityForPatrol;

        this.NodeHandler.SteeringForPatrol = this.NodeHandler.DesiredVelocityForPatrolling - this.NodeHandler.AgentRigidBody.velocity;

        this.NodeHandler.SteeringForPatrol = Vector3.ClampMagnitude(this.NodeHandler.SteeringForPatrol, this.NodeHandler.MaxForceForPatrol);

        this.NodeHandler.SteeringForPatrol = this.NodeHandler.SteeringForPatrol / this.NodeHandler.AgentRigidBody.mass;

        this.NodeHandler.AgentRigidBody.velocity = Vector3.ClampMagnitude(this.NodeHandler.AgentRigidBody.velocity + this.NodeHandler.SteeringForPatrol, this.NodeHandler.MaxSpeedForPatrol);

        this.NodeHandler.transform.position = this.NodeHandler.transform.position + this.NodeHandler.AgentRigidBody.velocity;

        this.NodeHandler.DirectionForRotation = this.NodeHandler.PatrolTargetTransform.position - this.NodeHandler.transform.position;

        this.NodeHandler.RotationSpeed = 4f * Time.deltaTime;

        this.NodeHandler.DirectionForRotationToSet = Vector3.RotateTowards(this.NodeHandler.transform.forward, this.NodeHandler.DirectionForRotation, this.NodeHandler.RotationSpeed, 0);

        Quaternion lookAt = Quaternion.LookRotation(this.NodeHandler.DirectionForRotationToSet);

        this.NodeHandler.transform.rotation = lookAt;

        //Check if the Agent is close to the waypoint
        if (this.NodeHandler.DistanceFromAgentToWaypoint < 0.5f )
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

        this.SetNodeStatus(NodeStates.running);
    }
}
