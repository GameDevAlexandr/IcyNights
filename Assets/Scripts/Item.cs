using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum  Type
    {
        picup, interaction
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
