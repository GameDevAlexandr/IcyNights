using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemAmount
{
    public InventoryItem item;
    [Range(1, 999)] public int amount;
}

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Inventory System/Crafting/Recipe")]
public class CraftRecipe : ScriptableObject
{
    public List<ItemAmount> CraftMaterials;
    public List<ItemAmount> CraftResults;

    [HideInInspector] public bool canBeCrafted;

    public bool CanCraft(InventoryObject inventory)
    {
        foreach(ItemAmount itemAmount in CraftMaterials)
        {
            if (inventory.FindItemOnInventory(itemAmount.item.data).amount < itemAmount.amount)
            {
                canBeCrafted = false;
                return false;
            }
        }
        canBeCrafted = true;
        return true;
    }

    public void Craft(InventoryObject inventory)
    {

        Debug.Log(CanCraft(inventory));
        if (CanCraft(inventory))
        {
            Debug.Log("a");

            /*foreach (ItemAmount itemAmount in CraftMaterials)
            {
                InventorySlot invSlot = inventory.FindItemOnInventory(itemAmount.item.data);

                if (invSlot.amount > 1)
                {
                    for (int i = 0; i < itemAmount.amount; i++)
                    {
                        invSlot.amount--;
                    }
                }
                else
                    invSlot.RemoveItem();
            }*/

            for (int i = CraftMaterials.Count - 1; i >= 0; i--)
            {
                var itemAmount = CraftMaterials[i];
                InventorySlot invSlot = inventory.FindItemOnInventory(itemAmount.item.data);
                Debug.Log(itemAmount + " / " + invSlot);


                if (invSlot.amount > 1)
                {
                    invSlot.amount -= itemAmount.amount;
                }
                else
                {
                    invSlot.RemoveItem();
                    inventory.RemoveWeight(invSlot.weightPerOne * invSlot.amount);
                }
                
            }

            foreach (ItemAmount itemAmount in CraftResults)
            {
                inventory.AddItem(itemAmount.item.data, itemAmount.amount);
                Debug.Log("1");
            }
        }
    }
}
