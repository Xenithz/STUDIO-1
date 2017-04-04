using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUIText : MonoBehaviour
{
    public enum States
    {
        candle,
        door,
        nothing,
    }

    public States currentState;

    public Text text;

    private void Update()
    {
        switch (currentState)
        {
            case States.candle:
                text.text = "Press E to interact with the candle";
                break;
            case States.door:
                text.text = "Press E to interact with the door";
                break;
            case States.nothing:
                text.text = "";
                break;
            default:
                break;
        }
    }
}
