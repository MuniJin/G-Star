using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3 : MonoBehaviour
{
    public float detectionRange = 100f;  // 플레이어를 감지하는 범위
    public float attackRange = 110f;    // 플레이어를 공격하는 범위
    public float attackCooldown = 0.5f;   // 공격 쿨다운
    public float maxHeightDifference = 3f; // 플레이어와 적 캐릭터 사이의 최대 높이 차이

    public Transform player;            // 플레이어의 위치
    private NavMeshAgent agent;         // NavMesh 에이전트
    private float lastAttackTime;       // 마지막으로 공격한 시간

    private Enemy_Character ec;

    private Rigidbody rb; // Rigidbody
    private float stuckTimer = -1; // 갇힘 타이머
    private Vector3 lastPosition; // 이전 위치
    private int worldLayerMask; // 월드 레이어 마스크
    private bool wiggleWaypointExixts; // 흔들림 지점 존재 여부
    private List<Vector3> waypoints = new List<Vector3>(); // 경유지 지점 리스트

    private bool isSkillReady = false; // 스킬 사용 준비 상태
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        agent = GetComponent<NavMeshAgent>();  // NavMesh 에이전트 설정
        ec = this.GetComponent<Enemy_Character>();  // Enemy_Character 스크립트 참조
        agent.enabled = true;
        // 에이전트 속도를 플레이어 캐릭터의 속도로 설정
        // agent.speed = ec.speed;

        //player = GameObject.FindGameObjectWithTag("Player").transform;  // 태그로 플레이어 찾기
    }

    void Update()
    {
        // 탐지 범위 내에서 플레이어를 감지하고 있는지 확인
        if (IsPlayerVisible())
        {
            agent.enabled = true;
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
                UseSkillAfterDelay();
            }

            float heightDifference = player.position.y - transform.position.y;

            //만약 플레이어가 적 캐릭터보다 maxHeightDifference 이상 높이에 있으면
            if (heightDifference > maxHeightDifference)
            {
                ec.EJump();
            }
            if (IsPlayerVisible() == false)
            {
                Debug.Log("타냐?");
                //플레이어가 보이지 않거나 범위를 벗어나면 다른 동작 수행(예: 순찰)
                PatrollingBehavior();
            }
            //ec.EUseSkill();
        }
    }

    // 플레이어가 보이고 5초 후에 스킬을 사용하는 함수
    IEnumerator UseSkillAfterDelay()
    {
        isSkillReady = true; // 스킬 사용 준비됨
        yield return new WaitForSeconds(5f); // 5초 대기

        // 5초가 지난 후에 플레이어가 보일 경우 스킬 사용
        if (IsPlayerVisible())
        {
            PerformSkill(); // 스킬 사용하는 함수
        }

        isSkillReady = false; // 스킬 사용이 끝남
    }

    // 스킬을 사용하는 함수
    void PerformSkill()
    {
        //if (Input.GetKeyDown(KeyCode.V))
            // 스킬을 사용하는 코드를 여기에 추가
            ec.EUseSkill(); // Enemy_Character 스크립트의 스킬 사용 함수 호출
    }


    // 플레이어가 보이는지 확인하는 함수
    bool IsPlayerVisible()
    {
        //RaycastHit hit;
        //Vector3 direction = player.position - this.GetComponent<Enemy_Character>().bulPos.transform.position;
        //플레이어와 적 사이에 장애물이 없는지 확인
        //if (player != null)
        //{
        //    Debug.DrawRay(transform.position, direction, Color.red);
        //    if (Physics.Raycast(this.GetComponent<Enemy_Character>().bulPos.transform.position, direction, out hit, detectionRange))
        //    {
        //        Debug.Log(hit.transform.CompareTag("Player"));
        //        return hit.transform.CompareTag("Player"); // 플레이어 태그가 있는지 확인하여 반환
        //        return hit.transform == player;
        //    }
        //}

        //return false; // 플레이어가 보이지 않음
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Player"))
            {
                return true;
            }
        }
        DrawDetectionRange();
        return false;
    }

    void DrawDetectionRange()
    {
        // Visualize the detection range using Debug.DrawLine
        Vector3 center = transform.position;
        float radius = detectionRange;

        DrawCircle(center, radius, Color.green);
    }

    void DrawCircle(Vector3 center, float radius, Color color)
    {
        int segments = 36;
        float angleIncrement = 360f / segments;
        Vector3 previousPoint = Vector3.zero;

        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleIncrement;
            Vector3 currentPoint = new Vector3(center.x + Mathf.Cos(angle) * radius, center.y, center.z + Mathf.Sin(angle) * radius);

            if (i > 0)
            {
                Debug.DrawLine(previousPoint, currentPoint, color);
            }

            previousPoint = currentPoint;
        }
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
        if (agent.enabled)
        {
            //agent.isStopped = true; // Stop the NavMeshAgent if it's enabled
            //agent.velocity = Vector3.zero;
        }

        var distanceTravelled = Vector3.Distance(lastPosition, transform.position);
        lastPosition = transform.position;

        // Check if the enemy is stuck
        if (distanceTravelled < 0.1f)
        {
            if (stuckTimer < 0) stuckTimer = Time.time;

            if (Time.time > stuckTimer + 1)
            {
                // Generate a random point to move
                Vector3 randomDirection = Random.insideUnitSphere * 4;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, 4, NavMesh.AllAreas);

                // Check for obstacles at the generated position
                if (!NavMesh.Raycast(transform.position, hit.position, out NavMeshHit hitInfo, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position); // Set the agent's destination to the new position
                    stuckTimer = -1;
                }
            }
        }
        else
        {
            if (!agent.enabled)
            {
                agent.enabled = true; // Enable the NavMeshAgent
                agent.isStopped = false;
            }
            // Perform other actions if the player is detected
        }
    }
}
