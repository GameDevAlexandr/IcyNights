using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntiractionPanel : MonoBehaviour
{
    public Image content;
    public RawImage icon;
    public Text health;
    public Image[] upgadeItemIcons;
    [HideInInspector] public List<InventoryCell> inventoryCells = new List<InventoryCell>();
    public bool ChecContent(Item item)
    {
        bool isCheced = false;
        if (inventoryCells.Count > 0)
        {
            for (int n = 0; n < inventoryCells.Count; n++)
            {
                if(inventoryCells[n].item.nameItem == item.nameItem)
                {
                    isCheced = true;
                    break;
                }
            }
        }
        return isCheced;
    }
    private void OnEnable()
    {
        if (inventoryCells.Count != 0)
        {
            int n = 0;
            while (n < inventoryCells.Count)
            {
                if (!inventoryCells[n])
                {
                    inventoryCells.RemoveAt(n);
                }
                else
                {
                    n++;
                }
            }
        }
    }
}
