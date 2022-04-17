using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] float MaxVolumeInventory;
    public static List<Item> itemsInInventory = new List<Item>();
    public static float filledVolume;
    private void Awake()
    {
  
    }
    public static void PickItem()
    {
        Item currentItem = new Item();
        currentItem.nameItem = ContctEnvironment.Item.nameItem;
        currentItem.count = ContctEnvironment.Item.count;
        currentItem.icon = ContctEnvironment.Item.icon;
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            if(itemsInInventory[i].nameItem == currentItem.nameItem)
            {
                itemsInInventory[i].count += currentItem.count;
                GeneralUi.PutItemToInventory(i);
                goto over;
            }
        }
        itemsInInventory.Add(currentItem);
        GeneralUi.PutItemToInventory(itemsInInventory.Count - 1);

    over:
        ContctEnvironment.IsItemPicked = false;
        GeneralUi.hint.SetActive(false);
        Destroy(ContctEnvironment.Item.gameObject);    
    }
}
