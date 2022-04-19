using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    [SerializeField] private Transform ground;
    [SerializeField] private Transform playerTransform;
    private ParticleSystem step;
    private AudioSource stepSound;
    private bool isStep; 
    private void Start()
    {
        step = GetComponent<ParticleSystem>();
        stepSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        float rotation = playerTransform.eulerAngles.y;
        step.startRotation = (90 + rotation) / 55.55f;
        if (ground.position.y-transform.position.y > 0 && !isStep)
        {
            step.Play();
            isStep = true;
            stepSound.Play();
            stepSound.pitch = Random.Range(0.8f, 1.2f);
        }
        if(ground.position.y - transform.position.y < 0)
        {
            isStep = false;
        }
    }
}
