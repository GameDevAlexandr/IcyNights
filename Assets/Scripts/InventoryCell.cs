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
    private Image charPanel;
    private Transform startParent;
    [HideInInspector] public Item item;
    [HideInInspector] public int indexInInventory;
    [HideInInspector] public Text itemCount;
    private void Awake()
    {
       
    }
    private void OnEnable()
    {
        charPanel = GameObject.Find("CharacterPanel").GetComponent<Image>();
        itemButton = GetComponent<Button>();
        itemCount = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
        startTransform = new RectTransform();
        startTransform.Equals(rectTransform);
    }
    public void ClickItemButton()
    {
        GeneralUi.itemStaticPanel.SetActive(true);
        GeneralUi.imageStaticIcon.sprite = item.icon;
        GeneralUi.selectedItem = item;
        GeneralUi.selectedItemIndex = indexInInventory;
    }
    public void PutElementToCell(Item thisItem)
    {
        GeneralUi.characterPanel.SetActive(true);
        item = thisItem;
        itemButton.image.sprite = item.icon;
        itemCount.text = item.count.ToString();
        GeneralUi.characterPanel.SetActive(false);
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
