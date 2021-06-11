using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Контроллер предмета вне инвентаря
public class WorldItem : MonoBehaviour {

    //ScriptableObject предмета
    private ItemData worldItem
    {
        get
        {
            return GetComponentInChildren<ItemVisual>().Item;
        }
    }

    //Инициализация ScriptableObject-ом
	public void Init(ItemData item)
    {
        GetComponentInChildren<ItemVisual>().Init(item);
    }
}
