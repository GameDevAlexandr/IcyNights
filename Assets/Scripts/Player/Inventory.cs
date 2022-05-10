using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] float MaxVolumeInventory;
    public static UnityEvent<bool, int> changeItemCountEvent = new UnityEvent<bool, int>();
    public static List<Item> itemsInInventory = new List<Item>();
    public static float filledVolume;
    private static bool noDestroy = false;

    private void Awake()
    {
        Control.pickItemEvent.AddListener(PickItem);
    }
    private static void PickItem(Item.Type type)
    {
        if (type == Item.Type.picup)
        {
            Item currentItem = new Item();
            currentItem = ContctEnvironment.item;
            for (int i = 0; i < itemsInInventory.Count; i++)
            {
                if (itemsInInventory[i].nameItem == currentItem.nameItem)
                {
                    AddItemToinvenotry(i, currentItem.count);
                    goto over;
                }
            }
            itemsInInventory.Add(currentItem);
            GeneralUi.PutItemToInventory(itemsInInventory.Count - 1);
            Player.CurrentParams.capacity += currentItem.weight * currentItem.count;
        over:
            ContctEnvironment.IsItemPicked = false;
            GeneralUi.hint.SetActive(false);
            if (!noDestroy)
            {
                Destroy(ContctEnvironment.item.gameObject);
            }
        }
        else
        {
            GeneralUi.ActivateInteractionPanel(true, ContctEnvironment.item);
        }
    }
    public static void AddItemToinvenotry(int inventoryIndex, int count)
    {
        bool isDestroed = false;
        Player.CurrentParams.capacity += itemsInInventory[inventoryIndex].weight * count;
        itemsInInventory[inventoryIndex].count += count;
        if (itemsInInventory[inventoryIndex].count == 0)
        {
            itemsInInventory.RemoveAt(inventoryIndex);
            isDestroed = true;
        }
        changeItemCountEvent.Invoke(isDestroed,inventoryIndex);
    }
    public static void AddItemAfterCraft(Item item)
    {
        for( int i = 0; i < itemsInInventory.Count; i++)
        {
            if (item.nameItem == itemsInInventory[i].nameItem)
            {
                AddItemToinvenotry(i, 1);
                break;
            }
            else if(i== itemsInInventory.Count - 1)
            {
                itemsInInventory.Add(item);
                GeneralUi.PutItemToInventory(itemsInInventory.Count - 1);
                Player.CurrentParams.capacity += item.weight;
            }
        }
    }
}
