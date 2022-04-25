using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum  Type
    {
        picup, interaction
    }
    [Header("Тип предмета")]
    public Type type;
    [Header ("Название")]
    public string nameItem;
    [Header("Содержание")]
    public string discription;
    [Header("Количество")]
    public int count;
    [Header("Вес")]
    public float weight;
    [Header("Объем")]
    public float volume;
    [Header("Иконка")]
    public Sprite icon;

    private Outline outline;
    private void Awake()
    {
       outline = GetComponent<Outline>(); 
    }
    //private void Update()
    //{
    //    if(Mathf.Abs(Vector3.Distance(transform.position, Player.playerPosition.position)) < 3)
    //    {
    //        outline.enabled = true;
    //    }
    //    else
    //    {
    //        outline.enabled = false;
    //    }
    //}
}
