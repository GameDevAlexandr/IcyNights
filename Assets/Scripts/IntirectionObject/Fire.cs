using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float maxTemperChange;
    [SerializeField] private float burnOutPerSecond;
    public float fireHelath;
    private Transform playerposition;
    private float timer;
    private float curTemperChange;
    private void Start()
    {
        playerposition = GameObject.Find("Player").transform;
        timer = Time.time;
        curTemperChange = maxTemperChange;
    }
    private void Update()
    {
        if (Time.time - timer > burnOutPerSecond* 0.5f)
        {
            fireHelath -= 0.5f;
            if (fireHelath < 100)
            {
                curTemperChange = maxTemperChange / 100 * fireHelath;
            }
            timer = Time.time;
            if(Mathf.Abs(Vector3.Distance(playerposition.position, transform.position)) < radius)
            {
                Player.AddParamCount("temper", curTemperChange);
            }
        }
    }
}
