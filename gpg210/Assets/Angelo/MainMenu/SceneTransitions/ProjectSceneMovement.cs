using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectSceneMovement : MonoBehaviour
{
    /*
         The project scene movement class will be used for transitions between scenes, quitting the application, and implementing 
         a loading screen
    */

    //Array to hold all the buttons in the main menu
    public GameObject[] buttons;

    //GameObject which is the loading screen
    public GameObject loadingScreen;

    //Function that will load game menu and show a loading screen
    public void LoadGameSceneFromMenu()
    {
        //Iterate through the buttons
        for(int i = 0; i < buttons.Length; i++)
        {
            //Set button at index i to false
            buttons[i].SetActive(false);
        }

        //Set the loading screen to active
        loadingScreen.SetActive(true);

        //Load the game scene
        LoadScene(2);
    }

    //Function that will handle all regular scene loading
    public void LoadScene(int sceneIndex)
    {
        //Load a scene at specified scene index
        SceneManager.LoadScene(sceneIndex);
    }

    //Function that will handle quitting unity application
    public void QuitUnityApplication()
    {
        //Quit application
        Application.Quit();
    }
}
