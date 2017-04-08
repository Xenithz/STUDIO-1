using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    // point where cmaera will be
    public float midpoint = 0.5f;



    // Speed of bobbing
    public float bobSpeed;

    // up and down movement of camera
    public float bobAmount = 0.15f;

    public bool cameraMoving;

    // Timer (how many times you are moving)
    public float timer = 0.0f;

    PlayerControllerV5 fps;

    // Use this for initialization
    void Start()
    {

        fps = GetComponentInParent<PlayerControllerV5>();
    }

    // Update is called once per frame
    void Update()
    {
        // wave setting zero per frame 
        float wave = 0.0f;

        // Poisition of parent object (capsule or player)
        Vector3 parentPosition = transform.localPosition;


        // getkey funcition is false and character is not running
        if (fps.RightLeft == 0 && fps.FrontBack == 0)
        {
            // then Timer is eqaul to zero 
            timer = 0;


        }
        // else 
        else
        {
            // setting value wave which will follow the sin curve has per the value of timer is being added on
            wave = Mathf.Sin(timer);
            // adding value of timer and speed of camera movement 
            timer = timer + bobSpeed;

            //            
        }


        // if parent speed is 200
        if (fps.speed == 200.0f)
        {
            // then bob speed is doubled
            bobSpeed = 0.15f * 2.0f;
        }
        else
        {
            // else bob speed remains same
            bobSpeed = 0.15f;
        }






        //Checking if the wave value has reached to its lowest value  

        //if wave or the point in graph is not equal to zero
        if (wave != 0)
        {


            // setting camera movement by multiplying the values of total axes,value of wave and bob amount 
            float cameraMovement = wave * bobAmount;

            // setting midepoint of the camera and movement in the y axis of parent position 
            parentPosition.y = midpoint + cameraMovement;
        }
        else
        {
            // if wave = zero then setting midpoint of the camera in the y axis of parent position without any movement 
            parentPosition.y = midpoint;
        }

        // setting transform position in relation to parent position per frame
        transform.localPosition = parentPosition;
    }

    // Refernce : https://www.youtube.com/watch?v=lBqxcznu9T8

}
