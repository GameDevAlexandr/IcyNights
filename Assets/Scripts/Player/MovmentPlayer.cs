using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Animator))]
public class MovmentPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody _rb;
    public static UnityEvent ActivateEnvironEvent = new UnityEvent();
    private float timerEActivation;
    private Animator animator;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        timerEActivation = Time.time;
        animator = GetComponent<Animator>();
        Control.pickItemEvent.AddListener(PickUp);
    }
    public void PlayerMove(float directionX, float directionY, bool isRun)
    {
        float currentSpeed = speed;
        if (isRun)
        {
            currentSpeed = speed*3;
        }
        else
        {
            currentSpeed = speed;
        }
        if (directionX != 0 || directionY != 0)
        {
            _rb.velocity = new Vector3(directionX * currentSpeed, 0, directionY * currentSpeed);
            if (!isRun)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
            }
            else
            {
                animator.SetBool("Run", true);
                animator.SetBool("Walk", true);
            }
            Vector3 direction = Vector3.zero;
            direction.x = directionX;
            direction.z = directionY;
            transform.rotation = Quaternion.LookRotation(direction);
            if (Time.time - timerEActivation > 1)
            {
                ActivateEnvironEvent.Invoke();
                timerEActivation = Time.time;
            }
        }
        else
        {
            animator.SetBool("Walk",false);
            animator.SetBool("Run", false);
        }
    }
    public void PickUp() 
    {
        animator.SetTrigger("Pick");
    }
}
