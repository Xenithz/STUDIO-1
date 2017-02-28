using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    /*
        The AudioManager class is a class which will handle all audio implementations inside of the game     
    */

    //Private constructor for singleton implementation
    private AudioManager()
    {

    }

    //Premade object of the AudioManager which will hold the instance
    private static AudioManager audioManagerInstance;
    
    //Public property to access the instance
    public static AudioManager AudioManagerInstance
    {
        get
        {
            //Check if the instance does not exist
            if (audioManagerInstance == null)
            {
                //Instantiate it as a new audio manager
                audioManagerInstance = new AudioManager();
            }

            return audioManagerInstance;
        }
    }

    //Array to hold all the footstep audioclips for the game
    public AudioClip[] footStepAudioClips;
    
    //Public enum to hold the footstep types inside the game
    public enum FootStepTypes
    {
        gravel,
        metal
    }
    
    //Tracks the current footstep type
    public FootStepTypes currentFootStepType;

    //Bool to check if input key is down
    private bool inputKeyIsDown;

    //Bool to check if the input key is already down
    private bool inputKeyIsAlreadyDown;

    //Array of bools to track if keys have been pressed
    private bool[] KeyBools;

    //Audio source to play sound
    public AudioSource audioSource1;

    //Audio index to iterate through array
    public int audioIndex;

    //Array to store environment audio clips
    private AudioClip[] environmentAudioClips;

    public void FootStepCues(AudioSource audioSource)
    {
        KeyBools = new bool[]
            {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.D)
            };
        if (KeyBools[0] || KeyBools[1] || KeyBools[2] || KeyBools[3])
        {
            inputKeyIsDown = true;
            if (!audioSource.isPlaying)
            {
                if (currentFootStepType == FootStepTypes.gravel)
                {
                    if (audioIndex  > 3)
                    {
                        audioIndex = 0;
                    }
                }
                else if (currentFootStepType == FootStepTypes.metal && audioIndex > 7)
                {
                    audioIndex = 4;
                }
                audioSource.clip = footStepAudioClips[audioIndex];
                audioSource.Play();
                audioIndex++;
            }
        }
        if (inputKeyIsDown && !inputKeyIsAlreadyDown)
        {
            inputKeyIsAlreadyDown = true;
        }
        if (!KeyBools[0] && !KeyBools[1] && !KeyBools[2] && !KeyBools[3] && inputKeyIsAlreadyDown)
        {
            audioSource.Stop();
            if (currentFootStepType == FootStepTypes.gravel)
            {
                audioIndex = 0;
            }
            else if (currentFootStepType == FootStepTypes.metal)
            {
                audioIndex = 4;
            }
            audioSource.clip = footStepAudioClips[audioIndex];
            inputKeyIsDown = false;
            inputKeyIsAlreadyDown = false;
        }
    }

    public void SetCurrentFootSteps(FootStepTypes desiredFootStepType)
    {
        currentFootStepType = desiredFootStepType;
    }

    public void InitialFootStepSetting()
    {
        if (currentFootStepType == FootStepTypes.gravel)
        {
            this.audioIndex = 0;
        }
        else if (currentFootStepType == FootStepTypes.metal)
        {
            this.audioIndex = 4;
        }
        //this.audio = base.GetComponent<AudioSource>();
        //this.audio.clip = this.footstepAudioClips[this.audioIndex];
    }

    public void EnvironmentCue(int environmentSoundIndex, Collider audioCollider)
    {
        AudioSource temporaryAudioSource;

        temporaryAudioSource = audioCollider.gameObject.GetComponent<AudioSource>();
        temporaryAudioSource.clip = environmentAudioClips[environmentSoundIndex];
        temporaryAudioSource.Play();
    }
}
