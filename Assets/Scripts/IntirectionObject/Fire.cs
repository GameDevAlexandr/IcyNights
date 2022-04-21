using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float temperChange;
    private Transform playerposition;
    private float timer;
    private void Start()
    {
        playerposition = GameObject.Find("Player").transform;
        timer = Time.time;
    }
    private void Update()
    {
        if (Time.time - timer > 0.5f)
        {
            timer = Time.time;
            if(Mathf.Abs(Vector3.Distance(playerposition.position, transform.position)) < radius)
            {
                Player.AddParamCount("temper", 3);
            }
        }
    }
}
