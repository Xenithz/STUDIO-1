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
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        this.NodeHandler.RotationSpeed = 3f * Time.deltaTime;

        this.NodeHandler.PatrolTargetTransform = this.NodeHandler.patrolWaypoints[this.NodeHandler.PatrolIncrementer];

        this.NodeHandler.DistanceFromAgentToWaypoint = Vector3.Distance(this.NodeHandler.transform.position, this.NodeHandler.PatrolTargetTransform.position);

        this.NodeHandler.DirectionToPatrolWaypoint = Vector3.Normalize(this.NodeHandler.PatrolTargetTransform.position - this.NodeHandler.transform.position);

        Quaternion lookAt = Quaternion.LookRotation(this.NodeHandler.DirectionToPatrolWaypoint);

        this.NodeHandler.transform.rotation = Quaternion.Lerp(this.NodeHandler.transform.rotation, lookAt, this.NodeHandler.RotationSpeed);

        this.NodeHandler.DesiredVelocityForPatrolling = Vector3.Normalize(this.NodeHandler.PatrolTargetTransform.position - this.NodeHandler.transform.position) * this.NodeHandler.MaxVelocityForPatrol;

        this.NodeHandler.SteeringForPatrol = this.NodeHandler.DesiredVelocityForPatrolling - this.NodeHandler.AgentRigidBody.velocity;

        this.NodeHandler.AgentRigidBody.AddForce(this.NodeHandler.SteeringForPatrol);

        this.NodeHandler.AgentRigidBody.velocity = Vector3.ClampMagnitude(this.NodeHandler.AgentRigidBody.velocity, this.NodeHandler.MaxVelocityForPatrol);
    }
}
