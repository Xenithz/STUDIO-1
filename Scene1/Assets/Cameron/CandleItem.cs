using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleItem : Item
{
    public CandleItem(Transform thisItem) : base(thisItem)
    {

    }

    private enum State
    {
        on,
        off
    }

    private State currentState;

    public override void ItemBehavior(Transform thisItem)
    {
        Debug.Log("working candle");
        Transform TargetFlame = thisItem.FindChild("Flame");

        if (currentState == State.on)
        {
            TargetFlame.gameObject.SetActive(true);

        }
        else if (currentState == State.off)
        {
            TargetFlame.gameObject.SetActive(false);
        }
    }

    public override void ItemStateSetOnAwake()
    {
        currentState = State.off;
    }
}
