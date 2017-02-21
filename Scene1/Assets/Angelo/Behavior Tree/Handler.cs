using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    /* 
        The handler class will be used to hold the behavior tree. The construction of the behavior tree will happen here.
        All nodes will be added in the awake function. After all nodes are added, the class will be added to the enemy prefab
    */
    [SerializeField]
    private GameObject player;
    
    private BehaviorTree test;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");

        test = new BehaviorTree();
        test.AddRoot();
    }

    private void Update()
    {
        test.MassNodeReset();
        test.RunThroughTree(this);
    }
}
