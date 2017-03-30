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

    public List<GameObject> Event1 = new List<GameObject>();
    public List<GameObject> Event2 = new List<GameObject>();
    public List<GameObject> Event3 = new List<GameObject>();
    public List<GameObject> Event4 = new List<GameObject>();
    public List<GameObject> Event5 = new List<GameObject>();
    public List<GameObject> Writing = new List<GameObject>();

    public GameObject ai;
    public GameObject frontDoor;

    private void Awake()
    {
        //Set this object to the static instance of the game manager
        gameManagerInstance = GameManager.GameManagerInstance;
        
        //Unpause the game when starting the game
        gameManagerInstance.SetPauseState(GameManager.PauseState.unpaused);

        gameManagerInstance.SetGameState(GameManager.GameState.phase1);

        gameManagerInstance.SetGateState(GameManager.ActionGate.shouldDo);

        gameManagerInstance.FirstEvents.AddRange(Event1);
        gameManagerInstance.SecondEvents.AddRange(Event2);
        gameManagerInstance.ThirdEvents.AddRange(Event3);
        gameManagerInstance.FourthEvents.AddRange(Event4);
        gameManagerInstance.FifthEvents.AddRange(Event5);
        gameManagerInstance.Writing.AddRange(Writing);

        gameManagerInstance.AI = ai;
        gameManagerInstance.frontDoor = frontDoor;
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

        else if(gameManagerInstance.currentGameState == GameManager.GameState.phase2 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
        {
            gameManagerInstance.SecondEvent();
        }

        else if(gameManagerInstance.currentGameState == GameManager.GameState.phase3 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
        {
            gameManagerInstance.ThirdEvent();
        }

        else if(gameManagerInstance.currentGameState == GameManager.GameState.phase4 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
        {
            gameManagerInstance.FourthEvent();
        }

        else if(gameManagerInstance.currentGameState == GameManager.GameState.phase5 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
        {
            gameManagerInstance.FifthEvent();
        }

        else if(gameManagerInstance.currentGameState == GameManager.GameState.phase6 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
        {
            gameManagerInstance.SixthEvent();
        }

        else if(gameManagerInstance.currentGameState == GameManager.GameState.phase7 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
        {
            gameManagerInstance.SeventhEvent();
        }
    }
}
