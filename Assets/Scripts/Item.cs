using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum  Type
    {
        �������, ��������������, ������
    }
    [Header("��� ��������")]
    public Type type;
    [Header ("��������")]
    public string nameItem;
    [Header("����������")]
    public string discription;
    [Header("����������")]
    public int count;
    [Header("���")]
    public float weight;
    [Header("�����")]
    public float volume;
    [Header("������")]
    public Sprite icon;
}
