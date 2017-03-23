using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneEvent : MonoBehaviour
{
    GameManagerHandler gameManager;
    bool doOnce;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerHandler>();
        doOnce = false;
    }

    public void EventTrigger()
    {
        if (doOnce == false)
        {
            gameManager.gameManagerInstance.SetGateState(GameManager.ActionGate.shouldDo);
            doOnce = true;
        }
    }
}
