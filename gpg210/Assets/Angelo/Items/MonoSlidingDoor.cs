using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSlidingDoor : MonoItem
{
    private enum State
    {
        open,
        closed
    }
    //States for the door

    private State currentState;
    //Track the current state of the door

    [SerializeField]
    private float smoothing;
    //Desired smoothing value

    [SerializeField]
    private bool doorIsLocked;

    [SerializeField]
    private Vector3 target;

    [SerializeField]
    private Vector3 target2;


    public bool DoorIsLocked
    {
        get
        {
            return doorIsLocked;
        }

        set
        {
            doorIsLocked = value;
        }
    }

    private void Awake()
    {
        currentState = State.closed;
        target = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
    }

    public override void CurrentBehavior()
    {
        if (doorIsLocked == false)
        {
            if (currentState == State.closed)
            {
                currentState = State.open;
            }
            else if (currentState == State.open)
            {
                currentState = State.closed;
            }
        }
    }

    private void Update()
    {
        if (currentState == State.open)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smoothing);
        }
        else if (currentState == State.closed)
        {
            transform.position = Vector3.Lerp(transform.position, target2, Time.deltaTime * smoothing);
        }
    }
}
