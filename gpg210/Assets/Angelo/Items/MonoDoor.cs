using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoDoor : MonoItem
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
    private float doorOpenValue;

    [SerializeField]
    private float doorClosedValue;

    [SerializeField]
    private float smoothing;
    //Desired smoothing value

    [SerializeField]
    private bool doorIsLocked;

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

    public override void CurrentBehavior()
    {
        if(currentState == State.closed)
        {
            currentState = State.open;
        }
        else if(currentState == State.open)
        {
            currentState = State.closed;
        }
    }

    private void Awake()
    {
        currentState = State.closed;
    }

    private void Update()
    {
       if(doorIsLocked == false)
        {
            if (currentState == State.open)
            {
                Quaternion target = Quaternion.Euler(0, doorOpenValue, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smoothing);
            }
            else if (currentState == State.closed)
            {
                Quaternion target2 = Quaternion.Euler(0, doorClosedValue, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, target2, Time.deltaTime * smoothing);
            }
        }
    }
}
