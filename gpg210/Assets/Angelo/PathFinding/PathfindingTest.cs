using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTest : MonoBehaviour
{
    public PathfindingMap temp;
    public PathfindingNode start;
    public PathfindingNode end;
    public List<PathfindingNode> path;

    private void Awake()
    {
        path = PathfindingHandler.CreateAPath(temp.map, start, end);
    }
}
