using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Control))]
public class ContctEnvironment : MonoBehaviour
{
    public static Item item;
    public static bool IsItemPicked;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            item = other.GetComponent<Item>();
            GeneralUi.hint.SetActive(true);

            if (Player.CurrentParams.capacity + item.weight > Player.CurrentParams.maxCapacity)
            {
                GeneralUi.hintText.text = "Слишком тяжело";
            }
            else
            {
                IsItemPicked = true;
                GeneralUi.hintText.text = item.nameItem;
            }
        }
        if(other.tag == "Interection")
        {
            item = other.GetComponent<Item>();
            GeneralUi.hint.SetActive(true);
            GeneralUi.hintText.text = item.nameItem;
            IsItemPicked = true;
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
