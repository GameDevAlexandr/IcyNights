using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{
    [HideInInspector]public Item craftedItem;
    public Transform content;
    public Image[] needItemIcon;
    public Text conditionText;
    public Button craftButton;
    private Text[] needItemsCount;
    private ItemsData itemsData;
    private bool craftIsLoaded;
    private List<Need> needs = new List<Need>();
    public struct Need 
    {
        public int invenoryIndex;
        public int count;
    }

    private void Awake()
    {
        needItemsCount = new Text[needItemIcon.Length];
    }
    public bool isCrafting(Item item)
    {
        needs.Clear();
        bool CraftComplite = true;
        int indexCraft = 0;
        for (int i = 0; i < itemsData.crafts.Length; i++)
        {
            if (itemsData.crafts[i].result.nameItem == item.nameItem)
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
                needItemIcon[i].sprite = itemComponent.itemComponent.icon;
                needItemsCount[i].text = itemComponent.count.ToString();
                
                if (Inventory.itemsInInventory.Count > 0)
                {
                    for (int j = 0; j < Inventory.itemsInInventory.Count; j++)
                    {
                        Item checkItem = Inventory.itemsInInventory[j];
                        if (checkItem.nameItem == itemComponent.itemComponent.nameItem)
                        {
                            Need need = new Need();
                            need.count = itemComponent.count;
                            need.invenoryIndex = j;
                            needs.Add(need);
                            Debug.Log("itemComponent.count");
                            if (checkItem.count >= itemComponent.count)
                            {
                                CraftComplite = true;
                                break;
                            }
                        }
                        CraftComplite = false;
                    }
                }
                else
                {
                    CraftComplite = false;
                }
            }
        }
        Debug.Log(CraftComplite);
        return CraftComplite;
    }
    public void SetCraftedMaterial()
    {
        if (isCrafting(craftedItem))
        {
            craftButton.interactable = true;
        }
        else
        {
            craftButton.interactable = false;
        }
    }
    private void OnEnable()
    {
        itemsData = GameData.DataSingleton.ItemsData;
        for (int i = 0; i < needItemIcon.Length; i++)
        {
            needItemsCount[i] = needItemIcon[i].GetComponentInChildren<Text>();
        }
        if (!craftIsLoaded)
        {
            for (int i = 0; i < itemsData.crafts.Length; i++)
            {
                GameObject newObject = Instantiate(GeneralUi.itemCell, content);
                InventoryCell cell = newObject.GetComponent<InventoryCell>();
                cell.buttonType = InventoryCell.ButtonType.craft;
                cell.item = itemsData.crafts[i].result;
                cell.PutElementToCell();
            }
            craftIsLoaded = true;
        }
    }
    public void StartCraftButtonClick()
    {
        Item item = new Item();
        item = craftedItem;
        for (int i = 0; i < needs.Count; i++)
        {
            Inventory.AddItemToinvenotry(needs[i].invenoryIndex, -needs[i].count);
            Debug.Log(needs[0].invenoryIndex + "  " + needs[0].count);
        }
        if (craftedItem.type == Item.Type.picup)
        {
            Inventory.AddItemAfterCraft(item);
        }
        SetCraftedMaterial();
    }
}
