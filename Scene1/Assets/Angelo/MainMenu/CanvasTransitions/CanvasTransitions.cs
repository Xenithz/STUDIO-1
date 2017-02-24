using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTransitions : MonoBehaviour
{
    /*
        The canvas transitions class will handle the enabling and disabling of canvases to simulate transitions between different menus     
    */

    //Enum to provide different states that a canvas can be in
    public enum CanvasStates
    {
        enabled,
        disabled
    }

    //Function that will enable or disable a canvas depending on the desired state
    public void HandleCanvas(GameObject canvasToEnable, CanvasStates desiredCanvasState)
    {
        //Check if the desired state is disabled
        if(desiredCanvasState == CanvasStates.disabled)
        {
            //Disable the canvas
            canvasToEnable.SetActive(false);
        }

        //Check if the desired state is enabled
        else if(desiredCanvasState == CanvasStates.enabled)
        {
            //Enable the canvas
            canvasToEnable.SetActive(true);
        }
    }
}
