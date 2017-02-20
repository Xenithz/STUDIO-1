using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceSelector : Node
{
    /*
         
    */

    public SequenceSelector(int desiredPriority, string desiredName) : base(desiredPriority, desiredName)
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
                base.BoolCheckNodeState(NodeStates.failed);
                return;
            }
            else if (this.Children[i].BoolCheckNodeState(NodeStates.running))
            {
                base.BoolCheckNodeState(NodeStates.running);
                continue;
            }
            else if (this.Children[i].BoolCheckNodeState(NodeStates.success))
            {
                continue;
            }
        }
        base.BoolCheckNodeState(NodeStates.success);
    }
}
