using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3 : MonoBehaviour
{
    public float detectionRange = 100f;  // �÷��̾ �����ϴ� ����
    public float attackRange = 100f;    // �÷��̾ �����ϴ� ����
    public float attackCooldown = 2f;   // ���� ��ٿ�

    public Transform player;            // �÷��̾��� ��ġ
    private NavMeshAgent agent;         // NavMesh ������Ʈ
    private float lastAttackTime;       // ���������� ������ �ð�

    private Enemy_Character ec;         

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // NavMesh ������Ʈ ����
        ec = this.GetComponent<Enemy_Character>();  // Enemy_Character ��ũ��Ʈ ����

        // ������Ʈ �ӵ��� �÷��̾� ĳ������ �ӵ��� ����
        // agent.speed = ec.speed;

        //player = GameObject.FindGameObjectWithTag("Player").transform;  // �±׷� �÷��̾� ã��
    }

    void Update()
    {
        // Ž�� ���� ������ �÷��̾ �����ϰ� �ִ��� Ȯ��
        if (IsPlayerVisible())
        {
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
            }
        }
        else
        {
            // �÷��̾ ������ �ʰų� ������ ����� �ٸ� ���� ���� (��: ����)
            PatrollingBehavior();
        }
    }

    // �÷��̾ ���̴��� Ȯ���ϴ� �Լ�
    bool IsPlayerVisible()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        // �÷��̾�� �� ���̿� ��ֹ��� ������ Ȯ��
        if (player != null)
        {
            if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
            {
                //return hit.transform.CompareTag("Player"); // �÷��̾� �±װ� �ִ��� Ȯ���Ͽ� ��ȯ
                return hit.transform == player;
            }
        }

        return false; // �÷��̾ ������ ����
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
        // �÷��̾ ������ ���� ���� ������ �߰��ϼ���
    }
}
