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

        //Getting closest node to the end transform
        PathfindingNode endingNode = GetClosestNode(end);

        //Open list
        List<PathfindingNode> q = new List<PathfindingNode>();

        foreach(PathfindingNode v in map)
        {
            //Set the g cost to infinity
            v.gCost = Mathf.Infinity;

            //set the parent to null
            v.parent = null;

            //add all the nodes to the open list
            q.Add(v);
        }

        startingNode.gCost = 0;

        while(q.Count > 0)
        {
            PathfindingNode u = GiveMeAnImportantNode(q);
            q.Remove(u);

            if(u == endingNode)
            {
                Stack<Transform> transforms = new Stack<Transform>();

                while(u.parent != null)
                {
                    transforms.Push(u.nodeTransform);
                    u = u.parent;
                }

                return transforms.Peek();
            }

            foreach(PathfindingNode v in u.linkedNodes)
            {
                float alt = u.gCost + (u.nodeTransform.position - v.nodeTransform.position).magnitude;

                if (alt < v.gCost)
                {
                    v.gCost = alt;
                    v.parent = u;
                }
            }
        }

        return null;
    }

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
