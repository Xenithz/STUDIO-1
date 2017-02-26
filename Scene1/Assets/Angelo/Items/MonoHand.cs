using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoHand : MonoBehaviour
{
    public float playerDetectionRadius = 360f;

    public Transform targetTransform;

    public Vector3 directionFromPlayerToItem;

    public float angle;

    public float distanceFromPlayerToItem;

    private void Update()
    {
        directionFromPlayerToItem = Vector3.Normalize(targetTransform.position - transform.position);
        distanceFromPlayerToItem = Vector3.Distance(targetTransform.transform.position, transform.position);

        angle = Vector3.Angle(directionFromPlayerToItem, transform.forward);

        Debug.DrawLine(transform.position, transform.position + directionFromPlayerToItem * 5, Color.red, 0.5f);

        if(distanceFromPlayerToItem < 3.5f && angle <= 25f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                targetTransform.gameObject.GetComponent<MonoItem>().CurrentBehavior();
            }
        }
    }
}
