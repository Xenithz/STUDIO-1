using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    // point where cmaera will be
    public float midpoint = 0.5f;



    // Speed of bobbing
    public float bobSpeed = 0.2f;

    // up and down movement of camera
    public float bobAmount = 0.15f;


    // Timer (how many times you are moving)
    public float timer = 0.0f;

    public PlayerControllerV5 fps;

    public GameObject player;

    // Use this for initialization
    void Start()
    {

        fps = player.GetComponent<PlayerControllerV5>();
    }

    // Update is called once per frame
    void Update()
    {
        // wave setting zero per frame 
        float wave = 0.0f;

        // Poisition of parent object (capsule or player)
        Vector3 parentPosition = transform.localPosition;

        // Getting axis 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // getkey funcition is false and character is not running
        if (!Input.GetKey(KeyCode.LeftShift) && fps.isRunning == false)
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

            // timer value is greater than the value of square of pie 
            if (timer > Mathf.PI * 2)
            {
                // then timer should be subtracted from the value of pie sqaure 
                timer = timer - (Mathf.PI * 2);
            }
        }


        //Checking if the wave value has reached to its lowest value  

        //if wave or the point in graph is not equal to zero
        if (wave != 0)
        {
            // adding absolute value of both the axis 
            float totalAxis = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

            // Clamping the value of total Axes between 0.0f and 1.0f; 
            totalAxis = Mathf.Clamp(totalAxis, 0.0f, 1.0f);

            // setting camera movement by multiplying the values of total axes,value of wave and bob amount 
            float cameraMovement = totalAxis * wave * bobAmount;

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

}
