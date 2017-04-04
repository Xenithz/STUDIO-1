using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoleInteractionLeaf : Node
{
    public SoleInteractionLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        if (this.NodeHandler.AgentHasSightOfPlayer == true && this.NodeHandler.IsInTrigger == false && this.NodeHandler.DistanceToPlayer < 5.5f && this.NodeHandler.Player.GetComponent<PlayerControllerV5>().isDead == false && this.NodeHandler.ScreamAndRun == false)
        {
            if (this.NodeHandler.ScreamAndRun == false)
            {
                SetNodeStatus(NodeStates.running);
                this.NodeHandler.AgentRigidBody.velocity = Vector3.ClampMagnitude(this.NodeHandler.AgentRigidBody.velocity, 0f);
                this.NodeHandler.Anim.SetBool("isWalking", false);

                this.NodeHandler.QuickTimer += Time.deltaTime;

                if (this.NodeHandler.QuickTimer >= 4f)
                {
                    this.NodeHandler.scream.PlayOneShot(this.NodeHandler.scream.clip, 15f);
                    this.NodeHandler.PatrolIncrementer--;
                    this.NodeHandler.ScreamAndRun = true;
                }
            }

            else if (this.NodeHandler.ScreamAndRun == true)
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
