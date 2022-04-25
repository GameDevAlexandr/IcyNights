using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovmentPlayer))]
public class Control : MonoBehaviour
{
    public static UnityEvent<Item.Type> pickItemEvent = new UnityEvent<Item.Type>();
    private MovmentPlayer mp;
    private float directX;
    private float directY;
    private void Start()
    {
        mp = GetComponent<MovmentPlayer>();
    }
    void Update()
    {
        directX = Input.GetAxisRaw("Horizontal");
        directY = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GeneralUi.characterPanel.activeSelf) 
            {
                GeneralUi.characterPanel.SetActive(false);
            }
            else
            {
                GeneralUi.characterPanel.SetActive(true);
            }
        }
        mp.PlayerMove(directX, directY, Input.GetKey(KeyCode.LeftShift));
        if(ContctEnvironment.IsItemPicked && Input.GetKeyDown(KeyCode.E))
        {
            pickItemEvent.Invoke(ContctEnvironment.item.type);
        }
    }
}
