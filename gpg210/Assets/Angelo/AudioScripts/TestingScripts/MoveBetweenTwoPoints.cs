using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenTwoPoints : MonoBehaviour
{
    public GameObject pointB;
    //Initialize game object variable to hold waypoint b

    public GameObject pointA;
    //Initialize game object variable to hold waypoint a

    IEnumerator Start()
    {
        while (true)
        //Endlessly loop movement
        {
            yield return StartCoroutine(MoveObject(transform, pointA.transform.position, pointB.transform.position, 3f));
            //Start coroutine move object with end point: point b.
            yield return StartCoroutine(MoveObject(transform, pointB.transform.position, pointA.transform.position, 3f));
            //Start coroutine move object with end point: point a
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;

            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            //Lerp between two points

            yield return null;
        }
    }
}
