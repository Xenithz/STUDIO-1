using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerHandler : MonoBehaviour
{
    /*
         The audio manager handler will be attached to a gameobject and will be responsible for the usage of
         all audio manager functions
    */

    //Create object of audio manager class
    public AudioManager audioManagerInstance;

    private void Awake()
    {
        //Set the object to the instance inside the audiomanager to gain access to the functions
        audioManagerInstance = AudioManager.AudioManagerInstance;
    }
}
