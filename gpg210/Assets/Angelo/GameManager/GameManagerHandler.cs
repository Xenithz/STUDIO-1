using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHandler : MonoBehaviour
{
    /*
         The game manager handler is the script that will be attached to a gameobject in the playarea
         and will allow access to the functions of the gamemanager
    */

    //Create object of the game manager class
    public GameManager gameManagerInstance;

    public List<GameObject> handlerEventList = new List<GameObject>();

    private void Awake()
    {
        //Set this object to the static instance of the game manager
        gameManagerInstance = GameManager.GameManagerInstance;
        
        //Unpause the game when starting the game
        gameManagerInstance.SetPauseState(GameManager.PauseState.unpaused);

        gameManagerInstance.eventList.AddRange(handlerEventList);
    }

    private void Update()
    {
        //Check every frame for pause
        gameManagerInstance.PauseCheck();

        if(gameManagerInstance.isPlayerAlive == true)
        {
            Debug.Log("FINISHED");
        }
    }
}
