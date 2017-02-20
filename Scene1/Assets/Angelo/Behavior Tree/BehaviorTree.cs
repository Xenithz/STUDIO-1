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
        if(Root != null)
        {
            Debug.LogError("ROOT EXISTS. USE DYNAMICADDNODE FUNCTION");
        }
        else
        {
            Root = new PrioritySelector(1, "root");
            AddToTreeNodeCollection(Root);
        }
    }

    //DynamicAddNode will allow the user to add any type of node to any composite node which exists inside of the treeNodeCollection list
    public void DynamicAddNode (Node nodeToAdd, string parentName = null)
    {
        if (Root != null && parentName != null)
        {
            Node parentToRecieve = treeNodeCollection.FirstOrDefault(o => o.NodeName == parentName);
            parentToRecieve.AddChild(nodeToAdd);
            AddToTreeNodeCollection(nodeToAdd);
        }
        else if(Root == null)
        {
            Debug.LogError("OVERRIDING REQUEST, ROOT IS NULL AND HAS TO BE ADDED");
            Root = new PrioritySelector(1, "root");
            AddToTreeNodeCollection(Root);
        }
        else if(parentName == null)
        {
            Debug.LogError("INVALID PARENT. PLEASE ADD A PARENT NAME FOR SEARCHING");
        }
    }

    public void MassNodeReset()
    {
        for(int i = 0; i < treeNodeCollection.Count; i++)
        {
            if (treeNodeCollection[i].BoolCheckNodeState(Node.NodeStates.failed) || treeNodeCollection[i].BoolCheckNodeState(Node.NodeStates.ready) || treeNodeCollection[i].BoolCheckNodeState(Node.NodeStates.success))
            {
                treeNodeCollection[i].SetNodeStatus(Node.NodeStates.ready);
            }
        }
    }

    //AddToTreeNodeCollection will add a node to the treeNodeCollectionList
    private void AddToTreeNodeCollection(Node nodeToAdd)
    {
        treeNodeCollection.Add(nodeToAdd);
    }
}
