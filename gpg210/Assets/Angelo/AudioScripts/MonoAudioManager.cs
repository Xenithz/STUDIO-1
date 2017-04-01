﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoAudioManager : MonoBehaviour
{
    public AudioClip[] footstepAudioClips;

    public AudioClip[] environmentAudioClips;

    private AudioSource audioSourceForPlayer;

    private bool keyIsDown;

    private bool keyAlreadyDown;

    private bool[] keys;

    private int audioIndex;

    public GameObject gameManager;

    public enum SoundType
    {
        wood,
        ceramic,
    }

    public SoundType soundType;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");

        audioSourceForPlayer = gameObject.GetComponent<AudioSource>();

        this.soundType = SoundType.wood;
        if (this.soundType == SoundType.wood)
        {
            this.audioIndex = 0;
        }
        else if (this.soundType == SoundType.ceramic)
        {
            this.audioIndex = 2;
        }
        this.audioSourceForPlayer = base.GetComponent<AudioSource>();
        this.audioSourceForPlayer.clip = this.footstepAudioClips[this.audioIndex];
    }

    private void Update()
    {
        if(gameManager.GetComponent<GameManagerHandler>().gameManagerInstance.currentPauseState != GameManager.PauseState.paused)
        {
            //Bool array to track key presses
            this.keys = new bool[]
                {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.D)
                };
            //If pressing key
            if (this.keys[0] || this.keys[1] || this.keys[2] || this.keys[3])
            {
                //Set bool for checking if the key is down to true
                this.keyIsDown = true;

                //Check if the audio is not playing
                if (!this.audioSourceForPlayer.isPlaying)
                {
                    //if its gravel
                    if (this.soundType == SoundType.wood)
                    {
                        //set to hardcoded value
                        if (this.audioIndex > 3)
                        {
                            this.audioIndex = 0;
                        }
                    }

                    else if (this.soundType == SoundType.ceramic && this.audioIndex > 3)
                    {
                        this.audioIndex = 2;
                    }
                    this.audioSourceForPlayer.clip = this.footstepAudioClips[this.audioIndex];
                    this.audioSourceForPlayer.Play();
                    this.audioIndex++;
                }
            }
            if (this.keyIsDown && !this.keyAlreadyDown)
            {
                this.keyAlreadyDown = true;
            }
            if (!this.keys[0] && !this.keys[1] && !this.keys[2] && !this.keys[3] && this.keyAlreadyDown)
            {
                this.audioSourceForPlayer.Stop();
                if (this.soundType == SoundType.wood)
                {
                    this.audioIndex = 0;
                }
                else if (this.soundType == SoundType.ceramic)
                {
                    this.audioIndex = 2;
                }
                this.audioSourceForPlayer.clip = this.footstepAudioClips[this.audioIndex];
                this.keyIsDown = false;
                this.keyAlreadyDown = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "audioTrigger")
        {
            EnvironmentCue(0, other);
        }

        if(other.gameObject.tag == "audioTriggerOneShot")
        {
            EnvironmentCue(0, other);
            Destroy(other.gameObject);
        }
    }

    public void EnvironmentCue(int environmentSoundIndex, Collider audioCollider)
    {
        AudioSource temporaryAudioSource;

        temporaryAudioSource = audioCollider.gameObject.GetComponent<AudioSource>();
        temporaryAudioSource.clip = environmentAudioClips[environmentSoundIndex];
        temporaryAudioSource.Play();
        Destroy(audioCollider.gameObject);
    }
}
