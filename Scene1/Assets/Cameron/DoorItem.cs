using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorItem : Item
{
    public DoorItem(Transform thisItem) : base(thisItem)
    {

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

    public override void ItemBehavior(Transform thisItem)
    {

        Debug.Log("Working door");
        if (currentState == State.open)
        {
            currentState = State.closed;
            Quaternion target = Quaternion.Euler(0, doorOpenValue, 0);
            thisItem.localRotation = Quaternion.Slerp(thisItem.localRotation, target, Time.deltaTime * smoothing);
        }
        else if (currentState == State.closed)
        {
            currentState = State.open;
            Quaternion target2 = Quaternion.Euler(0, doorCloseValue, 0);
            thisItem.localRotation = Quaternion.Slerp(thisItem.localRotation, target2, Time.deltaTime * smoothing);
        }
    }

    public override void ItemStateSetOnAwake()
    {
        currentState = State.closed;
    }
}

