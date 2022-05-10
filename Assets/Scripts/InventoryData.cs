using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    public static Item[] woodArea = new Item[20];
    public static Item[] otherArea = new Item[40];
    public static Item quick;
    public static Item weapon;
    public static Item lights;
    public static Item bed;
    public static Item everyDay;

    public static bool addItem(Item item)
    {
        bool freeValue = false;
        switch (item.placeType)
        {
            case Item.PlaceType.woodArea:
                for (int i = 0; i < woodArea.Length; i++)
                {
                    if (woodArea[i].count <= 0)
                    {
                        woodArea[i] = item;
                        return true;
                    }

                }
                break;
        }
        return freeValue;
    }
}
