using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    /* 
        The handler class will be used to hold the behavior tree. The construction of the behavior tree will happen here.
        All nodes will be added in the awake function. After all nodes are added, the class will be added to the enemy prefab
    */

    //Create an object of class BehaviorTree
    private BehaviorTree test;

    //Get a reference for the player in the scene
    [SerializeField]
    private GameObject player;

    //Allows the player variable to be kept private
    public GameObject Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    //Check if the A.I is inside the trigger of a doorspace
    private bool isInTrigger;

    //allows the isInTrigger variable to be kept private
    public bool IsInTrigger
    {
        get
        {
            return isInTrigger;
        }

        set
        {
            isInTrigger = value;
        }
    }

    //Get a reference for the collider of the door
    private Collider doorCollider;

    //allows the doorCollider variable to be kept private
    public Collider DoorCollider
    {
        get
        {
            return doorCollider;
        }

        set
        {
            doorCollider = value;
        }
    }

    //Incrementing timer for door opening mechanic
    private float doorOpenTimer;

    //allows the doorOpenTimer to be kept private
    public float DoorOpenTimer
    {
        get
        {
            return doorOpenTimer;
        }

        set
        {
            doorOpenTimer = value;
        }
    }

    //track the distance to the player
    private float distanceToPlayer;

    //allows the distanceToPlayer variable to kept private
    public float DistanceToPlayer
    {
        get
        {
            return distanceToPlayer;
        }

        set
        {
            distanceToPlayer = value;
        }
    }

    //angle will contain the angle between the direction of the enemy and player, and the front direction of the agent
    private float angle;

    //allows the angle variable to be kept private
    public float Angle
    {
        get
        {
            return angle;
        }

        set
        {
            angle = value;
        }
    }

    //enemyFieldOfView will contain the fixed FOV of the enemy(can only see the player in that angle)
    private float enemyFieldOfView = 100f;

    //allows the enemyFieldOfView variable to be kept private
    public float EnemyFieldOfView
    {
        get
        {
            return enemyFieldOfView;
        }

        set
        {
            enemyFieldOfView = value;
        }
    }

    //speed will store the desired rotation speed
    private float rotationSpeed;

    //allows the rotationSpeed variable to be kept private
    public float RotationSpeed
    {
        get
        {
            return rotationSpeed;
        }

        set
        {
            rotationSpeed = value;
        }
    }

    //directionbetween enemy and player will store the normalized magnitude (direction) of the agent to the target
    private Vector3 directionBetweenEnemyAndPlayer;

    //allows the directionBetweenEnemyAndPlayer variable to be kept private
    public Vector3 DirectionBetweenEnemyAndPlayer
    {
        get
        {
            return directionBetweenEnemyAndPlayer;
        }

        set
        {
            directionBetweenEnemyAndPlayer = value;
        }
    }

    private void Awake()
    {
        //Set the player variable to the player in the scene
        player = GameObject.FindGameObjectWithTag("player");

        //Initialize the new behavior tree
        test = new BehaviorTree(this);

        //Add the root to the behavior tree
        test.AddRoot();

        test.DynamicAddNode(new SequenceSelector(0, "seq", this), "root");
        test.DynamicAddNode(new VisionLeaf(0, "vision", this), "seq");
        test.DynamicAddNode(new ChaseLeaf(1, "chase", this), "seq");
    }

    private void Update()
    {
        //Reset the nodes in the tree
        test.MassNodeReset();

        //Run through the behaviors of the tree
        test.RunThroughTree();
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
