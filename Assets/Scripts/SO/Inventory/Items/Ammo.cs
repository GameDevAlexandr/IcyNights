using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Inventory System/Items/Ammo")]
public class Ammo : InventoryItem
{
    public void Awake()
    {
        type = ItemType.Weapon;
    }
}
