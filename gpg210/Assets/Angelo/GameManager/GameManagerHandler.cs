using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public List<GameObject> FinalCandles = new List<GameObject>();
    public List<Light> Lights = new List<Light>();
    public List<Light> KitchenLights = new List<Light>();

    public GameObject ai;
    public GameObject frontDoor;
    public Image black;
    public float fadeTime;
    public int gameOverSceneIndex;
    public Text objectivesUI;


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
        gameManagerInstance.FinalCandles.AddRange(FinalCandles);
        gameManagerInstance.Lights.AddRange(Lights);
        gameManagerInstance.KitchenLights.AddRange(KitchenLights);

        gameManagerInstance.AI = ai;
        gameManagerInstance.frontDoor = frontDoor;
        gameManagerInstance.black = black;
        gameManagerInstance.fadeTime = fadeTime;
        gameManagerInstance.black.GetComponent<CanvasRenderer>().SetAlpha(0f);
        gameManagerInstance.objectivesUI = objectivesUI;
    }

    private void Update()
    {
        //Debug.Log(gameManagerInstance.currentActionGate);\
        Debug.Log(gameManagerInstance.currentGameState);
        //Check every frame for pause
        gameManagerInstance.PauseCheck();

       if(gameManagerInstance.currentPauseState != GameManager.PauseState.paused)
        {
            if (gameManagerInstance.isPlayerAlive == true)
            {
                gameManagerInstance.CameronFade();

                if(gameManagerInstance.black.canvasRenderer.GetAlpha() == 1f)
                {
                    SceneManager.LoadScene(gameOverSceneIndex);
                }
            }

            if (gameManagerInstance.currentGameState == GameManager.GameState.phase1 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
            {
                gameManagerInstance.FirstEvent();
            }

            else if (gameManagerInstance.currentGameState == GameManager.GameState.phase2 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
            {
                gameManagerInstance.SecondEvent();
            }

            else if (gameManagerInstance.currentGameState == GameManager.GameState.phase3 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
            {
                gameManagerInstance.ThirdEvent();
                StartCoroutine(gameManagerInstance.KitchenFlash());
                StartCoroutine(gameManagerInstance.WritingOnWall());
            }

            else if (gameManagerInstance.currentGameState == GameManager.GameState.phase4 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
            {
                gameManagerInstance.FourthEvent();
                StopAllCoroutines();
            }

            else if (gameManagerInstance.currentGameState == GameManager.GameState.phase5 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
            {
                gameManagerInstance.FifthEvent();
            }

            else if (gameManagerInstance.currentGameState == GameManager.GameState.phase6 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
            {
                gameManagerInstance.SixthEvent();
            }

            else if (gameManagerInstance.currentGameState == GameManager.GameState.phase7 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
            {
                gameManagerInstance.SeventhEvent();
            }

            else if (gameManagerInstance.currentGameState == GameManager.GameState.phase8 && gameManagerInstance.currentActionGate == GameManager.ActionGate.shouldDo)
            {
                gameManagerInstance.EightEvent();
            }
        }
    }
}
