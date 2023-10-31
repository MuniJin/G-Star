using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3 : Default_Character
{
    public float detectionRange = 10f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;

    public GameObject projectilePrefab;
    public Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if player is within detection range and there are no obstacles
        if (IsPlayerVisible())
        {
            // Chase the player
            ChasePlayer();

            // Rotate towards the player
            RotateTowardsPlayer();

            // Check attack range and cooldown
            if (IsPlayerWithinAttackRange() && Time.time - lastAttackTime > attackCooldown)
            {
                // Attack the player
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // Player is not visible or out of range, perform other actions (e.g., patrol)
            PatrollingBehavior();
        }
    }

    bool IsPlayerVisible()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        // Check if there are no obstacles between enemy and player
        if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
        {
            return hit.transform.CompareTag("Player");
        }

        return false;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    bool IsPlayerWithinAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    void AttackPlayer()
    {
        // Perform long-range attack, e.g., shoot projectile
        // Your attack logic goes here
    }

    void PatrollingBehavior()
    {
        // Perform patrolling or other behavior when player is not visible
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    protected override void Jump()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack(Vector3 fp, float shootPower)
    {
        // 발사할 프로젝타일 생성
        GameObject projectile = Instantiate(projectilePrefab, fp, Quaternion.identity);
        // 프로젝타일 발사
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 dir = -(fp - player.transform.position).normalized;
        rb.AddForce(dir * shootPower, ForceMode.Impulse);
    }

    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}