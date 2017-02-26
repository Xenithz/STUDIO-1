using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    //target will contain the transform of the desired target GameObject
    public Transform target;

    //angle will contain the angle between the direction of the enemy and player, and the front direction of the agent
    public float angle;

    //enemyFieldOfView will contain the fixed FOV of the enemy(can only see the player in that angle)
    public float enemyFieldOfView = 100f;

    //directionbetween enemy and player will store the normalized magnitude (direction) of the agent to the target
    public Vector3 directionBetweenEnemyAndPlayer;

	// Update is called once per frame
	void Update ()
    {
        //speed will store the desired rotation speed
        float speed = 3f * Time.deltaTime;

        //Calculate the magnitude and then normalize to get the direction
        directionBetweenEnemyAndPlayer = (target.transform.position - transform.position).normalized;

        //Draw line in the direction
        Debug.DrawLine(transform.position, transform.position + directionBetweenEnemyAndPlayer * 5, Color.red, 0.5f);

        //calculate the angle between the direction of the agent to the target, and the forward direction of the agent
        angle = Vector3.Angle(directionBetweenEnemyAndPlayer, transform.forward);

        //Create a new quaternion with the specified direction
        Quaternion lookAt = Quaternion.LookRotation(directionBetweenEnemyAndPlayer);

        //If the enemyFieldOfView is larger than the angle, then the target is inside the agent's field of view
        if(angle < enemyFieldOfView * 0.5)
        {
            //Interpolate from the current rotation to the quaternion look at by the speed, and normalize
            transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, speed);

            //Debug that the player is inside the FOV of the agent
            Debug.Log("is in FOV");
        }
	}
}
