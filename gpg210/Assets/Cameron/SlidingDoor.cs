using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {

    public Transform pos1;
    public Transform pos2;

    public float speed;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        speed = 0.0001f * Time.deltaTime;
        Vector3 Start = pos1.position;
        Vector3 end = pos2.position;
        transform.position = Vector3.Lerp(Start, end, speed);
        //Vector3 centre = (pos1 + pos2) * 1f;
        //Vector3.Lerp(pos1, pos2, speed);
		
	}

}
