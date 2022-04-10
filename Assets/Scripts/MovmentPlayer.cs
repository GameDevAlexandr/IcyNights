using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovmentPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody _rb;
    public static UnityEvent ActivateEnvironEvent = new UnityEvent();
    private float timerEActivation;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        timerEActivation = Time.time;
    }
    public void PlayerMove(float directionX, float directionY)
    {
        _rb.velocity = new Vector3(directionX*speed, 0, directionY*speed);
        if(Time.time - timerEActivation > 1)
        {
            ActivateEnvironEvent.Invoke();
            timerEActivation = Time.time;
        }
    }
}
