using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3 : MonoBehaviour
{
    public float detectionRange = 100f;  // �÷��̾ �����ϴ� ����
    public float attackRange = 110f;    // �÷��̾ �����ϴ� ����
    public float attackCooldown = 0.5f;   // ���� ��ٿ�
    public float maxHeightDifference = 3f; // �÷��̾�� �� ĳ���� ������ �ִ� ���� ����

    public Transform player;            // �÷��̾��� ��ġ
    private NavMeshAgent agent;         // NavMesh ������Ʈ
    private float lastAttackTime;       // ���������� ������ �ð�

    private Enemy_Character ec;

    private Rigidbody rb; // Rigidbody
    private float stuckTimer = -1; // ���� Ÿ�̸�
    private Vector3 lastPosition; // ���� ��ġ
    private int worldLayerMask; // ���� ���̾� ����ũ
    private bool wiggleWaypointExixts; // ��鸲 ���� ���� ����
    private List<Vector3> waypoints = new List<Vector3>(); // ������ ���� ����Ʈ

    private bool isSkillReady = false; // ��ų ��� �غ� ����
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        agent = GetComponent<NavMeshAgent>();  // NavMesh ������Ʈ ����
        ec = this.GetComponent<Enemy_Character>();  // Enemy_Character ��ũ��Ʈ ����
        agent.enabled = true;
        // ������Ʈ �ӵ��� �÷��̾� ĳ������ �ӵ��� ����
        // agent.speed = ec.speed;

        //player = GameObject.FindGameObjectWithTag("Player").transform;  // �±׷� �÷��̾� ã��
    }

    void Update()
    {
        // Ž�� ���� ������ �÷��̾ �����ϰ� �ִ��� Ȯ��
        if (IsPlayerVisible())
        {
            agent.enabled = true;
            // �÷��̾ �ѱ�
            ChasePlayer();

            // �÷��̾� ������ ȸ��
            RotateTowardsPlayer();

            // ���� ���� ���� �ְ�, ���� ��ٿ��� �������� Ȯ��
            if (IsPlayerWithinAttackRange() && Time.time - lastAttackTime > attackCooldown)
            {

                // �÷��̾ ����
                AttackPlayer();
                lastAttackTime = Time.time;
                UseSkillAfterDelay();
            }

            float heightDifference = player.position.y - transform.position.y;

            //���� �÷��̾ �� ĳ���ͺ��� maxHeightDifference �̻� ���̿� ������
            if (heightDifference > maxHeightDifference)
            {
                ec.EJump();
            }
            if (IsPlayerVisible() == false)
            {
                Debug.Log("Ÿ��?");
                //�÷��̾ ������ �ʰų� ������ ����� �ٸ� ���� ����(��: ����)
                PatrollingBehavior();
            }
            //ec.EUseSkill();
        }
    }

    // �÷��̾ ���̰� 5�� �Ŀ� ��ų�� ����ϴ� �Լ�
    IEnumerator UseSkillAfterDelay()
    {
        isSkillReady = true; // ��ų ��� �غ��
        yield return new WaitForSeconds(5f); // 5�� ���

        // 5�ʰ� ���� �Ŀ� �÷��̾ ���� ��� ��ų ���
        if (IsPlayerVisible())
        {
            PerformSkill(); // ��ų ����ϴ� �Լ�
        }

        isSkillReady = false; // ��ų ����� ����
    }

    // ��ų�� ����ϴ� �Լ�
    void PerformSkill()
    {
        //if (Input.GetKeyDown(KeyCode.V))
            // ��ų�� ����ϴ� �ڵ带 ���⿡ �߰�
            ec.EUseSkill(); // Enemy_Character ��ũ��Ʈ�� ��ų ��� �Լ� ȣ��
    }


    // �÷��̾ ���̴��� Ȯ���ϴ� �Լ�
    bool IsPlayerVisible()
    {
        //RaycastHit hit;
        //Vector3 direction = player.position - this.GetComponent<Enemy_Character>().bulPos.transform.position;
        //�÷��̾�� �� ���̿� ��ֹ��� ������ Ȯ��
        //if (player != null)
        //{
        //    Debug.DrawRay(transform.position, direction, Color.red);
        //    if (Physics.Raycast(this.GetComponent<Enemy_Character>().bulPos.transform.position, direction, out hit, detectionRange))
        //    {
        //        Debug.Log(hit.transform.CompareTag("Player"));
        //        return hit.transform.CompareTag("Player"); // �÷��̾� �±װ� �ִ��� Ȯ���Ͽ� ��ȯ
        //        return hit.transform == player;
        //    }
        //}

        //return false; // �÷��̾ ������ ����
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

    // �÷��̾ �Ѵ� �Լ�
    void ChasePlayer()
    {
        agent.SetDestination(player.position); // NavMesh ������Ʈ�� �÷��̾� ��ġ�� �̵� ��ǥ ����
    }

    // �÷��̾� ������ ȸ���ϴ� �Լ�
    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // �÷��̾ ���� ���� ���� �ִ��� Ȯ���ϴ� �Լ�
    bool IsPlayerWithinAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    // �÷��̾ �����ϴ� �Լ�
    void AttackPlayer()
    {
        // ���� ���� ����, ���� ���, �߻�ü�� �߻��ϴ� ���� ���� �ൿ�� ����
        // ���⿡ ���� ������ �߰��ϼ���
        ec.EAttack();  // Enemy_Character Ŭ������ ���� �Լ� ȣ��
    }

    // �÷��̾ ������ ���� ���� ������ �����ϴ� �Լ�
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
