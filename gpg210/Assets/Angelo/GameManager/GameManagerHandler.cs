using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerHandler : MonoBehaviour
{
    /*
         The game manager handler is the script that will be attached to a gameobject in the playarea
         and will allow access to the functions of the gamemanager
    */

    //Create object of the game manager class
    public GameManager gameManagerInstance;

    private void Awake()
    {
        //Set this object to the static instance of the game manager
        gameManagerInstance = GameManager.GameManagerInstance;
        
        //Unpause the game when starting the game
        gameManagerInstance.SetPauseState(GameManager.PauseState.unpaused);

        gameManagerInstance.SetGameState(GameManager.GameState.phase1);

        gameManagerInstance.SetGateState(GameManager.ActionGate.shouldNotDo);
    }

    private void Update()
    {
        //Check every frame for pause
        gameManagerInstance.PauseCheck();

        if(gameManagerInstance.isPlayerAlive == true)
        {
            Debug.Log("FINISHED");
        }

        if(gameManagerInstance.currentGameState == GameManager.GameState.phase1 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
        {
            gameManagerInstance.FirstEvent();
        }
    }
}
