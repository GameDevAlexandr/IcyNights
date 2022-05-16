using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryClass : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public GameObject inventoryPanel;

    public TMP_Text weightText;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<ItemClass>();
        if (item)
        {
            ItemFix _item = new ItemFix(item.item);
            if (inventory.AddItem(_item, item.amount))
                Destroy(other.gameObject);
        }
    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        ToggleInventory();

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            inventory.Save();
            equipment.Save();
        }
       
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            equipment.Load();
        }
    }

    private void LateUpdate()
    {
        weightText.text = "Вес: " + inventory.currentWeight.ToString("0.0") + " / " + inventory.maxWeight.ToString("0.0") + "кг";
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
        inventory.currentWeight = 0;
    }

    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) && inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
            //space for animation//
        }
        else if (Input.GetKeyDown(KeyCode.I) && !inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(true);
            //space for animation//
        }
    }

/*    public void WeightCheck()
    {
        List<InventorySlot> notChecked = new List<InventorySlot> = inventory.Container.items;
        InventorySlot[] isChecked = new InventorySlot[30];

        for (int i = 0; i < notChecked.Length; i++)
        {
            foreach (InventorySlot itemObj in inventory.Container.items)
            {
                if (itemObj.ID > -1)
                {
                    float weight = itemObj.weightPerOne * itemObj.amount;
                    currentWeight += weight;
                    weightText.text = "Вес: " + currentWeight.ToString("n0") + " / " + maxWeight.ToString("n0");
                    notChecked[i].
                }
                return;
            }
        }
    }*/


}
