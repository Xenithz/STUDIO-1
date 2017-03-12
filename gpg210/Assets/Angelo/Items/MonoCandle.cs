using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCandle : MonoItem
{
    /* 
        MonoCandle class will be used to contain the behavior for the candles in the game
    */

    //The states that the candle can be in
    private enum State
    {
        on,
        off
    }

    //Track the current state of the candle
    private State currentState;

    //Reference for the fire particle system
    private ParticleSystem fire;
    
    //Reference for the emission of the fire particle system
    private ParticleSystem.EmissionModule emissionModuleForFire;

    public override void CurrentBehavior()
    {
        //Check if it's on
        if(currentState == State.on)
        {
            //Turn it off
            currentState = State.off;
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
}

    private void Update()
    {
        //Check if it's current state is on
        if(currentState == State.on)
        {
            //Set the emission enabled bool to true
            emissionModuleForFire.enabled = true;
        }

        //Check if it's current state is off
        else if(currentState == State.off)
        {
            //Set thye emission enabled bool to false
            emissionModuleForFire.enabled = false;
        }
    }
}
