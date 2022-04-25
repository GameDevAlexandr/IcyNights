using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("������ ��������")]
    [SerializeField] private float radius;
    [Header("������������ ������� ����� � �������")]
    [SerializeField] private float maxTemperChange;
    [Header("������������ �������")]
    public Fuel[] fuels;
    [Header("��������� ����� �������")]
    public float fireHelath;
    [Header("�������� ��� 2�� ������")]
    public UpgradeItem[] upgradeItems_L2;  
    [Header("�������� ��� 3�� ������")]
    public UpgradeItem[] upgradeItems_L3;
    public RenderTexture fireTextwre;
    public int currentLevel = 0;
    public GameObject level_2_Objects;
    public GameObject level_3_Objects;
    private Transform playerposition;
    private float timer;
    private float curTemperChange;
    [System.Serializable]
    public struct Fuel 
    {
       public Item item;
       public int fireHealthAdded;
    }
    [System.Serializable]
    public struct UpgradeItem
    {
        public Item item;
        public int count;
    }
    private void Start()
    {
        playerposition = GameObject.Find("Player").transform;
        timer = Time.time;
        curTemperChange = maxTemperChange;
    }
    private void Update()
    {
        if (Time.time - timer > 0.2f)
        {
            fireHelath -= 0.1f;
            GeneralUi.interPanel.health.text = fireHelath.ToString();
            if (fireHelath < 100)
            {
                curTemperChange = maxTemperChange / 200 * fireHelath;
            }
            timer = Time.time;
            if(Mathf.Abs(Vector3.Distance(playerposition.position, transform.position)) < radius)
            {
                Player.AddParamCount("temper", curTemperChange);
            }
        }
    }
}
