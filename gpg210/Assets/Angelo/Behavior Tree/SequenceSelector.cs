using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceSelector : Node
{
    /*
         The sequence selector node will iterate through all the nodes in it's children list and run each node's NodeBehavior.
         If a NodeBehavior returns failed, it will go end the loop
    */


    //SequenceSelector constructor
    public SequenceSelector(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
    }

    //Override the node behavior function to implement the behavior of the sequence selector
    public override void NodeBehavior()
    {
        //Loop through all of the children of the SequenceSelector
        for(int i = 0; i < this.Children.Count; i++)
        {
            //Access each child's behavior
            this.Children[i].NodeBehavior();

            //Check if the behavior returns fail, if it returns fail then also return fail
            if (this.Children[i].BoolCheckNodeState(NodeStates.failed))
            {
                base.SetNodeStatus(NodeStates.failed);
                return;
            }
            
            //If it returns running, then return running
            else if (this.Children[i].BoolCheckNodeState(NodeStates.running))
            {
                base.SetNodeStatus(NodeStates.running);
                return;
            }

            //If it returns success, then continue
            else if (this.Children[i].BoolCheckNodeState(NodeStates.success))
            {
                
            }
        }

        //Return success if all of the children return success
        SetNodeStatus(NodeStates.success);
    }
}
