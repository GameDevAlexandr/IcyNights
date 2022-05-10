using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    public enum  Type
    {
        picup, interaction
    }
    public enum PlaceType
    {
        woodArea,
        otherArea,
        quickItem,
        weaponItem,
        lightItem,
        bedItem,
        everyDayItem
    }
    [Header("���������� �����")]
    public PlaceType placeType;
    [Header("��� ��������")]
    public Type type;
    [Header ("��������")]
    public string nameItem;
    [Header("����������")]
    public string discription;
    [Header("����������")]
    public int count;
    [Header("���� �����������")]
    public float strenght;
    [Header("���")]
    public float weight;
    [Header("������")]
    public Sprite icon;
    private Color outlineColor;
    private bool onOutline;

    private Outline outline;
    private void Awake()
    {
        if(outline = GetComponent<Outline>())
        {
            outline.enabled = false;
            outlineColor = outline.OutlineColor;
        }
        
    }
    public void OnOutline()
    {
        if (outline)
        {
            outline.enabled = true;           
            onOutline = true;
            StartCoroutine(DisableOutline());
            
        }
    }
    IEnumerator DisableOutline()
    {
        yield return new WaitForSeconds(5);
        onOutline = false;
    }
    private void FixedUpdate()
    {
        if (onOutline)
        {
            outlineColor.a = 0.7f;
            outline.OutlineColor = outlineColor;
        }
        else
        {
            outlineColor.a -= 0.01f;
            Mathf.Clamp(outlineColor.a, 0, 1);
            outline.OutlineColor = outlineColor;
            if (outlineColor.a == 0)
            {
                outline.enabled = false;
            }
        }
    }
}
