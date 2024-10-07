
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    private int Timer; 

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private bool ReachedCheckpoint; 

    private void Awake()
    {
        Timer = 0; 
        player = GameObject.Find("PlayerObject").transform;
        agent = GetComponent<NavMeshAgent>();
       
    }
    private void Start()
    {

        walkPoint = new Vector3(-15, 6, -20);
         
    }

    private void Update()
    {
        if (health >= 20)
        {
            health = 20; 
        }
        //regeneration
        health += 0.01f;
        this.gameObject.GetComponent<NavMeshAgent>().speed = health/10; 
        Timer++; 
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            Timer = 0; 
        }
        else if(distanceToWalkPoint.magnitude > 1f && Timer > 400)
        {
            walkPointSet = false; Timer = 0;
        }
            
    }
    private void SearchWalkPoint()
    {
        
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
      
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {



            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    //Comment out before build
    /* private void OnDrawGizmosSelected()
     {
         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(transform.position, attackRange);
         Gizmos.color = Color.yellow;
         Gizmos.DrawWireSphere(transform.position, sightRange);
     }
    */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
            Debug.Log("Your Bullet has hit the Sphere!");
        }
    }
}
