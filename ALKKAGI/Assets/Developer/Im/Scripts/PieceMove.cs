

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    //�⹰�� ������ �̵���Ű�� ��ũ��Ʈ

    public GameObject DragObj; //�巡�׵Ǵ� ������Ʈ(Trigger ������Ʈ)
    private GameObject GM; //�˱�� �Ŵ���
    private Rigidbody rb; // �ش� ������Ʈ�� ������ٵ�
    public Vector3 Arrow; //�̵�����
    private float MoveSpeed; //�̵��ӵ�
    private Vector3 SaveSpeed; //����� �ӵ�
    private Vector3 dir; //�浹����
    float totalSpeed; //�浹�ӵ�

    private void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
        rb = this.gameObject.GetComponent<Rigidbody>(); //������Ʈ�� ������ٵ� �ڵ����� �־��ֱ�
    }

    public void RotationReset() //���� �ʱ�ȭ
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveStart() //�⹰ �̵�
    {
        GM.GetComponent<AlKKAGIManager>().RedCrash = false;
        MoveSpeed = DragObj.GetComponent<AlggagiDrag>().ShootPower;
        rb.AddForce(Arrow * MoveSpeed, ForceMode.Impulse);
        Invoke("NotCrash", 1f); //�Ŵ����� ������
    }
    private void NotCrash() //�꽺�� üũ
    {
        if (!GM.GetComponent<AlKKAGIManager>().RedCrash)
        {
            GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
            GM.GetComponent<AlKKAGIManager>().CrashObjR = null;
            if (!GM.GetComponent<AlKKAGIManager>().blueT)
                GM.GetComponent<AlKKAGIManager>().BlueStart();
        }
        else
        {
            //Debug.Log("�浹!");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BluePiece" && this.gameObject.tag == "RedPiece" && GM.GetComponent<AlKKAGIManager>().CrashObjB != collision.gameObject
            && GM.GetComponent<AlKKAGIManager>().IsMyTurn)
        {
            GM.GetComponent<AlKKAGIManager>().RedCrash = true;
            GameObject collidedObject = collision.gameObject;

            GM.GetComponent<AlKKAGIManager>().CrashObjR = this.gameObject;
            GM.GetComponent<AlKKAGIManager>().CrashObjB = collidedObject;

            SaveSpeed = rb.velocity;
            totalSpeed = SaveSpeed.magnitude;
            dir = this.gameObject.transform.localPosition - collidedObject.transform.localPosition;

            if (totalSpeed < 1f)
            {
                Debug.Log("���� R");
                totalSpeed = 20f;
            }
            rb.isKinematic = true;
            GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().velocity = Vector3.zero;

            GM.GetComponent<AlKKAGIManager>().Crash();
        }
    }

    public void Win() //FPS �¸���
    {
        rb.isKinematic = false;
        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(-dir * totalSpeed * 0.7f, ForceMode.Impulse);
        rb.AddForce(dir * totalSpeed * 0.4f, ForceMode.Impulse);
    }

    public void lose() //FPS �й��
    {
        rb.isKinematic = false;
        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(-dir * totalSpeed * 0.4f, ForceMode.Impulse);
        rb.AddForce(dir * totalSpeed * 0.7f, ForceMode.Impulse);
    }
}

