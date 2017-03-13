using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMap : MonoBehaviour
{
    public List<GameObject> map = new List<GameObject>();

    private void Awake()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        map.AddRange(GameObject.FindGameObjectsWithTag("node"));
    }

    private void Update()
    {

    }
}
