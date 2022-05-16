using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]

public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public InventoryItem[] itemObjects;
    public Dictionary<int, InventoryItem> GetItem = new Dictionary<int, InventoryItem>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < itemObjects.Length; i++)
        {
            itemObjects[i].data.id = i;
            GetItem.Add(i, itemObjects[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, InventoryItem>();
    }
}
