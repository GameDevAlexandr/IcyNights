using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Control))]
public class ContctEnvironment : MonoBehaviour
{
    private Control control;
    private Inventory inventory;

    private void Awake()
    {
        control = GetComponent<Control>();
        inventory = GetComponent<Inventory>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Player.IsItemPicked = true;
            inventory.curentItem = other.GetComponent<Item>();
            GeneralUi.hint.SetActive(true);
            GeneralUi.hintText.text = inventory.curentItem.nameItem;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            Player.IsItemPicked = false;
            GeneralUi.hint.SetActive(false);
        }
    }

}
