using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    /*
        The game manager will hold the variables important to the FLOW of the game and will control various aspects such as pausing
        in the game space, and ending the play session by setting the player to dead
    */

    //Private constructor as the GameManager will implement the singleton design pattern
    private GameManager()
    {

    }

    //Private static object of the GameManager class (Only one instance can exist)
    private static GameManager gameManagerInstance;

    //Property to access the instance
    public static GameManager GameManagerInstance
    {
        get
        {
            //If the instance is empty
            if (gameManagerInstance == null)
            {
                //Initialize it as a new GameManager
                gameManagerInstance = new GameManager();
            }

            return gameManagerInstance;
        }
    }

    public List<GameObject> FirstEvents = new List<GameObject>();

    //Enum to contain the different states of game with relation to pausing
    public enum PauseState
    {
        unpaused,
        paused
    }

    public enum GameState
    {
        phase1,
        phase2,
        phase3,
    }

    public enum ActionGate
    {
        shouldDo,
        shouldNotDo
    }

    public ActionGate currentActionGate;

    public GameState currentGameState;

    //Tracks if the game is paused or not
    public PauseState currentPauseState;

    //Bool to track if the player is alive
    public bool isPlayerAlive;

    //Canvas access
    public GameObject pauseMenuCanvas;

    //Function to set the isPlayerAlive bool to false
    public void PlayerHasDied()
    {
        isPlayerAlive = true;
    }

    public void SetGameState(GameState desiredGameState)
    {
        this.currentGameState = desiredGameState;
    }

    #region ScriptedEvents Functions

    public void SetGateState(ActionGate desiredGate)
    {
        this.currentActionGate = desiredGate;
    }

    public void FirstEvent()
    {
        Debug.Log("this works");
        SetGateState(ActionGate.shouldNotDo);
        SetGameState(GameState.phase2);
    }

    public void SecondEvent()
    {
        Debug.Log("I moved");
        SetGateState(ActionGate.shouldNotDo);
        SetGameState(GameState.phase3);
    }

    #endregion

    #region Pause Functions
    //Set the pause state
    public void SetPauseState(PauseState desiredPauseState)
    {
        this.currentPauseState = desiredPauseState;
    }

    //Function to handle pause without revealing the code specifics
    public void PauseCheck()
    {
        //Check if escape key is pressed and pause state is unpaused
        if(Input.GetKeyDown(KeyCode.Escape) && currentPauseState == PauseState.unpaused)
        {
            //Pause the game
            PauseGame();
        }

        //Check if the escape key is pressed and the pause state is paused
        else if(Input.GetKeyDown(KeyCode.Escape) && currentPauseState == PauseState.paused)
        {
            //Unpause the game
            UnPauseGame();
        }
    }

    //Private function to pause the game
    private void PauseGame()
    {
        currentPauseState = PauseState.paused;

        //Freeze time completely
        Time.timeScale = 0;
        
        //Make the cursor visible to the player
        Cursor.visible = true;

        //Unlock the cursor but confine it to the space of the game window
        Cursor.lockState = CursorLockMode.Confined;

        //Set the pause menu canvas to active
        //pauseMenuCanvas.SetActive(true);
    }

    //Private function to unpause the game
    private void UnPauseGame()
    {
        currentPauseState = PauseState.unpaused;

        Debug.Log("GO");
        //Continue time
        Time.timeScale = 1;

        //Make the cursor invisible
        Cursor.visible = false;

        //Lock the cursor to the center of the game window
        Cursor.lockState = CursorLockMode.Locked;

        //Deactivate the pause menu canvas
        //pauseMenuCanvas.SetActive(false);
    }
    #endregion
}
