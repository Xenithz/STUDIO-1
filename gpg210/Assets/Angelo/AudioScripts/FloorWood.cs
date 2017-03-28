using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorWood : MonoBehaviour
{
    public MonoAudioManager monoAudio;

    private void Awake()
    {
        monoAudio = GameObject.FindGameObjectWithTag("player").GetComponent<MonoAudioManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "foot")
        {
            monoAudio.soundType = MonoAudioManager.SoundType.wood;
        }
    }
}
