using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool CanOpen;
    public bool isOpen;
    public bool ButtonPressed;
    public bool ButtonPressedToClose;
    public float Delay;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanOpen = true;
        }
        else
        {
            CanOpen = false;
        }
    }

    void Update()
    {
        if (isOpen == false)
        {
            if (CanOpen == true)
            {
                if (Input.GetKeyDown(KeyCode.E) && Delay == 0f)
                {
                    ButtonPressed = true;
                }
                if (ButtonPressed == true)
                {
                    Quaternion DoorOpen = Quaternion.Euler(0, 90f, 0);
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, DoorOpen, Time.deltaTime * 2.5f);

                    if (transform.localRotation == Quaternion.Euler(0, 90f, 0))
                    {
                        isOpen = true;
                        ButtonPressed = false;
                    }
                    Delay = +Time.deltaTime;
                    Delay = 0f;
                }
            }
        }

        if (isOpen == true)
        {
            if (CanOpen == true)
            {
                if (Input.GetKeyDown(KeyCode.E) && Delay == 0f)
                {
                    ButtonPressedToClose = true;
                }
                if (ButtonPressedToClose == true)
                {
                    Quaternion DoorClosed = Quaternion.Euler(0, 0, 0);
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, DoorClosed, Time.deltaTime * 2.5f);

                    if (transform.localRotation == Quaternion.Euler(0, 0, 0))
                    {
                        isOpen = false;
                        ButtonPressedToClose = false;
                        Delay = 0f;
                    }
                    Delay = +Time.deltaTime;
                    Delay = 0f;
                }
            }
        }

    }
}




