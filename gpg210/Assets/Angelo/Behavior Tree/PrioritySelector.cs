using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySelector : Node
{
    /*
         The priority selector class will go through the nodes according to the priority value that was assigned to them
         If a node returns successfull, it will stop going through the nodes
    */

    //PrioritySelector constructor
    public PrioritySelector(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    //Override the virtual NodeBehavior in the Node class to apply the priority selector's specific functionality
    public override void NodeBehavior()
    {
        //Iterate through all the nodes in the children list of the priority selector
        for(int i = 0; i < this.Children.Count; i++)
        {
            //Run each child's behavior
            this.Children[i].NodeBehavior();

            //Check if the child's nodestate success
            if (this.Children[i].BoolCheckNodeState(NodeStates.success))
            {
                //Set the priority selector's state to successs
                SetNodeStatus(NodeStates.success);
                return;
            }

            //Check if the child's nodestate is running
            else if (this.Children[i].BoolCheckNodeState(NodeStates.running))
            {
                //Set the priority selector's state to runnning
                SetNodeStatus(NodeStates.running);
                return;
            }

            //Check if the child's nodestate is failed
            else if (this.Children[i].BoolCheckNodeState(NodeStates.failed))
            {

            }
        }
        SetNodeStatus(NodeStates.failed);
    }
}
