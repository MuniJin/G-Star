using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    // ���� ��ֹ��� �����ϴ� ������ �����մϴ�.
    private int range;
    private float speed;
    private bool isThereAnyThing = false;

    // ���� ������ ����� �����մϴ�.
    public GameObject target;
    private float rotationSpeed;
    private RaycastHit hit;

    // �ʱ�ȭ �Լ�
    void Start()
    {
        range = 80;
        speed = 10f;
        rotationSpeed = 15f;
    }

    // �����Ӹ��� ȣ��Ǵ� ������Ʈ �Լ�
    void Update()
    {
        // �ƹ� �͵� �տ� ���ٸ� ����� ���� �ε巴�� ȸ���մϴ�.
        if (!isThereAnyThing)
        {
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }

        // ���� ������ŵ�ϴ�.
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // �տ� ��ֹ��� �ִ��� Ȯ���մϴ�.
        // ������Ʈ ���ʿ� �� ���� ���̸� ��� ��ֹ��� �����մϴ�.
        Transform leftRay = transform;
        Transform rightRay = transform;

        // Physics.Raycast�� ����Ͽ� ��ֹ��� �����մϴ�.
        if (Physics.Raycast(leftRay.position + (transform.right * 7), transform.forward, out hit, range) ||
            Physics.Raycast(rightRay.position - (transform.right * 7), transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                isThereAnyThing = true;
                transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
            }
        }

        // ���� ������Ʈ �� �κп� �� ���� RayCast�� �� ���� ������Ʈ�� �̹� ��ֹ��� ����ߴ����� �����մϴ�.
        // �� ������ false�� �����Ͽ� �տ� �ƹ��͵� ������ �ǹ��մϴ�.
        if (Physics.Raycast(transform.position - (transform.forward * 4), transform.right, out hit, 10) ||
            Physics.Raycast(transform.position - (transform.forward * 4), -transform.right, out hit, 10))
        {
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                isThereAnyThing = false;
            }
        }

        // Physics.RayCast�� ������ϱ� ���� ���̸� �ð�ȭ�մϴ�.
        Debug.DrawRay(transform.position + (transform.right * 7), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.right * 7), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.forward * 4), -transform.right * 20, Color.yellow);
        Debug.DrawRay(transform.position - (transform.forward * 4), transform.right * 20, Color.yellow);
    }
}
