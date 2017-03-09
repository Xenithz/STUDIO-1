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

    public enum DoorType
    {
        front,
        side,
        front2,
        side2,
        front3,
        side3
    }

    public DoorType thisDoorType;

    private State currentState;
    //Track the current state of the door

    private float frontDoorOpenValue = 90f;
    //Desired angle for door open

    private float frontDoorCloseValue = 0f;
    //Desired angle for door close

    private float sideDoorOpenValue = 0f;

    private float sideDoorClosedValue = 90f;

    private float frontDoor2OpenValue = -90f;

    private float frontDoor2CloseValue = 0;

    private float sideDoor2OpenValue = 0;

    private float sideDoor2CloseValue = -90;

    private float frontDoor3OpenValue = 90;

    private float frontDoor3CloseValue = 180;

    private float sideDoor3CloseValue = -90f;

    private float sideDoor3OpenValue = -180f;

    private float doorOpenValue;

    private float doorClosedValue;

    private float smoothing = 2f;
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

        if (thisDoorType == DoorType.front)
        {
            doorOpenValue = frontDoorOpenValue;
            doorClosedValue = frontDoorCloseValue;
        }
        else if (thisDoorType == DoorType.side)
        {
            doorOpenValue = sideDoorOpenValue;
            doorClosedValue = sideDoorClosedValue;
        }
        else if(thisDoorType == DoorType.front2)
        {
            doorOpenValue = frontDoor2OpenValue;
            doorClosedValue = frontDoor2CloseValue;
        }
        else if(thisDoorType == DoorType.side2)
        {
            doorOpenValue = sideDoor2OpenValue;
            doorClosedValue = sideDoor2CloseValue;
        }
        else if(thisDoorType == DoorType.front3)
        {
            doorOpenValue = frontDoor3OpenValue;
            doorClosedValue = frontDoor3CloseValue;
        }
        else if(thisDoorType == DoorType.side3)
        {
            doorOpenValue = sideDoor3OpenValue;
            doorClosedValue = sideDoor3CloseValue;
        }
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
