using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{
    public InventoryObject inventory;

    public Vector2 iconDimensions = new Vector2(100, 100);

    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    private void Start()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].onAfterUpdate += OnSlotUpdate;
        }

        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    private void OnSlotUpdate(InventorySlot slot)
    {
        if (slot.item.id >= 0)
        {
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.invItem.visualComponent;
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount == 1 ? "" : slot.amount.ToString("n0");
        }
        else
        {
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public abstract void CreateSlots();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventtrigger = new EventTrigger.Entry();
        eventtrigger.eventID = type;
        eventtrigger.callback.AddListener(action);
        trigger.triggers.Add(eventtrigger);
    }

    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
    }

    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }

    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
    }

    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }

    public void OnDragStart(GameObject obj)
    {
        if (slotsOnInterface[obj].item.id <= -1)
            return;

        var mouseObj = new GameObject();
        var rt = mouseObj.AddComponent<RectTransform>();
        rt.sizeDelta = iconDimensions;
        mouseObj.transform.SetParent(transform.parent);
        var img = mouseObj.AddComponent<Image>();
        img.sprite = slotsOnInterface[obj].invItem.visualComponent;
        img.raycastTarget = false;

        MouseData.tempItemBeingDragged = mouseObj;
    }

    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);

        /*if (itemOnMouse.ui != null)
        {
            if (mouseHoverObj)
                if (mouseHoverItem.CanPlace(getItemObj[itemsDisplayed[obj].ID]) && (mouseHoverItem.item.id <= -1 || (mouseHoverItem.item.id >= 0 && itemsDisplayed[obj].CanPlace(getItemObj[mouseHoverItem.item.id]))))
                    inventory.MoveItem(itemsDisplayed[obj], mouseHoverItem.parent.itemsDisplayed[itemOnMouse.hoverObj]);
        }
        else
        {
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }*/

        if (MouseData.interfaceMouseIsOver == null)
        {
            var weight = slotsOnInterface[obj].amount * slotsOnInterface[obj].weightPerOne;
            inventory.RemoveWeight(weight);

            slotsOnInterface[obj].RemoveItem();

            return;
        }

        if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            inventory.SwapItem(slotsOnInterface[obj], mouseHoverSlotData);

            if (slotsOnInterface[obj].item.id == mouseHoverSlotData.item.id)
            {
                mouseHoverSlotData.AddAmount(slotsOnInterface[obj].amount);
                slotsOnInterface[obj].UpdateSlot(new ItemFix(), 0);
            }
        }

    }

    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    /*    private void OnDrawGizmos()

        {
            for (int i = 0; i < inventory.Container.items.Length; i++)
            {
                var position = GetPosition(i); // This is the raw placement position

                // Uses panel's current position for pivot
                Gizmos.DrawCube(new Vector2(transform.position.x + position.x, transform.position.y + position.y), new Vector2(50, 50));
            }
        }*/
}

public static class MouseData
{
    public static UserInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;

}
