using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GeneralUi : MonoBehaviour
{
    public static GameObject characterPanel;
    public static GameObject hint;
    public static Text hintText;
    public static Image inventoryPanel;
    public static GameObject itemStaticPanel;
    public static Image imageStaticIcon;
    public static Item selectedItem;
    public static UnityEvent<Item> dropItemEvent = new UnityEvent<Item>();
    public static int selectedItemIndex;

    [SerializeField] private GameObject charPanel;
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private Image inventPanel;
    [SerializeField] private GameObject itemCellPrefab;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private Image imageIcon;
    [Header("Healt, Vigor, Stamina, Hunger, Thirst, Temper")]
    [SerializeField] private Image[] parametrIndicators = new Image[6];
    [SerializeField] private Text capacityText;
    private static List<InventoryCell> inventoryCells = new List<InventoryCell>();
    private static GameObject itemCell;
 
    private void Awake()
    {
        characterPanel = charPanel;
        hint = hintPanel;
        hint.SetActive(true);
        hintText = GetComponentInChildren<Text>();
        hint.SetActive(false);
        inventoryPanel = inventPanel;
        itemCell = itemCellPrefab;
        itemStaticPanel = itemPanel;
        imageStaticIcon = imageIcon;
        Player.changeParametrEvent.AddListener(SetIndicatorsValue);
    }
    public static void PutItemToInventory(int index)
    {
        if (index > inventoryCells.Count - 1)
        {
            GameObject newCell = Instantiate(itemCell, inventoryPanel.transform);
            InventoryCell inventoryCell = newCell.GetComponent<InventoryCell>();
            inventoryCell.PutElementToCell(Inventory.itemsInInventory[index]);
            inventoryCell.indexInInventory = inventoryCells.Count;
            inventoryCells.Add(inventoryCell);
        }
        else
        {
            inventoryCells[index].PutElementToCell(Inventory.itemsInInventory[index]);
        }
    }
    public void DropItem()
    {
        if(Inventory.itemsInInventory[selectedItemIndex].count > 0)
        {
            Inventory.itemsInInventory[selectedItemIndex].count--;
            inventoryCells[selectedItemIndex].itemCount.text = Inventory.itemsInInventory[selectedItemIndex].count.ToString();
            dropItemEvent.Invoke(selectedItem); //Player listner
        }
        if (Inventory.itemsInInventory[selectedItemIndex].count == 0)
        {
            Destroy(inventoryCells[selectedItemIndex].gameObject);
            inventoryCells.RemoveAt(selectedItemIndex);
            Inventory.itemsInInventory.RemoveAt(selectedItemIndex);
            itemPanel.SetActive(false);
        }
    }
    private void SetIndicatorsValue()
    {
        parametrIndicators[0].fillAmount = Player.CurrentParams.health/100;
        parametrIndicators[1].fillAmount = Player.CurrentParams.tired/100;
        parametrIndicators[2].fillAmount = Player.CurrentParams.stamina/100;
        parametrIndicators[3].fillAmount = Player.CurrentParams.hunger/100;
        parametrIndicators[4].fillAmount = Player.CurrentParams.thirst/100;
        parametrIndicators[5].fillAmount = Player.CurrentParams.bodyTemper/100;
        capacityText.text = Player.CurrentParams.capacity.ToString();
    }

}
