using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private  PlayerParams.pParam maxPlayerParams;
    public static PlayerParams.pParam  CurrentParams;
    private float changeTimer;
    private void Start()
    {
        CurrentParams = maxPlayerParams;
        changeTimer = Time.time;
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
    }
}
