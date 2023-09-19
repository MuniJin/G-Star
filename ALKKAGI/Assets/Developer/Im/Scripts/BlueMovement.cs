using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    public GameObject GM;
    private float MoveSpeed; //�̵��ӵ�
    public float Pita = 0f;
    public float DisX;
    public float DisZ;
    private Vector3 targetlocal;
    private Vector3 Arrow;
    public Vector3 SaveSpeed;

    private void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
    }
    public void BlueMove()
    {
        StartCoroutine("RaySet") ;
    }

    public void MoveStart() //�⹰ �̵�
    {
        Invoke("RocateRed", 2f);
    }

    private void MoveMath()
    {
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�� �⹰�� ��� �⹰�� �Ÿ���
        MoveSpeed = ((float)Math.Floor(Pita)); //�ӵ���
        Vector3 direction = new Vector3(DisX*100 - this.gameObject.transform.localPosition.x, 0, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;
        this.gameObject.GetComponent<Rigidbody>().AddForce(Arrow * MoveSpeed, ForceMode.Impulse);
    }

    private void RocateRed()
    {
        Debug.Log("show");
        GameObject Target = GM.GetComponent<AlKKAGIManager>().LeftRedPiece[UnityEngine.Random.Range(0, 15)];
        if (Target == null)
        {
            RocateRed();
        }
        else
        {
            targetlocal = Target.transform.localPosition;
            DisX = targetlocal.x / 100;
            DisZ = targetlocal.z / 100;
            MoveMath();
        }
    }

    IEnumerable RaySet()
    {
        RaycastHit hitInfo;

        Ray ray = new Ray();
        ray.origin = this.gameObject.transform.localPosition;
        ray.direction = this.transform.forward;

        if (Physics.Raycast(ray, out hitInfo, 30))
        {
            // �浹�� ��ü�� ��ġ
            Vector3 hitPoint = hitInfo.point;

            // �浹�� ��ü�� ��� ����
            Vector3 hitNormal = hitInfo.normal;

            // �浹�� ��ü�� ���� ������Ʈ
            GameObject hitObject = hitInfo.collider.gameObject;
        }
        yield return 0;
    }
}