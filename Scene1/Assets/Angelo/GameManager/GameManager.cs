using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    private GameManager()
    {

    }

    private static GameManager gameManagerInstance;

    public static GameManager GameManagerInstance
    {
        get
        {
            if (gameManagerInstance == null)
            {
                gameManagerInstance = new GameManager();
            }

            return gameManagerInstance;
        }
    }
    
    public enum PlayerGameState
    {
        phase1,
        phase2,
        phase3,
        phase4
    }

    public PlayerGameState currentPlayerState;

    public enum PauseState
    {
        unpaused,
        paused
    }

    public PauseState currentPauseState;

    public bool isPlayerAlive;

    public GameObject pauseMenuCanvas;

    public void PlayerHasDied()
    {
        isPlayerAlive = false;
    }

    public void SetPlayerGameState(PlayerGameState desiredState)
    {
        this.currentPlayerState = desiredState;
    }

    public void SetPauseState(PauseState desiredPauseState)
    {
        this.currentPauseState = desiredPauseState;
    }

    public void PauseCheck()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && currentPauseState == PauseState.unpaused)
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && currentPauseState == PauseState.paused)
        {
            UnPauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenuCanvas.SetActive(true);
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuCanvas.SetActive(false);
    }
}
