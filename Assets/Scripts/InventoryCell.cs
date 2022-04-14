using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] 
public class InventoryCell : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IDropHandler
{
    private Text itemCount;
    private Button itemButton;
    private RectTransform rectTransform;
    private RectTransform startTransform;
    private Image charPanel;
    private int itemIndex;
    private Item item;
    private Transform startParent;
    private void Awake()
    {
        itemButton = GetComponent<Button>();
        itemCount = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
        startTransform = new RectTransform();
        startTransform.Equals(rectTransform);
    }
    private void OnEnable()
    {
        charPanel = GameObject.Find("CharacterPanel").GetComponent<Image>();
        
    }
    public void PickElementToCell(int index)
    {
        itemIndex = index;
        item = Inventory.itemCells[index].Item;
        itemCount.text = item.count.ToString();            
        itemButton.image.sprite = item.icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startParent = transform.parent;
        transform.parent = charPanel.transform;
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
