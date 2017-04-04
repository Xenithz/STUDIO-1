using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLeaf : Node
{
    /*
        The chase leaf class will be used to make the A.I chase the player. The Chase leaf makes usage of pathfinding and steering behaviors.
    */

    //ChaseLeaf constructor
    public ChaseLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    //Override the NodeBehavior function to implement the ChaseLeaf behavior
    public override void NodeBehavior()
    {
        if (this.NodeHandler.AgentHasSightOfPlayer == true && this.NodeHandler.IsInTrigger == false && this.NodeHandler.DistanceToPlayer < 5.5f && this.NodeHandler.Player.GetComponent<PlayerControllerV5>().isDead == false && this.NodeHandler.ScreamAndRun == true)
        {
            Debug.Log("chasing");

           if(this.NodeHandler.ShouldTurnAroundInstantly == true)
           {
                if (this.NodeHandler.scream.isPlaying != true)
                {
                    this.NodeHandler.ScreamAudioCue = true;
                }
                if (this.NodeHandler.ScreamAudioCue == true)
                {
                    this.NodeHandler.scream.PlayOneShot(this.NodeHandler.scream.clip, 3.5f);
                    this.NodeHandler.ScreamAudioCue = false;
                }

                this.NodeHandler.DirectionForRotation = this.NodeHandler.Player.transform.position - this.NodeHandler.transform.position;

                this.NodeHandler.RotationSpeed = 10f * Time.deltaTime;

                Quaternion lookAt = Quaternion.LookRotation(this.NodeHandler.DirectionForRotation);

                this.NodeHandler.transform.rotation = Quaternion.Lerp(this.NodeHandler.transform.rotation, lookAt, this.NodeHandler.RotationSpeed);

                if(this.NodeHandler.transform.rotation == lookAt)
                {
                    SetNodeStatus(NodeStates.success);
                }
            }

            else
            {
                Debug.Log("gotcha");
                if (this.NodeHandler.scream.isPlaying != true)
                {
                    this.NodeHandler.ScreamAudioCue = true;
                }
                if (this.NodeHandler.ScreamAudioCue == true)
                {
                    this.NodeHandler.scream.PlayOneShot(this.NodeHandler.scream.clip, 3.5f);
                    this.NodeHandler.ScreamAudioCue = false;
                }

                this.NodeHandler.Anim.SetFloat("walkSpeed", 3);

                this.NodeHandler.Anim.SetBool("iswWalking", true);

                Transform target = this.NodeHandler.PathHnd.CreateAPath(this.NodeHandler.transform, this.NodeHandler.Player.transform);

                this.NodeHandler.DesiredVelocityForChasing = Vector3.Normalize(target.position - this.NodeHandler.transform.position) * this.NodeHandler.MaxVelocityForChase;

                this.NodeHandler.SteeringForChase = this.NodeHandler.DesiredVelocityForChasing - this.NodeHandler.AgentRigidBody.velocity;

                this.NodeHandler.SteeringForChase = Vector3.ClampMagnitude(this.NodeHandler.SteeringForChase, this.NodeHandler.MaxForceForChase);

                this.NodeHandler.SteeringForChase = this.NodeHandler.SteeringForChase / this.NodeHandler.AgentRigidBody.mass;

                this.NodeHandler.AgentRigidBody.velocity = Vector3.ClampMagnitude(this.NodeHandler.AgentRigidBody.velocity + this.NodeHandler.SteeringForChase, this.NodeHandler.MaxSpeedForChase);

                this.NodeHandler.AgentRigidBody.velocity = this.NodeHandler.AgentRigidBody.velocity;

                this.NodeHandler.DirectionForRotation = this.NodeHandler.Player.transform.position - this.NodeHandler.transform.position;

                this.NodeHandler.RotationSpeed = 2f * Time.deltaTime;

                this.NodeHandler.DirectionForRotationToSet = Vector3.RotateTowards(this.NodeHandler.transform.forward, this.NodeHandler.DirectionForRotation, this.NodeHandler.RotationSpeed, 0);

                Quaternion lookAt = Quaternion.LookRotation(this.NodeHandler.DirectionForRotationToSet);

                this.NodeHandler.transform.rotation = lookAt;

                this.NodeHandler.transform.rotation = Quaternion.Euler(0, this.NodeHandler.transform.rotation.eulerAngles.y, 0);

                //Set the node status to running
                SetNodeStatus(NodeStates.running);

                //Check if the distance to the player is under 2f
                if (this.NodeHandler.DistanceToPlayer <= 2f)
                {
                    //Set the nodestatus to success
                    SetNodeStatus(NodeStates.success);
                }

                if (this.NodeHandler.AgentHasSightOfPlayer == true && this.NodeHandler.IsInTrigger == true)
                {
                    SetNodeStatus(NodeStates.success);
                }
            }
        }

        else if(this.NodeHandler.Player.GetComponent<PlayerControllerV5>().isDead == true)
        {
            SetNodeStatus(NodeStates.success);
        }

        else
        {
            SetNodeStatus(NodeStates.failed);
        }
    }
}
