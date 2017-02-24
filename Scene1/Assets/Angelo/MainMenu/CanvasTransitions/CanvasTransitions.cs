using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTransitions : MonoBehaviour
{
    public enum CanvasStates
    {
        enabled,
        disabled
    }

    public void HandleCanvas(GameObject canvasToEnable, CanvasStates desiredCanvasState)
    {
        if(desiredCanvasState == CanvasStates.disabled)
        {
            canvasToEnable.SetActive(false);
        }
        else if(desiredCanvasState == CanvasStates.enabled)
        {
            canvasToEnable.SetActive(true);
        }
    }
}
