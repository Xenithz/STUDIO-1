using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUIText : MonoBehaviour
{
    public enum TextStates
    {
        candle,
        door,
        nothing,
    }

    public TextStates currentState;

    public Text text;

    private void Awake()
    {
        currentState = TextStates.nothing;
    }

    private void Update()
    {
        switch (currentState)
        {
            case TextStates.candle:
                text.text = "Press E to interact with the candle";
                break;
            case TextStates.door:
                text.text = "Press E to interact with the door";
                break;
            case TextStates.nothing:
                text.text = " ";
                break;

            default:
                text.text = " ";
                break;
        }
    }
}
