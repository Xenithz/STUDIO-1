using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCandleEvent : MonoItem
{
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

    public bool doOnce;

    private void Awake()
    {
        //Set it on
        currentState = State.on;

        //Set the fire variable
        fire = gameObject.GetComponentInChildren<ParticleSystem>();

        //Set the emission module
        emissionModuleForFire = fire.emission;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerHandler>();
        doOnce = false;
    }

    public override void CurrentBehavior()
    {
        if (doOnce == false)
        {
            currentState = State.off;
            candleAudioSource.clip = candleClip;
            candleAudioSource.Play();
            gameManager.gameManagerInstance.SetGateState(GameManager.ActionGate.shouldDo);
            doOnce = true;
        }
    }


    private void Update()
    {
        //Check if it's current state is on
        if (currentState == State.on)
        {
            //Set the emission enabled bool to true
            emissionModuleForFire.enabled = true;
        }

        if (currentState == State.off)
        {
            emissionModuleForFire.enabled = false;
        }
    }
}
