using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Control))]
public class ContctEnvironment : MonoBehaviour
{
    public static Item Item;
    public static bool IsItemPicked;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Item = other.GetComponent<Item>();
            GeneralUi.hint.SetActive(true);

            if (Player.CurrentParams.capacity + Item.weight > Player.CurrentParams.maxCapacity)
            {
                GeneralUi.hintText.text = "Слишком тяжело";
            }
            else
            {
                IsItemPicked = true;
                GeneralUi.hintText.text = Item.nameItem;
            }
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            IsItemPicked = false;
            GeneralUi.hint.SetActive(false);
        }
    }
}
