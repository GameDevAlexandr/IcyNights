using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum  Type
    {
        горючее, воспламенитель, розжиг
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
}
