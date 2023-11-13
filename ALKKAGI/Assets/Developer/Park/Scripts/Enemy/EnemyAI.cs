using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    // 적이 장애물을 감지하는 범위를 설정합니다.
    private int range;
    private float speed;
    private bool isThereAnyThing = false;
    private float rayhit = 1f;

    // 적이 추적할 대상을 지정합니다.
    public GameObject target;
    private float rotationSpeed;
    private RaycastHit hit;

    public string parentPlayer;

    // 초기화 함수
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        range = 80;
        speed = 10f;
        rotationSpeed = 15f;

        if (this.gameObject.transform.parent.tag == "Player")
            target = GameObject.FindWithTag("Enemy").gameObject;

        if (this.gameObject.transform.parent.tag == "Enemy")
            target = GameObject.FindWithTag("Player").gameObject;

        this.transform.parent = null;
    }

    // 프레임마다 호출되는 업데이트 함수
    void Update()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
        // 아무 것도 앞에 없다면 대상을 향해 부드럽게 회전합니다.
        if (!isThereAnyThing)
        {
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }

        // 적을 전진시킵니다.
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // 앞에 장애물이 있는지 확인합니다.
        // 오브젝트 양쪽에 두 개의 레이를 쏘아 장애물을 감지합니다.
        Transform leftRay = transform;
        Transform rightRay = transform;

        // Physics.Raycast를 사용하여 장애물을 감지합니다.
        if (rayhit <= 1)
        {
            if (Physics.Raycast(leftRay.position + (transform.right * 7), transform.forward, out hit, range) ||
            Physics.Raycast(rightRay.position - (transform.right * 7), transform.forward, out hit, range))
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    isThereAnyThing = true;
                    transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                }
            }
        }

            

        // 이제 오브젝트 끝 부분에 두 개의 RayCast를 더 쏴서 오브젝트가 이미 장애물을 통과했는지를 감지합니다.
        // 이 변수를 false로 설정하여 앞에 아무것도 없음을 의미합니다.
        if (Physics.Raycast(transform.position - (transform.forward * 4), transform.right, out hit, 10) ||
            Physics.Raycast(transform.position - (transform.forward * 4), -transform.right, out hit, 10))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                isThereAnyThing = false;
            }
        }

        // Physics.RayCast를 디버깅하기 위해 레이를 시각화합니다.
        Debug.DrawRay(transform.position + (transform.right * 7), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.right * 7), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.forward * 4), -transform.right * 20, Color.yellow);
        Debug.DrawRay(transform.position - (transform.forward * 4), transform.right * 20, Color.yellow);
    }
}
