using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : MonoBehaviour
{
    [SerializeField] private ParticleSystem step1;
    [SerializeField] private ParticleSystem step2;
    [SerializeField] private ParticleSystem step3;
    [SerializeField] private ParticleSystem step4;
    public  List<GameObject> objects = new List<GameObject>();
    [SerializeField] private float wander_range;
    float timerSearch;
    public bool attack = false;
   public  GameObject target;
   public GameObject FireTarget;
    float dist;
    float distTarget;
    public GameObject testPtefab;
    public bool move;
    float outTime;
    float radiusFire;
    private Fire fire;
    float offsetForceFire;
    public bool fireON;
    public float Damage;
    public bool isDeath;
    private float timerSteps;



    private NavMeshAgent agent;
    private Vector3 startPosition;
    [SerializeField] private float maxDistance;
    [SerializeField] private float stayTime;
    [SerializeField] private float speed;
    private float stayTimer;
    [SerializeField] private Animator animator;
    private bool isStay = true;
    // Start is called before the first frame update
    void Start()
    {
        outTime = 2;
        timerSearch = 0.5f;
        attack = false;
        move = false;
        fireON = false;
        isDeath = false;


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
        timerSteps -= Time.deltaTime;
        if (timerSteps <= 0 && move == true)
        {
            timerSteps = 0.25f;
            step1.Play();
            step2.Play();
            step3.Play();
            step4.Play();
        }



        timerSearch -= Time.deltaTime;

        if(timerSearch <= 0)
        {
            timerSearch = 0.5f;
            SearchTarget();
        }

        if (attack == false && isDeath == false)
        {
            float pathDistance = agent.remainingDistance;
            if (pathDistance <= 0.3f)
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



        if (target != null && fireON == false && isDeath == false)
        {
            
            MoveTarget();
        }
        if(target != null && isDeath == false)
        {
            distTarget = Vector3.Distance(target.transform.position, transform.position);
            
            if(distTarget <= 7  && distTarget > 2)
            {
                animator.SetBool("Attack", false);
                fireON = false;
                FireTarget = null;
            }
            else if(distTarget <= 2)
            {
                
                fireON = false;
                FireTarget = null;
                animator.SetBool("Walk", false);
                animator.SetBool("Attack", true);

                //«ƒесь вписывать дамаг через  корутину ...
            }

        }

        if(isDeath == true) // смерть
        {
            animator.SetBool("Death", true);
        }








        if (FireTarget == null && isDeath == false)
        {
       //     fire.fireHelath = 0;
            fireON = false;
        }

        if (FireTarget != null && fireON == true && isDeath == false)
        {

            dist = Vector3.Distance(FireTarget.transform.position, transform.position);
            print(dist);
            radiusFire = fire.fireHelath;
            if (radiusFire >= 30 && fireON == true)
            {
                if (dist <= 15 && dist > 10)   /// стоит
                {
                    agent.speed = 0;
                    animator.SetBool("Walk", false);
                    agent.SetDestination(transform.position);

                }
                else if (dist <= 10 && move == false)  // отходит
                {
                    move = true;
                    Vector3 newPosition = EscapePositionForAgent();
                    Vector3 direction = newPosition.normalized;
                    agent.speed = 15;
                    animator.SetBool("Walk", true);
                    agent.SetDestination(transform.position + direction * dist);
                    StartCoroutine(MoveFalse());
                }
                else if (dist > 15)   // можно сделать чтобы убегал игрок или все его волк нашел и не отпустит не когда.
                {
                    move = false;
                    FireTarget = null;
                    fireON = false;
                    attack = false;
                }
                else if (dist < 6)  // attack player
                {
                    print("attack");
                    FireTarget = null;
                    fireON = false;

                }
            }
            else if (radiusFire < 30 && fireON == true)
            {
                objects.Clear();
                fireON = false;
                FireTarget = null;
            }
        }

    }


    IEnumerator MoveFalse()
    {
        yield return new WaitForSeconds(2f);
        
            move = false;
        
    }


    private void SearchTarget()
    {

        FireTarget = null;
        
        objects.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, wander_range);
        {
            foreach (Collider hitCollider in hitColliders)
            {

                if (hitCollider.transform.tag == "Player")
                {
                    attack = true;
                    target = hitCollider.gameObject;

                }

                if (hitCollider.transform.tag == "Interection")
                {
                    float shortestDistance = 0;
                    if (!objects.Contains(hitCollider.gameObject))
                    {
                        objects.Add(hitCollider.gameObject);
                    }
                    foreach (GameObject enemy in objects)
                    {
                        float distanceNearbyFire = enemy.GetComponent<Fire>().fireHelath;
                        if (distanceNearbyFire > shortestDistance)
                        {
                            shortestDistance = distanceNearbyFire;
                            FireTarget = enemy.gameObject;
                            fire = FireTarget.GetComponent<Fire>();
                            fireON = true;
                            attack = false;

                        }
                    }
                }

                if (hitCollider.transform.tag == "Trap")
                {
                    
              //      trapObject = hitCollider.gameObject;
                }


            }
        }
    }

    private void MoveTarget()
    {
        agent.speed = speed;
        if (FireTarget == null)
        {
            animator.SetBool("Walk", true);
            agent.SetDestination(target.transform.position);
        }
    }

    private Vector3 EscapePositionForAgent()
    {

        return new Vector3((transform.position.x - FireTarget.transform.position.x),
                                         transform.position.y,
                                         (transform.position.z - FireTarget.transform.position.z));

    }

    private Vector3 NewPositionForAgent()
    {
        return new Vector3(startPosition.x + Random.Range(-maxDistance, maxDistance), startPosition.y, startPosition.z + Random.Range(-maxDistance, maxDistance));
    }

    void OnDrawGizmosSelected()             // рисует рассато€ние радиуса поражени€ обьектов.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, wander_range);
    }
}
