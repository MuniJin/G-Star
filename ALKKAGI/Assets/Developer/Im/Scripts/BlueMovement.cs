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
    private Vector3 dir;
    float totalSpeed;
    private void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
    }

    public void MoveStart() //�⹰ �̵�
    {
        Invoke("RocateRed", 1f);
    }

    private void MoveMath()
    {
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�� �⹰�� ��� �⹰�� �Ÿ���
        MoveSpeed = ((float)Math.Floor(Pita)); //�ӵ���
        Vector3 direction = new Vector3(DisX*100 - this.gameObject.transform.localPosition.x, 0, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;
        this.gameObject.GetComponent<Rigidbody>().AddForce(Arrow * MoveSpeed, ForceMode.Impulse);

        Debug.Log("�Ķ� ������");
    }

    private void RocateRed()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RedPiece" && this.gameObject.tag == "BluePiece" && GM.GetComponent<AlKKAGIManager>().CrashObjR != collision.gameObject && !GM.GetComponent<AlKKAGIManager>().IsMyTurn)
        {
            Debug.Log("������浹");
         
            GameObject collidedObject = collision.gameObject;
            AlKKAGIManager alm = GM.GetComponent<AlKKAGIManager>();
           
            alm.CrashObjR = collidedObject;
            alm.CrashObjB = this.gameObject;


            SaveSpeed = this.gameObject.GetComponent<Rigidbody>().velocity;
            totalSpeed = SaveSpeed.magnitude;
            dir = this.gameObject.transform.localPosition - collidedObject.transform.localPosition;

            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            alm.CrashObjR.GetComponent<Rigidbody>().velocity = Vector3.zero;
            
            alm.Crash();
        }
    }
    public void RedWin()
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.4f, ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(-SaveSpeed * 0.7f, ForceMode.Impulse);
    }
    public void Redlose() //FPS�¸���
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.7f, ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(-SaveSpeed * 0.4f, ForceMode.Impulse);
    }

    //IEnumerable RaySet()
    //{
    //    RaycastHit hitInfo;

    //    Ray ray = new Ray();
    //    ray.origin = this.gameObject.transform.localPosition;
    //    ray.direction = this.transform.forward;

    //    if (Physics.Raycast(ray, out hitInfo, 30))
    //    {
    //        // �浹�� ��ü�� ��ġ
    //        Vector3 hitPoint = hitInfo.point;

    //        // �浹�� ��ü�� ��� ����
    //        Vector3 hitNormal = hitInfo.normal;

    //        // �浹�� ��ü�� ���� ������Ʈ
    //        GameObject hitObject = hitInfo.collider.gameObject;
    //    }
    //    yield return 0;
    //}
}