using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowGizmos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direct = transform.localPosition;
        direct.y += 5;
        Gizmos.DrawLine(transform.position, direct);
    }
    private void Update()
    {
        OnDrawGizmos();
    }
}
