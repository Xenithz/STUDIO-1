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

    public enum DoorType
    {
        wood,
        nonwood
    }

    public DoorType currentDoorType;

    [SerializeField]
    private float doorOpenValue;

    [SerializeField]
    private float doorClosedValue;

    [SerializeField]
    private float smoothing;
    //Desired smoothing value

    public AudioClip[] doorClips;

    public AudioSource doorAudioSource;

    public float Smoothing
    {
        get
        {
            return smoothing;
        }

        set
        {
            smoothing = value;
        }
    }

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
        if(doorIsLocked == false)
        {
            if(currentDoorType == DoorType.wood)
            {
                if (currentState == State.closed)
                {
                    doorAudioSource.clip = doorClips[0];
                    doorAudioSource.time = 1.7f;
                    doorAudioSource.Play();
                    currentState = State.open;
                }
                else if (currentState == State.open)
                {
                    doorAudioSource.clip = doorClips[1];
                    doorAudioSource.time = 0.1f;
                    doorAudioSource.Play();
                    currentState = State.closed;
                }
            }

            else if(currentDoorType == DoorType.nonwood)
            {
                if (currentState == State.closed)
                {
                    doorAudioSource.clip = doorClips[3];
                    doorAudioSource.Play();
                    currentState = State.open;
                }
                else if (currentState == State.open)
                {
                    doorAudioSource.clip = doorClips[3];
                    doorAudioSource.Play();
                    currentState = State.closed;
                }
            }
        }

        if(doorIsLocked == true)
        {
            doorAudioSource.clip = doorClips[2];
            doorAudioSource.time = 0.3f;
            doorAudioSource.Play();
        }
    }

    private void Awake()
    {
        currentState = State.closed;
    }

    private void Update()
    {
        if (currentState == State.open)
        {
            Quaternion target = Quaternion.Euler(0, doorOpenValue, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, target, Time.deltaTime * smoothing);
        }
        else if (currentState == State.closed)
        {
            Quaternion target2 = Quaternion.Euler(0, doorClosedValue, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target2, Time.deltaTime * smoothing);
        }
    }

    public void Creak()
    {
        doorAudioSource.clip = doorClips[4];
        doorAudioSource.time = 0.4f;
        doorAudioSource.Play();
    }

    public void BehaviorNoAudio()
    {
        if (currentState == State.closed)
        {
            currentState = State.open;
        }
    }
}
