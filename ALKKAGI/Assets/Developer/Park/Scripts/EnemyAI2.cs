using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI2 : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] Transform Target;

    //���� Ȱ��ȭ��Ű�� ���� ǥ���� �� ������ �Ÿ�
    [SerializeField] float Range = 10f;
    float distanceToTarget = Mathf.Infinity;

    [SerializeField] float turningSpeed = 5f;

    //���� Ȱ��ȭ ����
    public bool isProvoked = false;
    bool dead;

    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;
    public float detectionRange = 10f;
    public float attackRange = 5f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private Transform player;
    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���� ����ִ��� Ȯ���ϱ�
        //�׷��� ������ ��� ��ȯ
        //if(GetComponent<Health>())
        ////if (currentHealth <= 0f)
        //    if (dead) { return; }

        ////��ǥ������ �������� �Ÿ��� �Ҵ��մϴ�
        //distanceToTarget = Vector3.Distance(Target.position, transform.position);
        //if (isProvoked)
        //{
        //    //calling the movement function if activated
        //    EngageTarget();
        //}
        ////if not yet activated, compare their distance,
        ////whether the target have reached the range.
        //else if (distanceToTarget <= Range)
        //{
        //    //if the target entered the range then activate
        //    isProvoked = true;
        //}

        if (player == null)
            return;

        // �÷��̾�� ���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // �÷��̾ ���� ���� �ȿ� ���� ��
        if (distanceToPlayer <= detectionRange)
        {
            // �÷��̾� �������� ȸ��
            Vector3 targetDirection = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // �÷��̾ ���� �̵�
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // �÷��̾�� ���� �Ÿ��� ���� ���� �ȿ� �ְ� ���� ��ٿ��� ������ ��
            if (distanceToPlayer <= attackRange && Time.time - lastFireTime >= 1 / fireRate)
            {
                Attack();
                lastFireTime = Time.time;
            }
        }
    }

    private void Attack()
    {
        // �߻��� ������Ÿ�� ����
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        // ������Ÿ�� �߻�
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = (player.position - firePoint.position).normalized * 10f;
    }
}

