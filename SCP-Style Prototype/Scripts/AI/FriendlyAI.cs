
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsEnemy; 

    //Patroling
    public Vector3 walkPoint;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, enemyInSightRange;

    private void Awake()
    {
        player = GameObject.Find("WispLocation").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy); 

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (enemyInSightRange) AlertPlayer();
        if (!enemyInSightRange)
        {
           // this.gameObject.GetComponent<ParticleSystem>().startColor = Color.cyan;
        }
    }
    private void AlertPlayer()
    {
       // this.gameObject.GetComponent<ParticleSystem>().startColor = Color.red;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    //Comment out before build
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    */
}
