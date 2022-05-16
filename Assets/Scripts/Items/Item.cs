using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    public enum  Type
    {
        picup, interaction, heavy
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
    [Header("Занимаемое место")]
    public PlaceType placeType;
    [Header("Тип предмета")]
    public Type type;
    [Header ("Название")]
    public string nameItem;
    [Header("Содержание")]
    public string discription;
    [Header("Количество")]
    public int count;
    [Header("Сили воздействия")]
    public float strenght;
    [Header("Вес")]
    public float weight;
    [Header("Иконка")]
    public Sprite icon;
    private Color outlineColor;
    private bool onOutline;

    private Outline outline;
    private void Awake()
    {
        if(outline = GetComponent<Outline>())
        {
            outlineColor = outline.OutlineColor;
        }
        
    }
    public void OnOutline()
    {
        if (outline)
        {
            onOutline = true;
            StartCoroutine(DisableOutline());
            
        }
    }
    IEnumerator DisableOutline()
    {
        yield return new WaitForSeconds(2);
        onOutline = false;
    }
    private void FixedUpdate()
    {
        if (outline)
        {
            if (onOutline)
            {
                outlineColor.a = 0.7f;
                outline.OutlineColor = outlineColor;
            }
            else
            {
                outlineColor.a -= 0.01f;
                //Mathf.Clamp(outlineColor.a, 0, 1);
                if (outlineColor.a < 0)
                {
                    outlineColor.a = 0;
                }
                outline.OutlineColor = outlineColor;
            }
        }
    }
}
