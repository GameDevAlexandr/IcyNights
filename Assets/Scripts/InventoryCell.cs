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
    [HideInInspector] public Item item { get { return _item; } set { _item = value; PutElementToCell(); }}
    [HideInInspector] public int indexInInventory;
    [HideInInspector] public Text itemCount;
    [HideInInspector] public bool usible = false;
    [HideInInspector] public int strenght;
    private Item _item;
    private void Awake()
    {
        Inventory.changeItemCountEvent.AddListener(changeItemCount);
    }
    private void OnEnable()
    {
        itemButton = GetComponent<Button>();
        itemCount = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
        startTransform = new RectTransform();
        startTransform.Equals(rectTransform);
    }
    public void ClickItemButton()
    {
        if (!usible)
        {
            GeneralUi.itemStaticPanel.SetActive(true);
            GeneralUi.imageStaticIcon.sprite = item.icon;
            GeneralUi.selectedItem = item;
            GeneralUi.selectedItemIndex = indexInInventory;
        }
        else
        {
            ContctEnvironment.item.GetComponent<Fire>().fireHelath += strenght;
            Inventory.AddItemToinvenotry(indexInInventory, -1);
        }
    }
    public void PutElementToCell()
    {
        GeneralUi.characterPanel.SetActive(true);
        itemButton.image.sprite = _item.icon;
        itemCount.text = _item.count.ToString();
        GeneralUi.characterPanel.SetActive(false);
    }
    private void changeItemCount()
    {
        if (Inventory.itemsInInventory[indexInInventory].count > 0)
        {
            itemCount.text = Inventory.itemsInInventory[indexInInventory].count.ToString();
        }
        else
        {
            if (Inventory.itemsInInventory[indexInInventory])
            {
                Inventory.itemsInInventory.RemoveAt(indexInInventory);
            }
            //indexInInventory = Inventory.itemsInInventory.Count;
            Destroy(gameObject);
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
