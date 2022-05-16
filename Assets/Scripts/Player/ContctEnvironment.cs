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
        if (other.tag == "Item" && !MovmentPlayer.carryHeavy)
        {
            item = other.GetComponent<Item>();
            GeneralUi.hint.SetActive(true);
            if (item.type == Item.Type.picup)
            {
                if (Player.CurrentParams.capacity + item.weight > Player.CurrentParams.maxCapacity)
                {
                    GeneralUi.hintText.text = "������� ������";
                    return;
                }
            }
            IsItemPicked = true;
            GeneralUi.hintText.text = item.nameItem;
        }
        if(other.tag == "Roof")
        {
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item"|| other.tag == "Interection")
        {
            IsItemPicked = false;
            GeneralUi.hint.SetActive(false);
            GeneralUi.interPanel.gameObject.SetActive(false);
        }
        if (other.tag == "Roof")
        {
            other.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
