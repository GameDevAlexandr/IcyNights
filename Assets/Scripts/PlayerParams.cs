using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData;

public static class PlayerParams
{
    [System.Serializable]
    public struct pParam
    {
        [Header("��������� ��������")]
        public float health;
        public float healthRegeneration;
        [Header("��������� ������������")]
        public float stamina;
        public float staminaRegeneration;
        [Header("�������� �� ���������")]
        public float speedMovment;
        [Header("��������� ������")]
        public float hunger;
        public float hungerEffect;
        [Header("��������� �����")]
        public float thirst;
        public float thirstEffect;
        [Header("��������� ����������� ����")]
        public float bodyTemper;
        public float bodyTemperRegeneration;
        [Header("��������� ���������")]
        public float tired;
        public float tiredEffect;
        [Header("��������� ����������������")]
        public float capacity;
        public float capacityEffect;
    }
}
