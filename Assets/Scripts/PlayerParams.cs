using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData;

public static class PlayerParams
{
    [System.Serializable]
    public struct pParam
    {
        [Header("Параметры здоровья")]
        public float health;
        public float healthRegeneration;
        [Header("Параметры выносливости")]
        public float stamina;
        public float staminaRegeneration;
        [Header("Скорость по умолчанию")]
        public float speedMovment;
        [Header("Параметры голода")]
        public float hunger;
        public float hungerEffect;
        [Header("Параметры жажды")]
        public float thirst;
        public float thirstEffect;
        [Header("Параметры температуры тела")]
        public float bodyTemper;
        public float bodyTemperRegeneration;
        [Header("Параметры усталости")]
        public float tired;
        public float tiredEffect;
        [Header("Параметры грузоподъемности")]
        public float capacity;
        public float capacityEffect;
    }
}
