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
            [Header("�������")]
            public Item itemComponent;
            [Header("����������")]
            public int count;
        }
        [Header("�������� ��� ������������")]
        public ItemComponent[] itemComponents;
        [Header("������������ �������")]
        public Item result;
        [Header("������ ��� ������")]
        public bool enableToStart;
    }

    [Header("������� ��������")]
    public Item[] Items;
    [Header("�����")]
    public Craft[] crafts;
}
