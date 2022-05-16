using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovmentPlayer))]
public class Control : MonoBehaviour
{
    public static UnityEvent<Item.Type> pickItemEvent = new UnityEvent<Item.Type>();
    public static UnityEvent ActivateCharacterPanelEvent = new UnityEvent();
    private MovmentPlayer mp;
    private float directX;
    private float directY;
    private void Start()
    {
        mp = GetComponent<MovmentPlayer>();
    }
    void FixedUpdate()
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
                ActivateCharacterPanelEvent.Invoke();
            }
        }
        mp.PlayerMove(directX, directY, Input.GetKey(KeyCode.LeftShift));
        if (Input.GetKey(KeyCode.E))
        {
            mp.cameraPosition.RotateAroundLocal(Vector3.up, 0.1f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            mp.cameraPosition.RotateAroundLocal(Vector3.up, -0.1f);
        }
        if (ContctEnvironment.IsItemPicked && Input.GetKeyDown(KeyCode.F) && !MovmentPlayer.carryHeavy)
        {
            pickItemEvent.Invoke(ContctEnvironment.item.type);
        }
        else if (Input.GetKeyDown(KeyCode.F) && MovmentPlayer.carryHeavy)
        {
            pickItemEvent.Invoke(ContctEnvironment.item.type);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            mp.isTaked = !mp.isTaked;
            mp.TakeWeapon();
        }
        if (Input.GetMouseButtonDown(0))
        {
            mp.Attack();
        }
    }
}
