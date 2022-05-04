using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitNav : MonoBehaviour
{
    [SerializeField] private ParticleSystem step;
    private NavMeshAgent agent;
    [SerializeField] private GameObject _prefabTarget, _prefabEscape;
    private GameObject _prefabTargetTransform;
    [SerializeField] private GameObject _holeRabbit;
    [SerializeField] private float wander_range;
    [SerializeField] private GameObject trapObject;
    [SerializeField] private GameObject escapeObject;
    [SerializeField] private GameObject bufferPrefab;
    [SerializeField] private GameObject _armatureRabbit;
    [SerializeField] private DayCycle _dayCicle;
   
    private  float timerDay;
    public bool goHome;
    public bool move;
    public bool goTrap;
    public bool goEscape;
    private float idleTime;
    private float dist;
    private float timerSteps;


    private void Awake()
    {
   
        _dayCicle = GameObject.Find("DayCycle").GetComponent<DayCycle>();       
        agent = GetComponent<NavMeshAgent>();       
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWait());
        goHome = false;
        move = false;
        goTrap = false;
        goEscape = false;
        timerSteps = 0.25f;
        
    }



    private void Update()
    {
        timerDay = _dayCicle.TimeDay();
        timerSteps -= Time.deltaTime;
        if(timerSteps <= 0 && move == true)
        {
            timerSteps = 0.25f;
            step.Play();
        }
        
        if(timerDay <= 0.2f  || timerDay >= 0.42f)
        {
            goHome = true;
        }
        else
        {
            goHome = false;
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
                    
                }


            }
        }


        if (escapeObject != null)
        {
            dist = Vector3.Distance(escapeObject.transform.position, transform.position);
        }

        if(dist >= 50 )
        {
            if (goEscape == true)
            {
                goEscape = false;
                escapeObject = null;
                idleTime = 5;
                SpawnTargetPrefab();
            }

        }
        
    }


    private void FixedUpdate()
    {
        if (_prefabTargetTransform && move == false && goHome == false && goTrap == false && goEscape == false)
        {
      
            move = true;
            idleTime = 5;
            Move(_prefabTargetTransform);
        }
        if (goHome == true && move == false && goEscape == false)
        {
            move = true;
            Move(_holeRabbit);
        }
        if(goTrap == true && move == false && goHome == false && goEscape == false)
        {
            move = true;
            Move(trapObject);
        }
        if(goEscape == true && move == false)
        {
            move = true;
            idleTime = 2f;
            
            MoveEscape(escapeObject.transform.position);
        }
    }

    public void MoveEscape(Vector3 escapePosition)
    {
        StopAllCoroutines();
       Vector3   escapePositions = new Vector3((transform.position.x - escapePosition.x) + transform.position.x,
                                      transform.position.y,
                                      (transform.position.z - escapePosition.z) + transform.position.z);
                 
        GameObject buffer = Instantiate(bufferPrefab, escapePositions, Quaternion.identity);        
        agent.SetDestination(buffer.transform.InverseTransformPoint(transform.position));

      
    }

    public void Move(GameObject target)
    {
        
        agent.SetDestination(target.transform.position);
    }

    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(idleTime);
        Destroy(_prefabTargetTransform);
        move = false;
        SpawnTargetPrefab();
    }

    public void SpawnTargetPrefab()
    {
        _prefabTargetTransform = Instantiate(_prefabTarget, new Vector3(transform.position.x + (Random.Range(-5f, 5f)), transform.position.y, transform.position.z + (Random.Range(-5f, 5f))), Quaternion.identity);
        StartCoroutine(SpawnWait());



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("HoleRabbit"))
        {
            
            
            step.Stop();
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
