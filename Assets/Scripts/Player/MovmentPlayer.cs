using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Animator))]
public class MovmentPlayer : MonoBehaviour
{
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
        float currentSpeed = Player.CurrentParams.speedMovment;
        if (isRun)
        {
            currentSpeed = Player.CurrentParams.speedMovment * 3;
        }
        else
        {
            currentSpeed = Player.CurrentParams.speedMovment;
        }
        animator.speed = Player.CurrentParams.speedMovment / 2;
        if (directionX != 0 || directionY != 0)
        {
            _rb.velocity = new Vector3(directionX * currentSpeed, _rb.velocity.y, directionY * currentSpeed);
            if (!isRun)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                Player.CurrentParams.staminaRegeneration = 3;
            }
            else
            {
                animator.SetBool("Run", true);
                animator.SetBool("Walk", true);
                Player.CurrentParams.staminaRegeneration = -5;
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
    public void PickUp(Item.Type type) 
    {
        if (type == Item.Type.picup)
        {
            animator.SetTrigger("Pick");
        }
    }
}
