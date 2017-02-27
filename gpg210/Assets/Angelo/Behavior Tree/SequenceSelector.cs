using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceSelector : Node
{
    /*
         The sequence selector node will iterate through all the nodes in it's children list and run each node's NodeBehavior.
         If a NodeBehavior returns failed, it will go end the loop
    */

    public SequenceSelector(int desiredPriority, string desiredName, Handler desiredHandler) : base(desiredPriority, desiredName, desiredHandler)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
    }

    public override void NodeBehavior()
    {
        for(int i = 0; i < this.Children.Count; i++)
        {
            this.Children[i].NodeBehavior();

            if (this.Children[i].BoolCheckNodeState(NodeStates.failed))
            {
                BoolCheckNodeState(NodeStates.failed);
                return;
            }
            else if (this.Children[i].BoolCheckNodeState(NodeStates.running))
            {
                BoolCheckNodeState(NodeStates.running);
                return;
            }
            else if (this.Children[i].BoolCheckNodeState(NodeStates.success))
            {
                
            }
        }
        base.BoolCheckNodeState(NodeStates.success);
    }
}
