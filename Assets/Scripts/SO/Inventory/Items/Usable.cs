using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Usable Object", menuName = "Inventory System/Items/Usable")]

public class Usable : InventoryItem
{
    [Header("Восстанавливать кол-во:")]
    public int restoreFood;
    public int restoreWater;
    public int restoreHealth;
    public int restoreTemperature;
    public int restoreStamina;
    public int restoreSleep;

    public void Awake()
    {
        type = ItemType.Usable;
    }
}
