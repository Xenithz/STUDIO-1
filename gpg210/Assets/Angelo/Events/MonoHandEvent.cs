using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoHandEvent : MonoItem
{
    GameManagerHandler gameManager;
    public bool doOnce;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerHandler>();
        doOnce = false;
    }

    public override void CurrentBehavior()
    {
       if(doOnce == false)
        {
            gameManager.gameManagerInstance.SetGateState(GameManager.ActionGate.shouldDo);
            doOnce = true;
        }
    }
}
