using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Usable,             //Расходники
    Resource,           //Материалы
    Equip,              //Одежда
    Weapon,             //Оружие
    Ammo,               //Патроны
    Tool,               //Инструменты
    LightSource         //Освещение

}

public abstract class InventoryItem : ScriptableObject
{
    public ItemFix data = new ItemFix();
    [SerializeField] public Sprite visualComponent;
    [SerializeField] public ItemType type;
    [TextArea(0, 20)] [SerializeField] public string description;

    public float weightPerOne;
    public bool canStack;
    public int maxStackAmount;
}

[System.Serializable]
public class ItemFix
{
    public string name;
    public int id = -1;
    public bool canStack;
    public int maxStackAmount;
    public float weightPerOne;

    public ItemFix()
    {
        name = "";
        id = -1;
    }

    public ItemFix(InventoryItem item)
    {
        name = item.name;
        id = item.data.id;
        canStack = item.canStack;
        maxStackAmount = item.maxStackAmount;
        weightPerOne = item.weightPerOne;
    }
}