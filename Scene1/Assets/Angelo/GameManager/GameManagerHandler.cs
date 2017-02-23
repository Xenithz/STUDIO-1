using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHandler : MonoBehaviour
{
    public GameManager gameManagerInstance;

    private void Awake()
    {
        gameManagerInstance = GameManager.GameManagerInstance;
        gameManagerInstance.SetPauseState(GameManager.PauseState.unpaused);
    }

    private void Update()
    {
        gameManagerInstance.PauseCheck();
    }
}
