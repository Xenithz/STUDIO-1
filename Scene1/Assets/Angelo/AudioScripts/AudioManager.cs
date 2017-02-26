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
    private FootStepTypes currentFootStepType;

    //Bool to check if input key is down
    private bool inputKeyIsDown;

    //Bool to check if the input key is already down
    private bool inputKeyIsAlreadyDown;

    //Array of bools to track if keys have been pressed
    private bool[] KeyBools;

    //Audio source to play sound
    private AudioSource audioSource;

    //Audio index to iterate through array
    private int audioIndex;

    //Array to store environment audio clips
    private AudioClip[] environmentAudioClips;

    public void FootStepCues()
    {
        this.KeyBools = new bool[]
            {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.D)
            };
        if (this.KeyBools[0] || this.KeyBools[1] || this.KeyBools[2] || this.KeyBools[3])
        {
            this.inputKeyIsDown = true;
            if (!this.audioSource.isPlaying)
            {
                if (this.currentFootStepType == FootStepTypes.gravel)
                {
                    if (this.audioIndex  > 3)
                    {
                        this.audioIndex = 0;
                    }
                }
                else if (this.currentFootStepType == FootStepTypes.metal && this.audioIndex > 7)
                {
                    this.audioIndex = 4;
                }
                this.audioSource.clip = this.footStepAudioClips[this.audioIndex];
                this.audioSource.Play();
                this.audioIndex++;
            }
        }
        if (this.inputKeyIsDown && !this.inputKeyIsAlreadyDown)
        {
            this.inputKeyIsAlreadyDown = true;
        }
        if (!this.KeyBools[0] && !this.KeyBools[1] && !this.KeyBools[2] && !this.KeyBools[3] && this.inputKeyIsAlreadyDown)
        {
            this.audioSource.Stop();
            if (this.currentFootStepType == FootStepTypes.gravel)
            {
                this.audioIndex = 0;
            }
            else if (this.currentFootStepType == FootStepTypes.metal)
            {
                this.audioIndex = 4;
            }
            this.audioSource.clip = this.footStepAudioClips[this.audioIndex];
            this.inputKeyIsDown = false;
            this.inputKeyIsAlreadyDown = false;
        }
    }

    public void SetCurrentFootSteps(FootStepTypes desiredFootStepType)
    {
        this.currentFootStepType = desiredFootStepType;
    }

    public void EnvironmentCue(int environmentSoundIndex, Collider audioCollider)
    {
        AudioSource temporaryAudioSource;

        temporaryAudioSource = audioCollider.gameObject.GetComponent<AudioSource>();
        temporaryAudioSource.clip = environmentAudioClips[environmentSoundIndex];
        temporaryAudioSource.Play();
    }
}
