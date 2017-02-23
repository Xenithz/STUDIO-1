using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private AudioManager()
    {

    }

    private static AudioManager audioManagerInstance;

    public static AudioManager AudioManagerInstance
    {
        get
        {
            if (audioManagerInstance == null)
            {
                audioManagerInstance = new AudioManager();
            }

            return audioManagerInstance;
        }
    }

    public AudioClip[] footStepAudioClips;
    
    public enum FootStepTypes
    {
        gravel,
        metal
    }

    private FootStepTypes currentFootStepType;

    private bool inputKeyIsDown;

    private bool inputKeyIsAlreadyDown;

    private bool[] KeyBools;

    private AudioSource audioSource;

    private int audioIndex;

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

    public void EnvironmentCue(string environmentSoundName)
    {
        
    }
}
