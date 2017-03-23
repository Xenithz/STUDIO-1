using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingHandler : MonoBehaviour
{
    public List<PathfindingNode> map = new List<PathfindingNode>();
    public List<GameObject> nodes = new List<GameObject>();
    public LayerMask layer;

    public Transform test;

    private void Awake()
    {
        CreateMap();
    }

    private void Update()
    {
        foreach(PathfindingNode v in map)
        {
            for(int i = 0; i < v.linkedNodes.Count; i++)
            {
                Debug.DrawLine(v.nodeTransform.position, v.linkedNodes[i].nodeTransform.position);  
            }
        }
    }

    private void CreateMap()
    {
        nodes.AddRange(GameObject.FindGameObjectsWithTag("node"));

        foreach (GameObject v in nodes)
        {
            map.Add(new PathfindingNode(v.transform));
        }

        foreach (PathfindingNode v in map)
        {
            for(int i = 0; i < map.Count; i++)
            {
                RaycastHit rayHit;
                float distance = Vector3.Distance(v.nodeTransform.position, map[i].nodeTransform.position);

                if (!Physics.Raycast(v.nodeTransform.position, Vector3.Normalize(map[i].nodeTransform.position - v.nodeTransform.position), out rayHit, distance, layer))
                {
                    v.AddLinkedNode(map[i]);
                }

                else if (Physics.Raycast(v.nodeTransform.position, Vector3.Normalize(map[i].nodeTransform.position - v.nodeTransform.position), out rayHit, distance, layer))
                {
                    Debug.Log("hit");
                }
            }
        }
    }

    public Transform CreateAPath(Transform start, Transform end)
    {
        //Getting closest node to start transform
        PathfindingNode startingNode = GetClosestNode(start);

        Debug.Log(startingNode.nodeTransform);

        //Getting closest node to the end transform
        PathfindingNode endingNode = GetClosestNode(end);

        //Open list
        List<PathfindingNode> openList = new List<PathfindingNode>();

        foreach(PathfindingNode node in map)
        {
            //Set the g cost to infinity
            node.gCost = Mathf.Infinity;

            //set the parent to null
            node.parent = null;

            //add all the nodes to the open list
            openList.Add(node);
        }


        //Set the starting node g cost to 0
        startingNode.gCost = 0;

        while(openList.Count > 0)
        {
            //Get the node with the lowest g cost inside the open list
            PathfindingNode currentNode = GiveMeAnImportantNode(openList);

            //Remove it from the open list because it is visited
            openList.Remove(currentNode);

            //Check if the node is the ending node
            if(currentNode == endingNode)
            {
                //create new list for the path
                List<Transform> path = new List<Transform>();

                //If the current node is not null
                while(currentNode != null)
                {
                    //Add the current node to the path
                    path.Add(currentNode.nodeTransform);

                    //Retrace by going back to the parent of the current node
                    currentNode = currentNode.parent;
                }

                //Reverse the list
                path.Reverse();
                path.Add(end);
                float distance = Vector3.Distance(path[1].position, start.position);

                if (!Physics.Raycast(start.position, Vector3.Normalize(path[1].position - start.position), distance, layer))
                {
                    path.Remove(path[0]);
                }


                return path[0];
            }

            foreach(PathfindingNode v in currentNode.linkedNodes)
            {
                float alt = currentNode.gCost + (currentNode.nodeTransform.position - v.nodeTransform.position).magnitude;

                if (alt < v.gCost)
                {
                    v.gCost = alt;
                    v.parent = currentNode;
                }
            }
        }

        return null;
    }

    //TODO OPTIMIZE CODE BY GETTING MIN

    public PathfindingNode GetClosestNode(Transform desiredTransform)
    {
        List<PathfindingNode> listForSorting = new List<PathfindingNode>();
        listForSorting.AddRange(map);

        listForSorting.Sort(delegate (PathfindingNode a, PathfindingNode b)
        {
            return Vector3.Distance(desiredTransform.position, a.nodeTransform.position).CompareTo(Vector3.Distance(desiredTransform.position, b.nodeTransform.position));
        });

        return listForSorting[0];
    }

    //public PathfindingNode GetClosestNode2(Transform desiredTransform)
    //{
    //    List<PathfindingNode> listForSorting = new List<PathfindingNode>();
    //    listForSorting.AddRange(map);

    //    listForSorting.Sort(delegate (PathfindingNode a, PathfindingNode b)
    //    {
    //        return Vector3.Distance(desiredTransform.position, a.nodeTransform.position).CompareTo(Vector3.Distance(desiredTransform.position, b.nodeTransform.position));
    //    });

    //    return listForSorting[1];
    //}

    public PathfindingNode GiveMeAnImportantNode(List<PathfindingNode> listToSort)
    {
        List<PathfindingNode> temporaryList = new List<PathfindingNode>();

        temporaryList.AddRange(listToSort);

        temporaryList.Sort(delegate (PathfindingNode a, PathfindingNode b)
        {
            return a.gCost.CompareTo(b.gCost);
        });

        return temporaryList[0];
    }
}
