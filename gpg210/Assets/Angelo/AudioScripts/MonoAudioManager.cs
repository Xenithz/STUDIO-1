using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoAudioManager : MonoBehaviour
{
    public AudioClip[] footstepAudioClips;

    private AudioSource audio;

    private bool keyIsDown;

    private bool keyAlreadyDown;

    private bool[] keys;

    private int audioIndex;

    private enum SoundType
    {
        gravel,
        metal
    }

    private SoundType soundType;

    private void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();

        this.soundType = SoundType.gravel;
        if (this.soundType == SoundType.gravel)
        {
            this.audioIndex = 0;
        }
        else if (this.soundType == SoundType.metal)
        {
            this.audioIndex = 4;
        }
        this.audio = base.GetComponent<AudioSource>();
        this.audio.clip = this.footstepAudioClips[this.audioIndex];
    }

    private void Update()
    {
        this.keys = new bool[]
            {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.D)
            };
        if (this.keys[0] || this.keys[1] || this.keys[2] || this.keys[3])
        {
            this.keyIsDown = true;
            if (!this.audio.isPlaying)
            {
                if (this.soundType == SoundType.gravel)
                {
                    if (this.audioIndex > 3)
                    {
                        this.audioIndex = 0;
                    }
                }
                else if (this.soundType == SoundType.metal && this.audioIndex > 7)
                {
                    this.audioIndex = 4;
                }
                this.audio.clip = this.footstepAudioClips[this.audioIndex];
                this.audio.Play();
                this.audioIndex++;
            }
        }
        if (this.keyIsDown && !this.keyAlreadyDown)
        {
            this.keyAlreadyDown = true;
        }
        if (!this.keys[0] && !this.keys[1] && !this.keys[2] && !this.keys[3] && this.keyAlreadyDown)
        {
            this.audio.Stop();
            if (this.soundType == SoundType.gravel)
            {
                this.audioIndex = 0;
            }
            else if (this.soundType == SoundType.metal)
            {
                this.audioIndex = 4;
            }
            this.audio.clip = this.footstepAudioClips[this.audioIndex];
            this.keyIsDown = false;
            this.keyAlreadyDown = false;
        }
    }
}
