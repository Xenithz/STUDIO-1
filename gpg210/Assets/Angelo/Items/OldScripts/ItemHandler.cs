using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public enum ItemType
    {
        door,
        candle
    }

    public ItemType desiredItemType;

    public Item thisItem;

    private void Awake()
    {
        switch (desiredItemType)
        {
            case ItemType.door:
                thisItem = new DoorItem(transform);
                thisItem.ItemStateSetOnAwake();

                break;
            case ItemType.candle:
                thisItem = new CandleItem(transform);
                thisItem.ItemStateSetOnAwake();
                break;
        }
    }

    public void ItemBehavior()
    {
        thisItem.ItemBehavior(transform);
    }

}
