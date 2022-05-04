using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] 
public class InventoryCell : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IDropHandler
{
    private Button itemButton;
    private RectTransform rectTransform;
    private RectTransform startTransform;
    private Transform startParent;
    [HideInInspector] public Item item { get { return _item; } set { _item = value; }}
    [HideInInspector] public int indexInInventory = 999;
    [HideInInspector] public Text itemCount;
    //[HideInInspector] public bool usible = false;
    [HideInInspector] public int strenght;
    [HideInInspector] public ButtonType buttonType;
    [HideInInspector] public enum ButtonType 
    {
        invent,
        useble,
        craft
    }
    private Item _item;
    private void Awake()
    {
        Inventory.changeItemCountEvent.AddListener(changeItemCount);
        buttonType = ButtonType.invent;
        Control.ActivateCharacterPanelEvent.AddListener(PutElementToCell);
    }
    private void OnEnable()
    {
        itemButton = GetComponent<Button>();
        itemCount = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
        startTransform = new RectTransform();
        startTransform.Equals(rectTransform);
        //PutElementToCell();
    }
    public void ClickItemButton()
    {
        if (buttonType == ButtonType.invent)
        {
            GeneralUi.itemStaticPanel.SetActive(true);
            GeneralUi.imageStaticIcon.sprite = item.icon;
            GeneralUi.selectedItem = item;
            GeneralUi.selectedItemIndex = indexInInventory;
        }
        else if(buttonType == ButtonType.useble)
        {
            ContctEnvironment.item.GetComponent<Fire>().fireHelath += strenght;
            Inventory.AddItemToinvenotry(indexInInventory, -1);
        }
        else
        {
            GeneralUi.craftPanel.craftedItem = _item;
            GeneralUi.craftPanel.SetCraftedMaterial();
        }
    }
    public void PutElementToCell()
    {
        Debug.Log(buttonType);
        if (buttonType != ButtonType.craft)
        {
            Debug.Log(Inventory.itemsInInventory[indexInInventory].count);
            itemCount.text = Inventory.itemsInInventory[indexInInventory].count.ToString();
        }
        itemButton.image.sprite = _item.icon;
    }
    private void changeItemCount(bool IsDestroed, int index)
    {
        if (index == indexInInventory)
        {
            if (!IsDestroed)
            {
                itemCount.text = Inventory.itemsInInventory[indexInInventory].count.ToString();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnDrop(PointerEventData eventData)
    {
        rectTransform = startTransform;
        transform.parent = startParent;
    }
}
