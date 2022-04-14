using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToPlay : MonoBehaviour
{
    [SerializeField] Transform target;
    private Vector3 startPositonTarget;
    private Vector3 startPositon;
    private void Start()
    {
        startPositonTarget = target.position;
        startPositon = transform.position;
    }
    private void Update()
    {
        Vector3 difference = target.position - startPositonTarget;
        Vector3 newPosition = startPositon + difference;
        transform.position = newPosition;
    }
}
