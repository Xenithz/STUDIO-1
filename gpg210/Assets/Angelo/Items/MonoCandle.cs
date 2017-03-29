using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCandle : MonoItem
{
    /* 
        MonoCandle class will be used to contain the behavior for the candles in the game
    */

    //The states that the candle can be in
    public enum State
    {
        on,
        off
    }

    //Track the current state of the candle
    public State currentState;

    //Reference for the fire particle system
    private ParticleSystem fire;
    
    //Reference for the emission of the fire particle system
    private ParticleSystem.EmissionModule emissionModuleForFire;

    private GameManagerHandler gameManager;

    public AudioClip candleClip;

    public AudioSource candleAudioSource;

    public bool test;

    public GameObject testObj;

    public override void CurrentBehavior()
    {
        if(gameManager.gameManagerInstance.currentGameState == GameManager.GameState.phase6)
        {
            //Check if it's on
            if (currentState == State.on)
            {
                //Turn it off
                currentState = State.off;
                candleAudioSource.clip = candleClip;
                candleAudioSource.Play();
                gameManager.gameManagerInstance.candleCount++;
            }
        }

        if(test == true)
        {
            if (currentState == State.on)
            {
                Debug.Log("working1");
                currentState = State.off;
                candleAudioSource.clip = candleClip;
                candleAudioSource.Play();
                testObj.GetComponent<CandleTest>().candleCount++;
            }
        }
    }

    private void Awake()
    {
        //Set it on
        currentState = State.on;
        
        //Set the fire variable
        fire = gameObject.GetComponentInChildren<ParticleSystem>();

        //Set the emission module
        emissionModuleForFire = fire.emission;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerHandler>();
}

    private void Update()
    {
        //Check if it's current state is on
        if(currentState == State.on)
        {
            //Set the emission enabled bool to true
            emissionModuleForFire.enabled = true;
        }

        if(currentState == State.off)
        {
            emissionModuleForFire.enabled = false;
        }
    }
}
