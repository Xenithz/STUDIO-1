using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLeaf : Node
{
    /*
         
    */

    public AttackLeaf(int desiredPriority, string desiredName) : base(desiredPriority, desiredName)
    {
        this.NodePriority = desiredPriority;
        this.NodeName = desiredName;
    }

    public override void NodeBehavior(Handler agent)
    {

    }
}
