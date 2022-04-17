using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemsData", menuName = "ScriptableObjects/ItemsData", order = 1)]
public class ItemsData : ScriptableObject
{


    [System.Serializable]
    public struct Craft
    {
        [System.Serializable]
        public struct ItemComponent
        {
            [Header("Элемент")]
            public Item itemComponent;
            [Header("Количество")]
            public int count;
        }
        [Header("Элементы для производства")]
        public ItemComponent[] itemComponents;
        [Header("Производимый элемент")]
        public Item result;
        [Header("Дается при старте")]
        public bool enableToStart;
    }

    [Header("Игровые элементы")]
    public Item[] Items;
    [Header("Крафт")]
    public Craft[] crafts;
}
