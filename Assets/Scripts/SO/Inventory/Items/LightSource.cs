using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Light Source", menuName = "Inventory System/Items/LightSource")]
public class LightSource : InventoryItem
{
    public void Awake()
    {
        type = ItemType.LightSource;
    }
}
