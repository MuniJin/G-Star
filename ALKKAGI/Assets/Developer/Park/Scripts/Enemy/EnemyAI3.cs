using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3 : MonoBehaviour
{
    public float detectionRange = 40f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;

    public Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;

    private Enemy_Character ec;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ec = this.GetComponent<Enemy_Character>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //ec.EJump();

        // 장애물이 없고 감지 범위 안에 있다면
        if (IsPlayerVisible())
        {
            //플레이어 쫓기
            ChasePlayer();

            //플레이어 방향으로 돌기
            RotateTowardsPlayer();

            // 공격범위 체크 쿨다운
            if (IsPlayerWithinAttackRange() && Time.time - lastAttackTime > attackCooldown)
            {
                // Attack the player
                ec.EAttack();
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
        if(player != null)
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
        ec.EAttack();
    }

    void PatrollingBehavior()
    {
        // Perform patrolling or other behavior when player is not visible
    }
}