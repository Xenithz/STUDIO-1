using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySelector : Node
{
    /*
         The priority selector class will go through the nodes according to the priority value that was assigned to them
         If a node returns successfull, it will stop going through the nodes
    */

    public PrioritySelector(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
        this.NodeHandler = desiredHandler;
    }

    public override void NodeBehavior()
    {
        for(int i = 0; i < this.Children.Count; i++)
        {
            this.Children[i].NodeBehavior();

            if (this.Children[i].BoolCheckNodeState(NodeStates.success))
            {
                BoolCheckNodeState(NodeStates.success);
                return;
            }
            else if (this.Children[i].BoolCheckNodeState(NodeStates.running))
            {
                BoolCheckNodeState(NodeStates.running);
                return;
            }
            else if (!this.Children[i].BoolCheckNodeState(NodeStates.failed))
            {

            }
        }
        base.BoolCheckNodeState(NodeStates.failed);
    }
}
