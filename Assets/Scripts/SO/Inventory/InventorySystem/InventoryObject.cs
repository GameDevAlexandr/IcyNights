using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using TMPro;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]


public class InventoryObject : ScriptableObject
{
    [Header("Inventory settings")]
    public string savePath;
    public ItemDatabase database;
    public InventoryFix Container;
    public InventorySlot[] GetSlots { get { return Container.Slots; } }

    [Header("Weight Settings")]
    public float maxWeight;
    public float currentWeight;


    /*    private void OnEnable()
        {
    #if UNITY_EDITOR
            database = (ItemDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabase));
    #else
            database = Resources.Load<ItemDatabase>("Database");
    #endif
        }*/

/*    public void AddItem(ItemFix itemObj, int amnt)
    {
        if (!itemObj.canStack)
        {
            SetEmptySlot(itemObj, amnt);
            currentWeight += itemObj.weightPerOne * amnt;
            return;
        }

        for (int i = 0; i < Container.items.Length; i++)
        {
            if (Container.items[i].item.id == itemObj.id)
            {
                Container.items[i].AddAmount(amnt);
                currentWeight += itemObj.weightPerOne * amnt;
                return;
            }
        }
        SetEmptySlot(itemObj, amnt);

    }*/

   /* public void AddItem(ItemFix itemObj, int amnt)
    {
        if (itemObj.canStack)
        {
            for (int i = 0; i < Container.items.Length; i++)
            {
                if (Container.items[i].item.id == itemObj.id && Container.items[i].amount < itemObj.maxStackAmount)
                {
                    int a = Container.items[i].amount + amnt;

                    if (a > itemObj.maxStackAmount)
                    {
                        int adding = itemObj.maxStackAmount - a;
                        Debug.Log(adding);
                        Container.items[i].AddAmount(-adding);

                        int extent = a - itemObj.maxStackAmount;
                        Debug.Log(extent);

                        return;
                    }
                    else
                    {
                        Container.items[i].AddAmount(amnt);
                        Debug.Log("triggered add amount");
                        return;
                    }
                }
            }

            Debug.Log("triggered after 'for'");
        }
        else
        {
            Container.items.Add(new InventorySlot(itemObj.id, itemObj, amnt));
            Debug.Log("triggered last else");
            return;
        }
    }*/

    public bool AddItem(ItemFix itemObj, int amnt)
    {
        if (EmptySlotCount <= 0)
        {
            return false;
        }

        InventorySlot slot = FindItemOnInventory(itemObj);

        if (!database.GetItem[itemObj.id].canStack || slot == null)
        {
            SetEmptySlot(itemObj, amnt);
            currentWeight += itemObj.weightPerOne * amnt;
            return true;
        }
        slot.AddAmount(amnt);
        currentWeight += itemObj.weightPerOne * amnt;
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.id <= -1)
                    counter++;
            }
            return counter;
        }
    }

    public InventorySlot FindItemOnInventory(ItemFix item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.id == item.id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }

    public InventorySlot SetEmptySlot(ItemFix item, int amnt)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.id <= -1)
            {
                GetSlots[i].UpdateSlot(item, amnt);
                return GetSlots[i];

            }
        }
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
        //                                             //
        // ~~set up functions for inventory is full~~  //
        //                                             //
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
        return null;
    }

    public void SwapItem(InventorySlot item1, InventorySlot item2)
    {
        if(item2.CanPlace(item1.invItem) && item1.CanPlace(item2.invItem))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }
    }

    //This might require Isntantiate and stuff to actually drop an item not to destroy

    /*public void RemoveItem(ItemFix item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (Container.Slots[i].item == item)
            {
                currentWeight -= item.weightPerOne * GetSlots[i].amount;
                Debug.Log(currentWeight + " name: " + item + " Weight: " + item.weightPerOne + " Amount: " + GetSlots[i].amount);
            }
        }
    }*/

    public void RemoveWeight(float amnt)
    {
        currentWeight -= amnt;
    }

    [ContextMenu("Save")]
    public void Save()
    {
        /*        string saveData = JsonUtility.ToJson(this, true);
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
                bf.Serialize(file, saveData);
                file.Close();*/
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        formatter.Serialize(stream, currentWeight);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            /*            BinaryFormatter bf = new BinaryFormatter();
                        FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                        file.Close();*/

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            InventoryFix newContainer = (InventoryFix)formatter.Deserialize(stream);
            currentWeight = (float)formatter.Deserialize(stream);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
        currentWeight = 0;
    }
}

[System.Serializable]
public class InventoryFix
{
    public InventorySlot[] Slots = new InventorySlot[24];
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].UpdateSlot(new ItemFix(), 0);
        }
    }
}

public delegate void SlotUpdated(InventorySlot slot);

[System.Serializable]
public class InventorySlot
{
    //public InventoryClass inventoryClass;
    [System.NonSerialized] public UserInterface parent;
    [System.NonSerialized] public GameObject slotDisplay;
    [System.NonSerialized] public SlotUpdated onAfterUpdate;
    [System.NonSerialized] public SlotUpdated onBeforeUpdate;
    public ItemType[] Allowed = new ItemType[0];
    public ItemFix item = new ItemFix();
    public int amount;
    public float weightPerOne;

    public InventoryItem invItem
    {
        get
        {
            if (item.id >= 0)
            {
                return parent.inventory.database.GetItem[item.id];
            }
            return null;
        }
    }

    public InventorySlot()
    {
        UpdateSlot(new ItemFix(), 0);
    }

    public InventorySlot(ItemFix itemObj, int amnt)
    {
        UpdateSlot(itemObj, amnt);
        weightPerOne = itemObj.weightPerOne;
    }

    public void UpdateSlot(ItemFix itemObj, int amnt)
    {
        if (onBeforeUpdate != null)
            onBeforeUpdate.Invoke(this);
        item = itemObj;
        amount = amnt;
        weightPerOne = itemObj.weightPerOne;
        if (onAfterUpdate != null)
            onAfterUpdate.Invoke(this);
    }

    public void RemoveItem()
    {
        UpdateSlot(new ItemFix(), 0);
    }

    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value);
    }

    public bool CanPlace(InventoryItem item)
    {
        if (Allowed.Length <= 0 || item == null || item.data.id < 0)
            return true;

        for (int i = 0; i < Allowed.Length; i++)
        {
            if (item.type == Allowed[i])
                return true;
        }
        return false;
    }
}