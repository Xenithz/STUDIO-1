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

    //angle will contain the angle between the direction of the enemy and player, and the front direction of the agent
    private float angle;

    //enemyFieldOfView will contain the fixed FOV of the enemy(can only see the player in that angle)
    private float enemyFieldOfView = 100f;

    //speed will store the desired rotation speed
    private float rotationSpeed;

    //directionbetween enemy and player will store the normalized magnitude (direction) of the agent to the target
    private Vector3 directionBetweenEnemyAndPlayer;

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
        //Continously update the speed
        float rotationSpeed = 3f * Time.deltaTime;

        //Calculate the magnitude and then normalize to get the direction
        directionBetweenEnemyAndPlayer = (player.transform.position - transform.position).normalized;

        //calculate the angle between the direction of the agent to the target, and the forward direction of the agent
        angle = Vector3.Angle(directionBetweenEnemyAndPlayer, transform.forward);

        //Create a new quaternion with the specified direction
        Quaternion lookAt = Quaternion.LookRotation(directionBetweenEnemyAndPlayer);

        //Update the door timer
        doorOpenTimer += Time.deltaTime;
        
        //Set the distance of the player and update accordingly
        distanceToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        //Reset the nodes in the tree
        test.MassNodeReset();

        //Run through the behaviors of the tree
        test.RunThroughTree(this, isInTrigger, doorCollider, doorOpenTimer, player, distanceToPlayer, angle, enemyFieldOfView, rotationSpeed, directionBetweenEnemyAndPlayer);
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
