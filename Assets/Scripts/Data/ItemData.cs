using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Data/Item", order = 51)]
public class ItemData : ScriptableObject
{
    public Sprite Icon;     //Спрайт
    public string Title;    //Название
    public int ID;

    [Multiline]
    public string Description;      //Описание
    public string Commentary;       //Комментарий ГГ
    public bool Decoustructable;    //Возможность разбора
}
