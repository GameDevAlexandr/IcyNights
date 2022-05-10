using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private ParticleSystem flame;
    [SerializeField] private float burningTime;
    [HideInInspector] public bool isBurn;
    private void Update()
    {
        if (isBurn)
        {
            burningTime -= Time.deltaTime;
            if (burningTime > 0)
            {
                flame.gameObject.SetActive(true);
            }
        }
    }
    private void OnDisable()
    {
        flame.gameObject.SetActive(false);
        isBurn = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Interection")
        {
            isBurn = true;
        }
    }
}
