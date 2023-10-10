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
    private bool IsCrash; //�浹üũ
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
        IsCrash = false;
        MoveSpeed = DragObj.GetComponent<AlggagiDrag>().ShootPower;
        rb.AddForce(Arrow * MoveSpeed, ForceMode.Impulse);
        Invoke("NotCrash", 1f); //�Ŵ����� ������
    }
    private void NotCrash() //�꽺�� üũ
    {
        if (!IsCrash)
        {
            GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
            GM.GetComponent<AlKKAGIManager>().CrashObjR = null;
            if (!GM.GetComponent<AlKKAGIManager>().blueT)
                GM.GetComponent<AlKKAGIManager>().BlueTurn();
        }
        else
        {
            Debug.Log("�浹!");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BluePiece" && this.gameObject.tag == "RedPiece" && GM.GetComponent<AlKKAGIManager>().CrashObjB != collision.gameObject 
            && GM.GetComponent<AlKKAGIManager>().IsMyTurn)
        {
            IsCrash = true;
            GameObject collidedObject = collision.gameObject;

            GM.GetComponent<AlKKAGIManager>().CrashObjR = this.gameObject;
            GM.GetComponent<AlKKAGIManager>().CrashObjB = collidedObject;

            SaveSpeed = rb.velocity;
            totalSpeed = SaveSpeed.magnitude;
            rb.velocity = Vector3.zero;
            dir = this.gameObject.transform.localPosition - collidedObject.transform.localPosition;

            GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().velocity = Vector3.zero;
            
            GM.GetComponent<AlKKAGIManager>().Crash();
        }
    }

    public void Win() //FPS �¸���
    {
        Debug.Log(dir);
        Debug.Log(totalSpeed) ;

        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce( -dir*totalSpeed* 0.7f, ForceMode.Impulse);
        rb.AddForce(dir * totalSpeed * 0.4f , ForceMode.Impulse);

        if (!GM.GetComponent<AlKKAGIManager>().blueT)
            GM.GetComponent<AlKKAGIManager>().BlueTurn();
    }

    public void lose() //FPS �й��
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(-dir * totalSpeed * 0.4f, ForceMode.Impulse);
        rb.AddForce(dir * totalSpeed * 0.7f , ForceMode.Impulse);

        if (!GM.GetComponent<AlKKAGIManager>().blueT)
            GM.GetComponent<AlKKAGIManager>().BlueTurn();
    }
}