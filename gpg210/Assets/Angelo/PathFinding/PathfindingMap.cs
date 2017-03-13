using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfindingMap : MonoBehaviour
{
    public List<PathfindingNode> map = new List<PathfindingNode>();
    public List<GameObject> nodes = new List<GameObject>();

    #region
    private PathfindingNode first;
    private PathfindingNode second;
    private PathfindingNode third;
    private PathfindingNode fourth;
    private PathfindingNode fifth;
    private PathfindingNode sixth;
    private PathfindingNode seventh;
    private PathfindingNode eigth;
    private PathfindingNode ninth;
    #endregion

    private void Awake()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        nodes.AddRange(GameObject.FindGameObjectsWithTag("node"));
        
        foreach(GameObject v in nodes)
        {
            map.Add(new PathfindingNode(v.transform));
        }

        first = FindAndSet("first");
        second = FindAndSet("second");
        third = FindAndSet("third");
        fourth = FindAndSet("fourth");
        fifth = FindAndSet("fifth");
        sixth = FindAndSet("sixth");
        seventh = FindAndSet("seventh");
        eigth = FindAndSet("eigth");
        ninth = FindAndSet("ninth");

        first.AddLinkedNode(second);
        second.AddLinkedNode(ninth);
        second.AddLinkedNode(eigth);
        ninth.AddLinkedNode(sixth);
        eigth.AddLinkedNode(seventh);
        sixth.AddLinkedNode(first);
        seventh.AddLinkedNode(fourth);
        sixth.AddLinkedNode(fifth);
        fourth.AddLinkedNode(third);
        fifth.AddLinkedNode(third);
        third.AddLinkedNode(first);
    }

    private void Update()
    {
        Debug.DrawLine(first.nodeTransform.position, first.linkedNodes[0].nodeTransform.position);
        Debug.DrawLine(second.nodeTransform.position, second.linkedNodes[0].nodeTransform.position);
        Debug.DrawLine(second.nodeTransform.position, second.linkedNodes[1].nodeTransform.position);
        Debug.DrawLine(ninth.nodeTransform.position, ninth.linkedNodes[0].nodeTransform.position);
        Debug.DrawLine(eigth.nodeTransform.position, eigth.linkedNodes[0].nodeTransform.position);
        Debug.DrawLine(sixth.nodeTransform.position, sixth.linkedNodes[0].nodeTransform.position);
        Debug.DrawLine(sixth.nodeTransform.position, sixth.linkedNodes[1].nodeTransform.position);
        Debug.DrawLine(seventh.nodeTransform.position, seventh.linkedNodes[0].nodeTransform.position);
        Debug.DrawLine(fourth.nodeTransform.position, fourth.linkedNodes[0].nodeTransform.position);
        Debug.DrawLine(fifth.nodeTransform.position, fifth.linkedNodes[0].nodeTransform.position);
        Debug.DrawLine(third.nodeTransform.position, third.linkedNodes[0].nodeTransform.position);
    }

    private PathfindingNode FindAndSet(string desiredNode)
    {
        PathfindingNode temp = map.FirstOrDefault(o => o.nodeName == desiredNode);
        return temp;
    }
}
