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
    
    //Enum to track the different states of the player for the scripted portion of the ganme
    public enum PlayerGameState
    {
        phase1,
        phase2,
        phase3,
        phase4
    }

    //Tracks the current player state
    public PlayerGameState currentPlayerState;

    //Enum to contain the different states of game with relation to pausing
    public enum PauseState
    {
        unpaused,
        paused
    }

    //Tracks if the game is paused or not
    public PauseState currentPauseState;

    //Bool to track if the player is alive
    public bool isPlayerAlive;

    //Canvas access
    public GameObject pauseMenuCanvas;

    //Function to set the isPlayerAlive bool to false
    public void PlayerHasDied()
    {
        isPlayerAlive = false;
    }

    //Set the current GameState of the player
    public void SetPlayerGameState(PlayerGameState desiredState)
    {
        this.currentPlayerState = desiredState;
    }

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
        //Freeze time completely
        Time.timeScale = 0;
        
        //Make the cursor visible to the player
        Cursor.visible = true;

        //Unlock the cursor but confine it to the space of the game window
        Cursor.lockState = CursorLockMode.Confined;

        //Set the pause menu canvas to active
        pauseMenuCanvas.SetActive(true);
    }

    //Private function to unpause the game
    private void UnPauseGame()
    {
        //Continue time
        Time.timeScale = 1;

        //Make the cursor invisible
        Cursor.visible = false;

        //Lock the cursor to the center of the game window
        Cursor.lockState = CursorLockMode.Locked;

        //Deactivate the pause menu canvas
        pauseMenuCanvas.SetActive(false);
    }
}
