using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoDoor : MonoItem
{
    private bool triggerDoorAction;

    public bool TriggerDoorAction
    {
        get
        {
            return triggerDoorAction;
        }

        set
        {
            triggerDoorAction = value;
        }
    }

    private enum State
    {
        open,
        closed
    }
    //States for the door

    private State currentState;
    //Track the current state of the door

    private float doorOpenValue = 90f;
    //Desired angle for door open

    private float doorCloseValue = 0f;
    //Desired angle for door close

    private float smoothing = 2f;
    //Desired smoothing value

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

    private void Start()
    {
        currentState = State.closed;
    }

    private void Update()
    {
        if (currentState == State.open)
        {
            Quaternion target = Quaternion.Euler(0, doorOpenValue, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smoothing);
        }
        else if (currentState == State.closed)
        {
            Quaternion target2 = Quaternion.Euler(0, doorCloseValue, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target2, Time.deltaTime * smoothing);
        }
    }
}
