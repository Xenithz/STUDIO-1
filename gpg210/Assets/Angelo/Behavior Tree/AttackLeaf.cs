using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLeaf : Node
{
    /*
         The attack leaf will be used to handle the attack behavior of the A.I. When the A.I is within a certain range of the player, it will kill the player instantly.
    */

    public AttackLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        //Check if the distance is smaller than float 2
        if(this.NodeHandler.DistanceToPlayer < 2f)
        {
            Debug.Log("access");
            this.NodeHandler.Anim.SetTrigger("shouldAttack");

            if(this.NodeHandler.DistanceToPlayer < 1.5f)
            {
                this.NodeHandler.AgentRigidBody.velocity = Vector3.ClampMagnitude(this.NodeHandler.AgentRigidBody.velocity, 0);

                this.NodeHandler.Player.GetComponent<PlayerControllerV5>().isDead = true;

                if(this.NodeHandler.Player.GetComponent<PlayerControllerV5>().isDead == true)
                {
                    this.NodeHandler.AttackTimer += Time.deltaTime;

                    this.NodeHandler.Player.GetComponent<PlayerControllerV5>().ForcedTurn();

                    if (this.NodeHandler.AttackTimer > 5f)
                    {
                        //Trigger the die function attached to the player
                        this.NodeHandler.Player.GetComponent<PlayerControllerV5>().Die();
                        Debug.Log("DIE");

                        //Set the node state to success
                        base.SetNodeStatus(NodeStates.success);
                    }
                }
            }
        }

        if (this.NodeHandler.Player.GetComponent<PlayerControllerV5>().isDead == true)
        {
            this.NodeHandler.AttackTimer += Time.deltaTime;

            this.NodeHandler.Player.GetComponent<PlayerControllerV5>().ForcedTurn();

            if (this.NodeHandler.AttackTimer > 5f)
            {
                //Trigger the die function attached to the player
                this.NodeHandler.Player.GetComponent<PlayerControllerV5>().Die();
                Debug.Log("DIE");

                //Set the node state to success
                base.SetNodeStatus(NodeStates.success);
            }
        }

        else
        {
            Debug.Log("cant attack");
            //Set the node state to failed
            base.SetNodeStatus(NodeStates.failed);
        }
    }
}
