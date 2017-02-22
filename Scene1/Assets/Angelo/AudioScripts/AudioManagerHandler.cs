using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerHandler : MonoBehaviour
{
    public AudioManager audioManagerInstance;

    private void Awake()
    {
        audioManagerInstance = AudioManager.AudioManagerInstance;
    }

    public void FootStepImplementation()
    {
        audioManagerInstance.FootStepCues();
    }

    public void FootStepSet()
    {
        audioManagerInstance.SetCurrentFootSteps();
    }
}
