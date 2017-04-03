using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    /* 
        The handler class will be used to hold the behavior tree. The construction of the behavior tree will happen here.
        All nodes will be added in the awake function. After all nodes are added, the class will be added to the enemy prefab
    */

    #region All variables
    //Create an object of class BehaviorTree
    private BehaviorTree test;

    public AudioSource scream;

    public AudioSource footsteps;

    public AudioClip[] footstepArray;

    private PathfindingHandler pathHnd;

    public PathfindingHandler PathHnd
    {
        get
        {
            return pathHnd;
        }

        set
        {
            pathHnd = value;
        }
    }

    [SerializeField]
    private GameManagerHandler gameMng;

    public GameManagerHandler GameMng
    {
        get
        {
            return gameMng;     
        }

        set
        {
            gameMng = value;
        }
    }

    [SerializeField]
    private Animator anim;

    public Animator Anim
    {
        get
        {
            return anim;
        }

        set
        {
            anim = value;
        }
    }

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

    //agentRigidBody stores a reference for the rigidbody attached to the handler gameobject
    private Rigidbody agentRigidBody;

    //AgentRigidbody allows agentRigidBody to stay as a private variable
    public Rigidbody AgentRigidBody
    {
        get
        {
            return agentRigidBody;
        }

        set
        {
            agentRigidBody = value;
        }
    }

    private bool shouldTurnAroundInstantly;

    public bool ShouldTurnAroundInstantly
    {
        get
        {
            return shouldTurnAroundInstantly;
        }

        set
        {
            shouldTurnAroundInstantly = value;
        }
    }

    private bool screamAudioCue;

    public bool ScreamAudioCue
    {
        get
        {
            return screamAudioCue;
        }

        set
        {
            screamAudioCue = value;
        }
    }

    private bool isPaused;

    public bool IsPaused
    {
        get
        {
            return isPaused;
        }

        set
        {
            isPaused = value;
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

    //Used to check if the agent has sight of the player
    private bool agentHasSightOfPlayer;

    //Property to keep agentHasSightOfPlayer private
    public bool AgentHasSightOfPlayer
    {
        get
        {
            return agentHasSightOfPlayer;
        }

        set
        {
            agentHasSightOfPlayer = value;
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


    private float attackTimer;

    public float AttackTimer
    {
        get
        {
            return attackTimer;
        }

        set
        {
            attackTimer = value;
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
    private float enemyFieldOfView = 150f;

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
    [SerializeField]
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

    //The maximum velocity to be used for clamping 
    [SerializeField]
    private float maxVelocityForPatrol = 0.1f;

    //allows the maxVelocityForPatrol to be kept private
    public float MaxVelocityForPatrol
    {
        get
        {
            return maxVelocityForPatrol;
        }

        set
        {
            maxVelocityForPatrol = value; 
        }
    }

    [SerializeField]
    private float maxVelocityForChase = 0.1f;

    public float MaxVelocityForChase
    {
        get
        {
            return maxVelocityForChase;
        }

        set
        {
            maxVelocityForChase = value;
        }
    }

    [SerializeField]
    private float maxForceForChase = 0.1f;

    public float MaxForceForChase
    {
        get
        {
            return maxForceForChase;
        }

        set
        {
            maxForceForChase = value;
        }
    }

    [SerializeField]
    private float maxForceForPatrol = 0.1f;

    public float MaxForceForPatrol
    {
        get
        {
            return maxForceForPatrol;
        }

        set
        {
            maxForceForPatrol = value;
        }
    }

    [SerializeField]
    private float maxSpeedForChase = 0.1f;

    public float MaxSpeedForChase
    {
        get
        {
            return maxSpeedForChase;
        }

        set
        {
            maxSpeedForChase = value;
        }
    }

    [SerializeField]
    private float maxSpeedForPatrol = 0.1f;

    public float MaxSpeedForPatrol
    {
        get
        {
            return maxSpeedForPatrol;
        }

        set
        {
            maxSpeedForPatrol = value;
        }
    }

    //distanceFromAgentToWaypoint will store the distance from the agent to the waypoint in the patrol leaf
    private float distanceFromAgentToWaypoint;

    //allows the distanceFromAgentToWaypoint to be kept private
    public float DistanceFromAgentToWaypoint
    {
        get
        {
            return distanceFromAgentToWaypoint;
        }

        set
        {
            distanceFromAgentToWaypoint = value;
        }
    }

    //directionbetween enemy and player will store the normalized direction of the agent to the target
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

    //directionToPatrolWaypoint will store the normalized direction of the agent to the target
    private Vector3 directionToPatrolWaypoint;

    //allows the directionToPatrolWaypoint variable to be kept private
    public Vector3 DirectionToPatrolWaypoint
    {
        get
        {
            return directionToPatrolWaypoint;
        }

        set
        {
            directionToPatrolWaypoint = value;
        }
    }


    //steeringForPatrol stores the steering force as a vector3, to be used inside the patrol leaf
    private Vector3 steeringForPatrol;

    //allows the steeringForPatrol variable to be kept private
    public Vector3 SteeringForPatrol
    {
        get
        {
            return steeringForPatrol;
        }

        set
        {
            steeringForPatrol = value;
        }
    }

    private Vector3 steeringForChase;

    public Vector3 SteeringForChase
    {
        get
        {
            return steeringForChase;
        }

        set
        {
            steeringForChase = value;
        }
    }

    //desiredVelocityForPatrolling stores the desired velocity for patrolling (normalization of magnitude of direction multiplied by the max velocity)
    private Vector3 desiredVelocityForPatrolling;

    //allows the desiredVelocityForPatrolling variable to be kept private
    public Vector3 DesiredVelocityForPatrolling
    {
        get
        {
            return desiredVelocityForPatrolling;
        }

        set
        {
            desiredVelocityForPatrolling = value;
        }
    }

    private Vector3 desiredVelocityForChasing;

    public Vector3 DesiredVelocityForChasing
    {
        get
        {
            return desiredVelocityForChasing;
        }

        set
        {
            desiredVelocityForChasing = value;
        }
    }

    private Vector3 directionForRotation;

    public Vector3 DirectionForRotation
    {
        get
        {
            return directionForRotation;
        }

        set
        {
            directionForRotation = value;
        }
    }

    private Vector3 directionForRotationToSet;

    public Vector3 DirectionForRotationToSet
    {
        get
        {
            return directionForRotationToSet;
        }

        set
        {
            directionForRotationToSet = value;
        }
    }

    //patrolTargetTransform will store the transform of the current waypoint that the A.I is patrolling to
    private Transform patrolTargetTransform;

    //allows the patrolTargetTransform variable to be kept private
    public Transform PatrolTargetTransform
    {
        get
        {
            return patrolTargetTransform;
        }

        set
        {
            patrolTargetTransform = value;
        }
    }

    //patrolIncrementer stores the value for iterating through the patrolWaypoints list
    private int patrolIncrementer = 0;

    //allows the patrolIncrementer variable to be kept private
    public int PatrolIncrementer
    {
        get
        {
            return patrolIncrementer;
        }

        set
        {
            patrolIncrementer = value;
        }
    }

    //patrolWaypoints list will store all the waypoints for the A.I
    public List<Transform> patrolWaypoints = new List<Transform>();
    #endregion

    private void Awake()
    {
        //Set the player variable to the player in the scene
        player = GameObject.FindGameObjectWithTag("player");

        gameMng = GameObject.Find("GameManager").GetComponent<GameManagerHandler>();
        pathHnd = GameObject.Find("Pathfinder").GetComponent<PathfindingHandler>();

        //Set the agentRigidBody variable to the rigidbody attached to the gameobject which the handler is attached to
        agentRigidBody = gameObject.GetComponent<Rigidbody>();

        //Initialize the new behavior tree
        test = new BehaviorTree(this);

        //Add the root to the behavior tree
        test.AddRoot();

        //Add Sequence selector to handle the attack sequence
        test.DynamicAddNode(new SequenceSelector(0, "attackSequence", this), "root");

        //Attack Sequence Leaf Nodes
        test.DynamicAddNode(new VisionLeaf(0, "vision", this), "attackSequence");
        test.DynamicAddNode(new ChaseLeaf(1, "chase", this), "attackSequence");
        test.DynamicAddNode(new OpenDoorLeaf(2, "doorAttack", this), "attackSequence");
        test.DynamicAddNode(new AttackLeaf(3, "attack", this), "attackSequence");

        //Patrol Sequence
        test.DynamicAddNode(new SequenceSelector(1, "patrolSequence", this), "root");

        //Patrol Sequence Leaf Nodes
        test.DynamicAddNode(new OpenDoorLeaf(0, "doorPatrol", this), "patrolSequence");
        test.DynamicAddNode(new PatrolLeaf(1, "patrol", this), "patrolSequence");
    }

    private void Update()
    {
        if(GameMng.gameManagerInstance.currentPauseState == GameManager.PauseState.paused)
        {
            isPaused = true;
        }

        else if(GameMng.gameManagerInstance.currentPauseState == GameManager.PauseState.unpaused)
        {
            isPaused = false;
        }

        if(isPaused == false)
        {
            Debug.Log("working");
            //Reset the nodes in the tree
            test.MassNodeReset();

            //Run through the behaviors of the tree
            test.RunThroughTree();
        }

        else if (isPaused == true)
        {
            Debug.Log("Paused");
        }
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

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "doorSpace")
        {
            isInTrigger = false;
        }
    }
}
