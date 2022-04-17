using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static PlayerParams.pParam CurrentParams;
    public static bool IsItemPicked;
    public static Transform playerPosition;
    [SerializeField] private  PlayerParams.pParam maxPlayerParams;    
    private float changeTimer;
    private void Start()
    {
        CurrentParams = maxPlayerParams;
        changeTimer = Time.time;
        GeneralUi.dropItemEvent.AddListener(DropItem);

    }
    private void Update()
    {
       if(Time.time - changeTimer > 1)
        {
            ChangeParam();
            changeTimer = Time.time;
        }
    }
    private void ChangeParam()
    {
        if (CurrentParams.health < maxPlayerParams.health)
        {
            CurrentParams.health += CurrentParams.healthRegeneration;
        }
        if(CurrentParams.stamina < maxPlayerParams.stamina)
        {
            CurrentParams.stamina += CurrentParams.staminaRegeneration;
        }
        if(CurrentParams.hunger<maxPlayerParams.hunger)
        {
            CurrentParams.hunger += CurrentParams.hungerEffect;
        }
    }
    private void DropItem(Item item)
    {
        Item curItem;
        for (int i = 0; i < GameData.DataSingleton.ItemsData.Items.Length; i++)
        {
            if (GameData.DataSingleton.ItemsData.Items[i].nameItem == item.nameItem)
            {
                curItem = GameData.DataSingleton.ItemsData.Items[i];
                GameObject newItem = Instantiate(curItem.gameObject);
                for (int j = 0; j < 10; j++)
                {
                    Vector3 dropPosition = new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y, transform.position.z + Random.Range(-1.0f, 1.0f));
                    Ray _ray = new Ray(dropPosition, Vector3.down);
                    RaycastHit _hit;
                    if (Physics.Raycast(_ray, out _hit, 3.0f, LayerMask.GetMask("Ground")))
                    {
                        newItem.transform.position = _hit.point;
                        break;
                    }
                 
                }
                break;
            }
        }
    }
}
