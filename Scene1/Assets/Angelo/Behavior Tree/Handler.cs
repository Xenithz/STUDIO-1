using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    /* 
        The handler class will be used to hold the behavior tree. The construction of the behavior tree will happen here.
        All nodes will be added in the awake function. After all nodes are added, the class will be added to the enemy prefab
    */

    //Get a reference for the player in the scene
    [SerializeField]
    private GameObject player;
    
    //Create an object of class BehaviorTree
    private BehaviorTree test;

    //Check if the A.I is inside the trigger of a doorspace
    private bool isInTrigger;

    //Get a reference for the collider of the door
    private Collider doorCollider;

    //Incrementing timer for door opening mechanic
    private float doorOpenTimer;

    //track the distance to the player
    private float distanceToPlayer;

    private void Awake()
    {
        //Set the player variable to the player in the scene
        player = GameObject.FindGameObjectWithTag("player");

        //Initialize the new behavior tree
        test = new BehaviorTree();

        //Add the root to the behavior tree
        test.AddRoot();
    }

    private void Update()
    {
        //Update the door timer
        doorOpenTimer += Time.deltaTime;
        
        //Set the distance of the player and update accordingly
        distanceToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        //Reset the nodes in the tree
        test.MassNodeReset();

        //Run through the behaviors of the tree
        test.RunThroughTree(this, isInTrigger, doorCollider, doorOpenTimer, player, distanceToPlayer);
    }

    private void OnTriggerStay(Collider other)
    {
        //Check if the A.I is in a doorspace trigger
        if (other.gameObject.tag == "doorSpace")
        {
            //Set the doorCollider to the gameobject with the trigger
            doorCollider = other;

            //Set the intrigger variable to true
            isInTrigger = true;
        }
        else
        {
            //Reset the doorcollider
            doorCollider = null;
            
            //Set the intrigger variable to false
            isInTrigger = false;
        }
    }
}
