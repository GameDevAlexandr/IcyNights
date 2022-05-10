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
    private Vector3 oldDorection;
    private float transitionValue;
    private Vector3 staticDirection;
    public Transform cameraPosition;
    [HideInInspector] public bool isTaked;
    public GameObject activeWeapon;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        timerEActivation = Time.time;
        animator = GetComponent<Animator>();
        Control.pickItemEvent.AddListener(PickUp);
    }
    public void PlayerMove(float directionX, float directionY, bool isRun)
    {
        Vector3 direction = Vector3.zero;
        direction.x = Mathf.Cos(cameraPosition.eulerAngles.y * Mathf.PI / 180) * directionX + Mathf.Sin(cameraPosition.eulerAngles.y * Mathf.PI / 180) *  directionY;
        direction.z = Mathf.Cos(cameraPosition.eulerAngles.y * Mathf.PI / 180) * directionY + Mathf.Sin(cameraPosition.eulerAngles.y * Mathf.PI / 180) * - directionX;
        direction.Normalize();
        float directSpeed;
        if (staticDirection != direction&&direction!=Vector3.zero)
        {
            transitionValue = 0;
            oldDorection = staticDirection;
            staticDirection = direction;
        }
        if (Mathf.Abs(directionX) >= Mathf.Abs(directionY))
        {
            directSpeed = Mathf.Abs(directionX);
        }
        else
        {
            directSpeed = Mathf.Abs(directionY);
        }
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
        Vector3 velocityDirect = new Vector3(direction.x*currentSpeed, _rb.velocity.y, direction.z*currentSpeed);
        _rb.velocity = velocityDirect;
        Debug.Log(directSpeed);
        if (!isRun)
        {
            animator.SetFloat("Speed", directSpeed * 0.5f);
            Player.CurrentParams.staminaRegeneration = 3;
        }
        else
        {
            animator.SetFloat("Speed", directSpeed);
            Player.CurrentParams.staminaRegeneration = -5;
        }
       
        if (direction != Vector3.zero)
        {
            Quaternion oldRotation = Quaternion.LookRotation(oldDorection);
            Quaternion newRotation = Quaternion.LookRotation(direction);
            transitionValue += 0.05f;
            if(transitionValue >= 1)
            {
                transitionValue = 1;
            }
            transform.rotation = Quaternion.Lerp(oldRotation, newRotation, transitionValue);
            //Quaternion qu =  transform.rotation;
            //qu.y += cameraPosition.rotation.y;
            //transform.rotation = qu;
        }
        if (Time.time - timerEActivation > 1)
        {
            ActivateEnvironEvent.Invoke();
            timerEActivation = Time.time;
        }
    }
    public void PickUp(Item.Type type) 
    {
        if (type == Item.Type.picup)
        {
            animator.SetTrigger("Pick");
        }
    }
    public void TakeWeapon()
    {
        animator.SetBool("TakeWeapon", isTaked);
        if (isTaked)
        {
            activeWeapon.SetActive(true);
        }
        else
        {
            activeWeapon.SetActive(false);
        }
    }
    public void Attack()
    {
        if (isTaked)
        {
            animator.SetTrigger("Attack");
        }
    }
}
