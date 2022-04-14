using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [HideInInspector] public Item curentItem;
    [SerializeField] float volumeInventory;
    [SerializeField] GameObject inventCell;
    [SerializeField] Image inventPanel;
    public struct ItemCell
    {
       public GameObject itemObject;
       public Item Item;
       public InventoryCell iCell; 
    }
    public static List<ItemCell> itemCells = new List<ItemCell>();
    private void Awake()
    {
        Control.pickItemEvent.AddListener(PickItem);
    }
    private void PickItem()
    {
        GeneralUi.characterPanel.SetActive(true);
        GameObject ItemInInventory = new GameObject();
        ItemInInventory.Equals(curentItem.gameObject);
        Player.IsItemPicked = false;
        GeneralUi.hint.SetActive(false);
        Destroy(curentItem.gameObject);
        if (itemCells.Count == 0)
        {
            CreateInventoryCell();
        }
        else
        {
            for (int i = 0; i < itemCells.Count; i++)
            {
                if (itemCells[i].Item.nameItem == curentItem.nameItem)
                {
                    itemCells[i].Item.count += curentItem.count;
                    itemCells[i].iCell.PickElementToCell(i);
                    GeneralUi.characterPanel.SetActive(false);
                    return;
                }
            }
            CreateInventoryCell();
        }
        GeneralUi.characterPanel.SetActive(false);
    }
    private void CreateInventoryCell()
    {
        GameObject newInventCell = GameObject.Instantiate(inventCell, inventPanel.transform);
        ItemCell iCell = new ItemCell();
        iCell.itemObject = curentItem.gameObject;
        iCell.Item = curentItem;
        iCell.iCell = newInventCell.GetComponent<InventoryCell>();
        itemCells.Add(iCell);
        iCell.iCell.PickElementToCell(itemCells.Count-1);
        
    }
}
