using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI2 : Default_Character
{
    private NavMeshAgent navMeshAgent;

    public Transform Target;

    // 적을 활성화시키기 위한 표적과 적 사이의 거리
    [SerializeField] float Range = 20f;
    float distanceToTarget = Mathf.Infinity;

    [SerializeField] float turningSpeed = 5f;

    // 적의 활성화 여부
    public bool isProvoked = false;
    bool dead;

    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;
    public float detectionRange = 20f;
    public float attackRange = 20f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private Transform player;
    private Transform aimTransform; // 에임을 조절할 Transform
    private float lastFireTime;

    // 순찰 관련 변수
    //public Transform[] patrolPoints; // 순찰 지점들을 저장할 배열
    //private int currentPatrolPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        aimTransform = transform; // 에임을 조절할 대상을 자기 자신으로 설정
        navMeshAgent.stoppingDistance = 10f;
    }


    // Update is called once per frame
    void Update()
    {
        //목표물에서 적까지의 거리를 할당합니다
        distanceToTarget = Vector3.Distance(Target.position, transform.position);
        if (isProvoked)
            EngageTarget();
        
        //if not yet activated, compare their distance,
        //whether the target have reached the range.
        else if (distanceToTarget <= Range)
        {
            //if the target entered the range then activate
            isProvoked = true;
        }

        // 플레이어와 적의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, Target.position);

        // 플레이어가 감지 범위 안에 있을 때

        if (distanceToPlayer <= detectionRange)
        {
            // 플레이어 방향으로 회전
            Vector3 targetDirection = Target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // 플레이어를 향해 이동
            this.transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

            // 플레이어와 적의 거리가 공격 범위 안에 있고 공격 쿨다운이 지났을 때
            if (distanceToPlayer <= attackRange && Time.time - lastFireTime >= 1 / fireRate)
            {
                Attack(firePoint.position);
                lastFireTime = Time.time;
            }
        }

    }

    private void EngageTarget()
    {
        //Look at direction of target function
        lookTarget();

        //compare distace to target and stopping distance that assigned in navmesh agent in target.
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            //if enemy haven't reached the stopping condition, move towards target
            navMeshAgent.SetDestination(Target.position);
            //you can implement the animation codes for movement in here
        }
    }

    //Look in direcetion of target
    private void lookTarget()
    {
        //calculate new direction vector from target's position to enemies position
        Vector3 direction = (Target.position - transform.position).normalized;

        //make new qauternion with the new direction vector we calculated to assign that to the enemie's rotation
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //assign the created quaternion to the enemy
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);
    }


    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    protected override void Jump()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack(Vector3 fp)
    {
        // 발사할 프로젝타일 생성
        GameObject projectile = Instantiate(projectilePrefab, fp, Quaternion.identity);
        // 프로젝타일 발사
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 dir = -(fp - Target.transform.position).normalized;
        rb.AddForce(dir * 60f, ForceMode.Impulse);
    }

    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }

    // 점프

    // waypoint
}

