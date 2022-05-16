using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour
{

    [SerializeField] private ParticleSystem step;
    private NavMeshAgent agent;
    private Vector3 startPosition;
    [SerializeField] private float maxDistance;
    [SerializeField] private float stayTime;
    [SerializeField] private float speed;
    [SerializeField] private GameObject _holeRabbit;
    private DayCycle _dayCicle;
    [SerializeField] private float wander_range;
    [SerializeField] private GameObject trapObject;
    [SerializeField] private GameObject escapeObject;
    [SerializeField] private GameObject _armatureRabbit;
  
    public float stayTimer;
    private Animator animator;
    private float dist;
    public bool isStay = true;
    private float timerDay;
    public bool goHome;
    public bool goTrap;
    public bool goEscape;
    public bool move;
    private float idleTime;
    private float pathDistance;
    Vector3 escapePosition;
    private float timerSteps;
    // Start is called before the first frame update


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _dayCicle = GameObject.Find("DayCycle").GetComponent<DayCycle>();
    }
    void Start()
    {
        stayTimer = float.MaxValue;
        startPosition = transform.position;
        agent.destination = NewPositionForAgent();
        agent.speed = speed;
        animator.SetBool("Run", true);
        goHome = false;
        goTrap = false;
        goEscape = false;
        move = false;
        timerSteps = 0.25f;

    }

    // Update is called once per frame
    void Update()
    {
        timerDay = _dayCicle.TimeDay();
        if (timerDay <= 0.2f || timerDay >= 0.42f)
        {
            goHome = true;
        }
        else
        {
       //     move = false;
            goHome = false;
        }

        timerSteps -= Time.deltaTime;
        if (timerSteps <= 0 && move == true)
        {
            timerSteps = 0.25f;
            step.Play();
        }


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, wander_range);
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.transform.tag == "Trap")
                {
                    goTrap = true;
                    trapObject = hitCollider.gameObject;
                }

                if (hitCollider.transform.tag == "Player")
                {
                    goEscape = true;
                    escapeObject = hitCollider.gameObject;
                    escapePosition = escapeObject.transform.position;
                    EscapePositionForAgent();
                }
            }
        }

        if (escapeObject != null)
        {
            dist = Vector3.Distance(escapeObject.transform.position, transform.position);
        }

        if (dist >= 50)
        {
            if (goEscape == true)
            {
                agent.speed = 0; ;
                goEscape = false;
                escapeObject = null;
                isStay = true;
                Start();
            }

        }


        if (goHome == true && move == false && goEscape == false)
        {
            StopAllCoroutines();
            idleTime = 5;
            Move(_holeRabbit);
        }

        if (goTrap == true && move == false && goHome == false && goEscape == false)
        {
            idleTime = 5;
            Move(trapObject);
        }
        if (goEscape == true)
        {
            if (escapeObject)
            {
                move = true;
                StopAllCoroutines();
                Vector3 newESCPosition = EscapePositionForAgent();
            //    Vector3 direction = newESCPosition.normalized;
           //     agent.SetDestination(transform.position + direction * 50);
                agent.SetDestination(newESCPosition);
                agent.speed = 10;
                animator.SetBool("Run", true);
            }
        }





        if (goHome == false && goEscape == false && goTrap == false)
        {
            pathDistance = agent.remainingDistance;
            if (pathDistance <= 1)
            {
                move = false;
                agent.speed = 0;
                animator.SetBool("Run", false);
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
                move = true;
                agent.speed = speed;
                animator.SetBool("Run", true);
                stayTimer = float.MaxValue;
                isStay = true;

            }
        }

    }




    private Vector3 NewPositionForAgent()
    {
        return new Vector3(startPosition.x + Random.Range(-maxDistance, maxDistance), startPosition.y, startPosition.z + Random.Range(-maxDistance, maxDistance));
    }


    private void Move(GameObject target)
    {
        move = true;
        animator.SetBool("Run", true);
        agent.speed = 8;
        agent.SetDestination(target.transform.position);

    }

    private Vector3 EscapePositionForAgent()
    {

        return new Vector3((transform.position.x - escapePosition.x),
                                         transform.position.y,
                                         (transform.position.z - escapePosition.z));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("HoleRabbit"))
        {
            
            //    step.Stop();
            _armatureRabbit.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("HoleRabbit"))
        {

            _armatureRabbit.SetActive(true);
        }
    }

    void OnDrawGizmosSelected()             // рисует рассатояние радиуса поражения обьектов.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, wander_range);
    }

}
