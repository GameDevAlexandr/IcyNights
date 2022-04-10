using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private MovmentPlayer mp;
    private float directX;
    private float directY;
    private void Start()
    {
        mp = GetComponent<MovmentPlayer>();
    }
    void Update()
    {
        directX = Input.GetAxisRaw("Horizontal");
        directY = Input.GetAxisRaw("Vertical");
        mp.PlayerMove(directX, directY);
    }
}
