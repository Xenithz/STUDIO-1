using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoHand : MonoBehaviour
{
    public Transform targetTransform;

    public Vector3 directionFromPlayerToItem;

    public float angle;

    public float distanceFromPlayerToItem;

    public List<GameObject> itemList;

    private void Awake()
    {
        itemList = new List<GameObject>();
        itemList.AddRange(GameObject.FindGameObjectsWithTag("item"));
    }

    private void Update()
    {
        itemList.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(this.transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(this.transform.position, b.transform.position));
        });

        targetTransform = itemList[0].transform;

        directionFromPlayerToItem = Vector3.Normalize(targetTransform.position - transform.position);
        distanceFromPlayerToItem = Vector3.Distance(targetTransform.transform.position, transform.position);

        angle = Vector3.Angle(directionFromPlayerToItem, transform.forward);

        Debug.DrawLine(transform.position, transform.position + directionFromPlayerToItem * 5, Color.red, 0.5f);

        if(distanceFromPlayerToItem < 4f && angle <= 40f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                targetTransform.gameObject.GetComponent<MonoItem>().CurrentBehavior();
            }
        }
    }
}
