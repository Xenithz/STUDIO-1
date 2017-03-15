using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingHandler : MonoBehaviour
{
    public List<PathfindingNode> map = new List<PathfindingNode>();
    public List<GameObject> nodes = new List<GameObject>();
    public LayerMask layer;

    private void Awake()
    {
        CreateMap();
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

                if(!Physics.Raycast(v.nodeTransform.position, map[i].nodeTransform.position, out rayHit, distance, layer))
                {
                    v.AddLinkedNode(map[i]);
                }

                else if (Physics.Raycast(v.nodeTransform.position, map[i].nodeTransform.position, out rayHit, distance, layer) && rayHit.collider.CompareTag("wall"))
                {
                }
            }
        }
    }

    public static void CreateAPath(Transform start, Transform end)
    {
    }
}
