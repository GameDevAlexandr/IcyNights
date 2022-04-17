using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    private ItemsData itemsData ;
    private void Awake()
    {
        itemsData = GameData.DataSingleton.ItemsData;
    }
    public bool isCrafting(Item item)
    {
        bool CraftComplite = true;
        int indexCraft = 0;
        for (int i = 0; i < itemsData.crafts.Length; i++)
        {
            if(itemsData.crafts[i].result.nameItem == item.nameItem)
            {
                indexCraft = i;
                break;
            }
        }
        if (CraftComplite) 
        {
            for (int i = 0; i < itemsData.crafts[indexCraft].itemComponents.Length; i++)
            {
                ItemsData.Craft.ItemComponent itemComponent = itemsData.crafts[indexCraft].itemComponents[i];
                for (int j = 0; j < Inventory.itemsInInventory.Count; j++)
                {
                    Item checkItem = Inventory.itemsInInventory[j];
                    if (checkItem.nameItem ==itemComponent.itemComponent.nameItem && checkItem.count >= itemComponent.count)
                    {
                        CraftComplite = true;
                        break;
                    }
                    CraftComplite = false;
                }
            }
        }
        return CraftComplite;
    }
    public void StartCraft()
    {

    }
}
