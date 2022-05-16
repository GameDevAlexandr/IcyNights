using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 startPosition;
    [SerializeField] private float maxDistance;
    [SerializeField] private float stayTime;
    [SerializeField] private float speed;
    private float stayTimer;
    private Animator animator;
    private bool isStay =true;
    // Start is called before the first frame update
    void Start()
    {
        stayTimer = float.MaxValue;
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = NewPositionForAgent();
        agent.speed = speed;
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        float pathDistance = agent.remainingDistance;
        if (pathDistance<=5)
        {
            agent.speed = 0;
            animator.SetBool("Walk", false);
            if (isStay)
            {
                stayTimer = Time.time;
                isStay = false;
            }            
        }
        if (Time.time - stayTimer > stayTime)
        {
            Vector3 newPosition = NewPositionForAgent();
            agent.SetDestination(newPosition);
            agent.speed = speed;
            animator.SetBool("Walk", true);
            stayTimer = float.MaxValue;
            isStay = true;
        }
    }
    private Vector3 NewPositionForAgent()
    {
        return new Vector3(startPosition.x + Random.Range(-maxDistance, maxDistance), startPosition.y, startPosition.z + Random.Range(-maxDistance, maxDistance));
    }
}
