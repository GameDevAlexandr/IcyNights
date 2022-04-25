using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static UnityEvent changeParametrEvent = new UnityEvent();
    public static PlayerParams.pParam CurrentParams;
    public static bool IsItemPicked;
    public static Transform playerPosition;
    public static float debafStamina;
    public static float debafHealth;
    public static float debafTired;
    public static Fire selectedFire;
    [SerializeField] private  PlayerParams.pParam maxPlayerParams;
    private float changeTimer;
    private float hungerPower;
    private float temperPower;
    private float tiredPower;
    private float thirstPower;
    private float startSpeed;
    private void Start()
    {
        CurrentParams = maxPlayerParams;
        changeTimer = Time.time;
        GeneralUi.dropItemEvent.AddListener(DropItem);
        startSpeed = CurrentParams.speedMovment;
    }
    private void Update()
    {
       if(Time.time - changeTimer > 0.5f)
        {
            ChangeParam();
            changeTimer = Time.time;
        }
    }
    private void ChangeParam()
    {
        CurrentParams.speedMovment = startSpeed - CurrentParams.capacity * 0.012f;
        checkDebafs(CurrentParams.bodyTemper, ref temperPower);
        checkDebafs(CurrentParams.hunger, ref hungerPower);
        checkDebafs(CurrentParams.thirst, ref thirstPower);
        checkDebafs(CurrentParams.tired, ref tiredPower);
        float dHealth = 0;
        debafHealth = 0;
        if (temperPower > 0 || tiredPower > 0)
        {
            debafHealth = CurrentParams.healthRegeneration;
            if (temperPower == 0.2f)
            {
                dHealth += 0.5f;
            }
            if (tiredPower == 0.2f)
            {
                dHealth += 0.5f;
            }
            debafHealth += dHealth;
        }
        else
        {
            debafHealth = 0;
        }
        debafTired = temperPower + thirstPower + hungerPower;
        debafStamina = (hungerPower + thirstPower + tiredPower)*100;
        ChangerParametres(ref CurrentParams.health,  CurrentParams.healthRegeneration,debafHealth,0);
        ChangerParametres(ref CurrentParams.tired,  CurrentParams.tiredEffect, debafTired,0);
        ChangerParametres(ref CurrentParams.stamina,  CurrentParams.staminaRegeneration,0, debafStamina);
        ChangerParametres(ref CurrentParams.hunger,  CurrentParams.hungerEffect,0,0);
        ChangerParametres(ref CurrentParams.thirst,  CurrentParams.thirstEffect,0,0);
        ChangerParametres(ref CurrentParams.bodyTemper,  CurrentParams.bodyTemperRegeneration,0,0);
        changeParametrEvent.Invoke();
        
    }
    private void ChangerParametres( ref float parametr, float changeValue, float debaf, float staminaDebaf)
    {
        changeValue *= 0.5f;
        debaf *= 0.5f;
        float maxParam = 100 - staminaDebaf;
        if (maxParam < parametr)
        {
            parametr = maxParam;
        }
        if(parametr>=0 && parametr <= maxParam)
        {
            if(parametr + changeValue > maxParam)
            {
                changeValue = maxParam - parametr;
            }
            if (parametr + changeValue < 0)
            {
                changeValue = parametr;
            }
            parametr += changeValue - debaf;
        }        
    }
    private void checkDebafs(float parametr, ref float parametrPower)
    {
        if (parametr <= 60)
        {
            parametrPower = 0.1f;
        }
        if (parametr<=30 )
        {
            parametrPower = 0.2f;
        }
        if (parametr > 60)
        {
            parametrPower = 0f;
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
    public static void AddParamCount(string nameParam, float count)
    {
        switch (nameParam)
        {
            case "health": CurrentParams.health += count;

                break;
            case "temper": CurrentParams.bodyTemper += count;
                break;

        }
    }
}
