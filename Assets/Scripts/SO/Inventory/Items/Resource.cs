using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Object", menuName = "Inventory System/Items/Resource")]
public class Resource : InventoryItem
{
    public void Awake()
    {
        type = ItemType.Resource;
    }
}
