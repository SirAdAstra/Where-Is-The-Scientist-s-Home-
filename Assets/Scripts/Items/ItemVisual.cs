using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemVisual : MonoBehaviour
{
    private ItemData visualisingItem;
    public ItemData Item
    {
        get{
            return visualisingItem;
        }
    }

    public void Init(ItemData item)
    {
        visualisingItem = item;
        if (GetComponent<SpriteRenderer>()){
            GetComponent<SpriteRenderer>().sprite = item.Icon;
        }
        if (GetComponent<Image>()){
            GetComponent<Image>().sprite = item.Icon;
        }
    }
}
