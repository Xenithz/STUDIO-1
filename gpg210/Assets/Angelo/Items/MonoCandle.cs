using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCandle : MonoItem
{

    private enum State
    {
        on,
        off
    }

    private State currentState;

    private ParticleSystem fire;
    private ParticleSystem.EmissionModule emissionModuleForFire;

    public override void CurrentBehavior()
    {
        if(currentState == State.on)
        {
            currentState = State.off;
        }
    }

    private void Awake()
    {
        currentState = State.on;
        fire = gameObject.GetComponentInChildren<ParticleSystem>();
        emissionModuleForFire = fire.emission;
}

    private void Update()
    {
        if(currentState == State.on)
        {
            emissionModuleForFire.enabled = true;
            Debug.Log("is working");
        }
        else if(currentState == State.off)
        {
            emissionModuleForFire.enabled = false;
        }
    }
}
