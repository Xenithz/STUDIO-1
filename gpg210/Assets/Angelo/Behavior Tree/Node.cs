using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IComparable<Node>
{
    /*
        The Node class is the base class for all the different types of nodes in the behavior tree. It will carry all the common characteristics
        of nodes and common functions for nodes.
    */

    //Enum NodeStates will contain different states that a Node can be in
    public enum NodeStates
    {
        ready,
        running,
        success,
        failed
    }

    //currentState will track the current state of a Node
    private NodeStates currentState;

    //Public property which will allow the currentState variable to be kept private
    public NodeStates CurrentState
    {
        get
        {
            return this.currentState;
        }

        set
        {
            this.currentState = value;
        }
    }

    //nodePriority will be used to give nodes a priority for sorting
    private int nodePriority;

    //Public property which will allow the nodePriority variable to be kept private
    public int NodePriority
    {
        get
        {
            return this.nodePriority;
        }

        set
        {
            this.nodePriority = value;
        }
    }

    //nodeName will be used to give names a node for search capabilities via linq
    private string nodeName;

    //Public property will allow the nodeName variable to be kept private
    public string NodeName
    {
        get
        {
            return this.nodeName;
        }

        set
        {
            this.nodeName = value;
        }
    }

    //nodeHandler will be used to store access to all variables that the A.I stores in it's handler script
    private Handler nodeHandler;

    //NodeHandler will allow the nodeHandler to be kept private
    public Handler NodeHandler
    {
        get
        {
            return this.nodeHandler;
        }

        set
        {
            this.nodeHandler = value;
        }
    }

    //Node Constructor
    public Node(int desiredPriority, string desiredName, Handler desiredNodeHandler)
    {
        NodePriority = desiredPriority;
        NodeName = desiredName;
        NodeHandler = desiredNodeHandler;
    }

    //List of Nodes which will hold the children of a parent
    public List<Node> Children = new List<Node>();

    //Compare the NodePriority for sorting (make new custom comparer)
    public int CompareTo(Node nodeToCompare)
    {
        //Compare the current node priority to the nodeToCompare's node priority
        return NodePriority.CompareTo(nodeToCompare.NodePriority);
    }

    //SetNodeStatus function will set the current state of a Node
    public void SetNodeStatus(NodeStates stateToSet)
    {
        this.CurrentState = stateToSet;
    }

    //BoolCheckNodeState will return a bool indicating if the current state of a Node is equals to stateToCheck
    public bool BoolCheckNodeState(NodeStates stateToCheck)
    {
        return CurrentState.Equals(stateToCheck);
    }
    
    //AddChild will add a Node to current Node's list of children
    public void AddChild(Node nodeToAdd)
    {
        this.Children.Add(nodeToAdd);
        this.Children.Sort();
    }

    //Virtual NodeBehavior function will be overriden in other Node classes to provide different Behaviors for Nodes
    public virtual void NodeBehavior()
    {

    }
}
