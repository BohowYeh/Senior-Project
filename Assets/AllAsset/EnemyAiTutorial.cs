using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public AudioSource source;
    public AudioClip swing;
    public AudioClip damege;

    public NavMeshAgent agent;
    public float maxHealth=100,currentHealth;

    public Transform player;
    Animator animator;
    public LayerMask whatIsGround, whatIsPlayer;

 

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
   public  bool alreadyAttacked=false;
    public GameObject projectile;
    public GameObject sword;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        deactiveSword();
        currentHealth = maxHealth;
        player = GameObject.Find("PlayerModel").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
     
           agent.SetDestination(transform.position);
           transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            ///animator.SetBool("IsAttack", true);
            alreadyAttacked = true;
            activeSword();
            animator.SetBool("IsAttack", true);
            source.PlayOneShot(swing);

            /*Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);*/

            ///End of attack code




            Invoke(nameof(ResetAttack), timeBetweenAttacks);
          
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        deactiveSword();

        animator.SetBool("IsAttack", false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            animator.SetBool("Die", true);
            Invoke(nameof(DestroyEnemy), 3f);
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private void activeSword()
    {
        sword.GetComponent<Collider>().enabled = true;
    }
    private void deactiveSword()
    {
        sword.GetComponent<Collider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "weapon")
        {
            TakeDamage(50);
            source.PlayOneShot(damege);
        }
    }


}
