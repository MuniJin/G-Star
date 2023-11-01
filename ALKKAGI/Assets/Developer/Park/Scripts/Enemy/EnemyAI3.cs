using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3 : MonoBehaviour
{
    public float detectionRange = 100f;  // 플레이어를 감지하는 범위
    public float attackRange = 100f;    // 플레이어를 공격하는 범위
    public float attackCooldown = 2f;   // 공격 쿨다운

    public Transform player;            // 플레이어의 위치
    private NavMeshAgent agent;         // NavMesh 에이전트
    private float lastAttackTime;       // 마지막으로 공격한 시간

    private Enemy_Character ec;         

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // NavMesh 에이전트 설정
        ec = this.GetComponent<Enemy_Character>();  // Enemy_Character 스크립트 참조

        // 에이전트 속도를 플레이어 캐릭터의 속도로 설정
        // agent.speed = ec.speed;

        //player = GameObject.FindGameObjectWithTag("Player").transform;  // 태그로 플레이어 찾기
    }

    void Update()
    {
        // 탐지 범위 내에서 플레이어를 감지하고 있는지 확인
        if (IsPlayerVisible())
        {
            // 플레이어를 쫓기
            ChasePlayer();

            // 플레이어 쪽으로 회전
            RotateTowardsPlayer();

            // 공격 범위 내에 있고, 공격 쿨다운이 지났는지 확인
            if (IsPlayerWithinAttackRange() && Time.time - lastAttackTime > attackCooldown)
            {
                // 플레이어를 공격
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // 플레이어가 보이지 않거나 범위를 벗어나면 다른 동작 수행 (예: 순찰)
            PatrollingBehavior();
        }
    }

    // 플레이어가 보이는지 확인하는 함수
    bool IsPlayerVisible()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        // 플레이어와 적 사이에 장애물이 없는지 확인
        if (player != null)
        {
            if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
            {
                //return hit.transform.CompareTag("Player"); // 플레이어 태그가 있는지 확인하여 반환
                return hit.transform == player;
            }
        }

        return false; // 플레이어가 보이지 않음
    }

    // 플레이어를 쫓는 함수
    void ChasePlayer()
    {
        agent.SetDestination(player.position); // NavMesh 에이전트로 플레이어 위치로 이동 목표 설정
    }

    // 플레이어 쪽으로 회전하는 함수
    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // 플레이어가 공격 범위 내에 있는지 확인하는 함수
    bool IsPlayerWithinAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    // 플레이어를 공격하는 함수
    void AttackPlayer()
    {
        // 공격 로직 구현, 예를 들어, 발사체를 발사하는 등의 공격 행동을 수행
        // 여기에 공격 로직을 추가하세요
        ec.EAttack();  // Enemy_Character 클래스의 공격 함수 호출
    }

    // 플레이어가 보이지 않을 때의 동작을 수행하는 함수
    void PatrollingBehavior()
    {
        // 플레이어가 보이지 않을 때의 동작을 추가하세요
    }
}
