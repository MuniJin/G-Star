using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public bool isAttack;
    private Quaternion originVector;
    private Quaternion attackingVector;
    private Vector3 targetPos;
    private Vector3 originPos;
    private void Start()
    {
        isAttack = false;
        originVector = this.transform.rotation;
        attackingVector = new Quaternion(0f, 90f, 90f, 1);
        originPos = this.transform.position;
    }

    private void Update()
    {
        if(isAttack)
        {
            Debug.Log("Start Hammer Attack");
            if (this.GetComponent<MeshCollider>().isTrigger == false)
                this.GetComponent<MeshCollider>().isTrigger = true;

            this.transform.position = Vector3.MoveTowards(originPos, targetPos, 0.1f);
        }
    }

    public void Attack()
    {
        isAttack = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            targetPos = hit.point;
            Debug.Log(hit.point);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            Debug.Log(other.name);
            this.GetComponent<MeshCollider>().isTrigger = false;
        }
    }
}
