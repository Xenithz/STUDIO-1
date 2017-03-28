using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorLeaf : Node
{
    /*
        The open door leaf will be used for the A.I to open doors. It works by updating a timer when the A.I is inside of a trigger.
        Once the timer reaches a certain value, the A.I will open the door. This leaf node can never fail.
   */

    //OpenDoorLeaf constructo
    public OpenDoorLeaf(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    //Override the NodeBehavior function to implement the behavior of the OpenDoorLeaf
    public override void NodeBehavior()
    {
        //Check if the node handler is inside the trigger
       if(this.NodeHandler.IsInTrigger == true)
        {
            //Update the timer by time.delta time so that it increases by 1 second per real time
            this.NodeHandler.DoorOpenTimer += Time.deltaTime;

            //this.NodeHandler.Anim.SetBool("isWalking", false);

            //Set it to running after updating the timer
            SetNodeStatus(NodeStates.running);

            //Once the timer goes past 3, then access the behavior of the door and reset the timer, then disable the collider box of the door, and set the in trigger bool to false
            if (this.NodeHandler.DoorOpenTimer >= 3)
            {
                this.NodeHandler.DoorCollider.gameObject.GetComponentInParent<MonoItem>().CurrentBehavior();
                this.NodeHandler.DoorOpenTimer = 0f;
                this.NodeHandler.DoorCollider.gameObject.SetActive(false);
                this.NodeHandler.IsInTrigger = false;

                SetNodeStatus(NodeStates.success);
            }
        }

       else
       {
            SetNodeStatus(NodeStates.success);
       }
    }
}
