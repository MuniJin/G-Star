using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI2 : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] Transform Target;

    //적을 활성화시키기 위한 표적과 적 사이의 거리
    [SerializeField] float Range = 10f;
    float distanceToTarget = Mathf.Infinity;

    [SerializeField] float turningSpeed = 5f;

    //적의 활성화 여부
    public bool isProvoked = false;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        //적이 아직 살아있는지 확인하기
        //그렇지 않으면 즉시 반환
        //if(GetComponent<Health>())
        ////if (currentHealth <= 0f)
        //    if (dead) { return; }

        //목표물에서 적까지의 거리를 할당합니다
        distanceToTarget = Vector3.Distance(Target.position, transform.position);
        if (isProvoked)
        {
            //calling the movement function if activated
            EngageTarget();
        }
        //if not yet activated, compare their distance,
        //whether the target have reached the range.
        else if (distanceToTarget <= Range)
        {
            //if the target entered the range then activate
            isProvoked = true;
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
    private void lookTarget()
    {
        //calculate new direction vector from target's position to enemies position
        Vector3 direction = (Target.position - transform.position).normalized;

        //make new qauternion with the new direction vector we calculated to assign that to the enemie's rotation
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //assign the created quaternion to the enemy
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);
    }
}
