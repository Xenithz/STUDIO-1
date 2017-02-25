using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BehaviorTree
{
    /*
         The Behavior Tree class is the class which contains functions for operation and creation of the tree.
    */

    //Behavior Tree will always contain a root node as a base
    private Node root;

    //Public property which will allow the root Node to be kept as a private variable
    public Node Root
    {
        get
        {
            return root;
        }

        set
        {
            root = value;
        }
    }

    //List of ALL nodes being stored in the tree. This will allow for searching according to name.
    public List<Node> treeNodeCollection = new List<Node>();

    //Behavior Tree constructor
    public BehaviorTree()
    {

    }
    
    //AddRoot will add the root to the tree. Incase the root already exists, it will notify the user to use the DynamicAddNode function instead.
    public void AddRoot()
    {
        //Check if the root is not null
        if(Root != null)
        {
            //If the root is not null then the root exists, so debug that the root exists
            Debug.LogError("ROOT EXISTS. USE DYNAMICADDNODE FUNCTION");
        }
        else
        {
            //If it is null, add a new root to the tree
            Root = new PrioritySelector(1, "root");

            //Add the new root to the full list of nodes inside of the tree
            AddToTreeNodeCollection(Root);
        }
    }

    //DynamicAddNode will allow the user to add any type of node to any composite node which exists inside of the treeNodeCollection list
    public void DynamicAddNode (Node nodeToAdd, string parentName = null)
    {
        //Check if the root exists, and a parent name for searching has been specified
        if (Root != null && parentName != null)
        {
            //Search for the name of the specified parent inside the list of all nodes inside of the tree
            Node parentToRecieve = treeNodeCollection.FirstOrDefault(o => o.NodeName == parentName);
            
            //Add the new node to the specified parent
            parentToRecieve.AddChild(nodeToAdd);

            //Add the new node to the list of all the nodes inside the tree
            AddToTreeNodeCollection(nodeToAdd);
        }

        //Else check if the root is null
        else if(Root == null)
        {
            //Debug that the user request is being overriden
            Debug.LogError("OVERRIDING REQUEST, ROOT IS NULL AND HAS TO BE ADDED");

            //Override and add a new root
            Root = new PrioritySelector(1, "root");

            //Add the new root to the full list of nodes inside of the tree
            AddToTreeNodeCollection(Root);
        }

        //Else check if the specified parent is null
        else if(parentName == null)
        {
            //Debug that the user has to add a parent name
            Debug.LogError("INVALID PARENT. PLEASE ADD A PARENT NAME FOR SEARCHING");
        }
    }

    //Reset the nodes in treeNodeCollection list to ready according to their nodeState
    public void MassNodeReset()
    {
        //Iterate through the whole list of nodes inside of the tree
        for(int i = 0; i < treeNodeCollection.Count; i++)
        {
            //Check if the node at the index i has the state: failed, ready, success
            if (treeNodeCollection[i].BoolCheckNodeState(Node.NodeStates.failed) || treeNodeCollection[i].BoolCheckNodeState(Node.NodeStates.ready) || treeNodeCollection[i].BoolCheckNodeState(Node.NodeStates.success))
            {
                //Set the node state to ready
                treeNodeCollection[i].SetNodeStatus(Node.NodeStates.ready);
            }
        }
    }

    //Run through the whole tree by running the NodeBehavior of the root (priority selector)
    public void RunThroughTree(Handler agent, bool isInTrigger, Collider doorCollider, float doorOpenTimer, GameObject player, float distanceToPlayer, float angle, float enemyFieldOfView, float rotationSpeed, Vector3 directionBetweenEnemyAndPlayer)
    {
        //Call the nodebehavior of the root
        this.Root.NodeBehavior(agent, isInTrigger, doorCollider, doorOpenTimer, player, distanceToPlayer, angle, enemyFieldOfView, rotationSpeed, directionBetweenEnemyAndPlayer);
    }

    //AddToTreeNodeCollection will add a node to the treeNodeCollectionList
    private void AddToTreeNodeCollection(Node nodeToAdd)
    {
        //Add the node to the treecollection
        treeNodeCollection.Add(nodeToAdd);
    }
}
