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
    public static IntiractionPanel interPanel;
    //public static Button useInterButton;

    [SerializeField] private GameObject charPanel;
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private Image inventPanel;
    [SerializeField] private GameObject itemCellPrefab;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private Image imageIcon;
    [Header("Healt, Vigor, Stamina, Hunger, Thirst, Temper")]
    [SerializeField] private Image[] parametrIndicators = new Image[6];
    [SerializeField] private Text capacityText;
    [SerializeField] private IntiractionPanel intiractionPanel;
    //private static List<InventoryCell> inventoryCells = new List<InventoryCell>();
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
        interPanel = intiractionPanel;
        Player.changeParametrEvent.AddListener(SetIndicatorsValue);
    }
    public static void PutItemToInventory(int index)
    {
            GameObject newCell = Instantiate(itemCell, inventoryPanel.transform);
            InventoryCell inventoryCell = newCell.GetComponent<InventoryCell>();
            inventoryCell.item = Inventory.itemsInInventory[index];
    }
    public static void DropItem( bool droped)
    {
        if (Inventory.itemsInInventory[selectedItemIndex].count == 1)
        {
            itemStaticPanel.SetActive(false);
        }
        if (Inventory.itemsInInventory[selectedItemIndex].count > 0)
        {
            Inventory.AddItemToinvenotry(selectedItemIndex,-1);
            if (droped)
            {
                dropItemEvent.Invoke(selectedItem); //Player listner
            }
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
    public static void ActivateInteractionPanel(bool isActive, Item item)
    {
        interPanel.gameObject.SetActive(isActive);
        Fire fire;
        if(fire = item.GetComponent<Fire>())
        {
            interPanel.icon.texture = fire.fireTextwre;
            for (int i = 0; i < fire.fuels.Length; i++)
            {
                for (int j = 0; j < Inventory.itemsInInventory.Count; j++)
                {
                    if (fire.fuels[i].item.nameItem == Inventory.itemsInInventory[j].nameItem && 
                        !interPanel.ChecContent(Inventory.itemsInInventory[j])&& Inventory.itemsInInventory[j].count>0)
                    {
                        GameObject newCell = Instantiate(itemCell, interPanel.content.transform);
                        InventoryCell iCell = newCell.GetComponent<InventoryCell>();
                        interPanel.inventoryCells.Add(iCell);
                        iCell.item = Inventory.itemsInInventory[j];
                        iCell.strenght = fire.fuels[i].fireHealthAdded;
                        iCell.indexInInventory = j;
                        iCell.usible = true;
                    }
                }
            }
            if (fire.currentLevel == 0)
            {
                for (int i = 0; i < fire.upgradeItems_L2.Length; i++)
                {

                }
            }
        }
    }


}
