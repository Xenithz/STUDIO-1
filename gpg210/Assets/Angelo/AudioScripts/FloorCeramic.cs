using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCeramic : MonoBehaviour
{
    /*
        The FloorCeramic class will be attached to all ceramic floors in the scene. This will affect the monoauduio manager directly and change
        the current sound type.
    */

    public MonoAudioManager monoAudio;

    private void Awake()
    {
        monoAudio = GameObject.FindGameObjectWithTag("player").GetComponent<MonoAudioManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "foot")
        {
            monoAudio.soundType = MonoAudioManager.SoundType.ceramic;
        }
    }
}
