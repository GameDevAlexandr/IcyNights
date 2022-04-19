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
        currentItem = ContctEnvironment.item;
        //currentItem.nameItem = ContctEnvironment.item.nameItem;
        //currentItem.count = ContctEnvironment.item.count;
        //currentItem.icon = ContctEnvironment.item.icon;
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
        Player.CurrentParams.capacity += ContctEnvironment.item.weight;
        Destroy(ContctEnvironment.item.gameObject);    
    }
}
