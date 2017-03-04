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
        //Update the rotation speed for smoothness
        this.NodeHandler.RotationSpeed = 3f * Time.deltaTime;

        //Update the target transform a by iterating through the whole list via the usage of the incrementer variable
        this.NodeHandler.PatrolTargetTransform = this.NodeHandler.patrolWaypoints[this.NodeHandler.PatrolIncrementer];

        //Update the distance from the agent to the waypoint by using vector3.distance taking in the arguments (currrent transform, target transform)
        this.NodeHandler.DistanceFromAgentToWaypoint = Vector3.Distance(this.NodeHandler.transform.position, this.NodeHandler.PatrolTargetTransform.position);

        //Update the direction to the patrolwaypoint by normalizing the magnitude
        this.NodeHandler.DirectionToPatrolWaypoint = Vector3.Normalize(this.NodeHandler.PatrolTargetTransform.position - this.NodeHandler.transform.position);

        //Quaternion to store the rotation of the direction
        Quaternion lookAt = Quaternion.LookRotation(this.NodeHandler.DirectionToPatrolWaypoint);

        //Agent will always look at the rotation of the current target waypoint
        this.NodeHandler.transform.rotation = Quaternion.Lerp(this.NodeHandler.transform.rotation, lookAt, this.NodeHandler.RotationSpeed);

        //The desired velocity is calculated by normalizing the magnitude and multiplying it by the maximum velocity
        this.NodeHandler.DesiredVelocityForPatrolling = Vector3.Normalize(this.NodeHandler.PatrolTargetTransform.position - this.NodeHandler.transform.position) * this.NodeHandler.MaxVelocityForPatrol;

        //Create the steering force by minusing the current velocity from the desired velocity
        this.NodeHandler.SteeringForPatrol = this.NodeHandler.DesiredVelocityForPatrolling - this.NodeHandler.AgentRigidBody.velocity;

        //Add the force to the rigid body to get movemement
        this.NodeHandler.AgentRigidBody.AddForce(this.NodeHandler.SteeringForPatrol);

        //Clamp the magnitude of the velocity so that it does not exceed the maximum velocity
        this.NodeHandler.AgentRigidBody.velocity = Vector3.ClampMagnitude(this.NodeHandler.AgentRigidBody.velocity, this.NodeHandler.MaxVelocityForPatrol);

        //Check if the Agent is close to the waypoint
        if(this.NodeHandler.DistanceFromAgentToWaypoint < 2f)
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
    }
}
