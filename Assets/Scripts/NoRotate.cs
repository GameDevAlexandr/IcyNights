using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotate : MonoBehaviour
{
    Quaternion curPos;
    private void Start()
    {
        curPos = transform.rotation;
    }
    private void Update()
    {
        transform.rotation = curPos;
    }
}
