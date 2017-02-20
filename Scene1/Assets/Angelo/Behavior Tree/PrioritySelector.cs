using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySelector : Node
{
    /*
         
    */

    public PrioritySelector(int desiredPriority, string desiredName) : base(desiredPriority, desiredName)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
    }

    public override void NodeBehavior()
    {
        for(int i = 0; i < this.Children.Count; i++)
        {
            this.Children[i].NodeBehavior();

            if (this.Children[i].BoolCheckNodeState(NodeStates.success))
            {
                base.BoolCheckNodeState(NodeStates.success);
                return;
            }
            else if (this.Children[i].BoolCheckNodeState(NodeStates.running))
            {
                base.BoolCheckNodeState(NodeStates.running);
                continue;
            }
            else if (this.Children[i].BoolCheckNodeState(NodeStates.failed))
            {
                continue;
            }
        }
        base.BoolCheckNodeState(NodeStates.failed);
    }
}
