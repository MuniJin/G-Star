using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    //�⹰�� ������ �̵���Ű�� ��ũ��Ʈ

    public GameObject DragObj; //�巡�׵Ǵ� ������Ʈ(Trigger ������Ʈ)
    public GameObject GM;
    private Rigidbody rb; // �ش� ������Ʈ�� ������ٵ�
    public Vector3 Arrow; //�̵�����
    public float MoveSpeed; //�̵��ӵ�
    public Vector3 SaveSpeed; //����� �ӵ�


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
        GM.GetComponent<AlKKAGIManager>().IsMyTurn = false;
        MoveSpeed = DragObj.GetComponent<AlggagiDrag>().ShootPower;
        rb.AddForce(Arrow * MoveSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BluePiece" && this.gameObject.tag == "RedPiece" && GM.GetComponent<AlKKAGIManager>().CrashObjB != collision.gameObject)
        {
            SaveSpeed += rb.velocity;
            GameObject collidedObject = collision.gameObject;

            GM.GetComponent<AlKKAGIManager>().CrashObjR = this.gameObject;
            GM.GetComponent<AlKKAGIManager>().CrashObjB = collidedObject;

            rb.velocity = Vector3.zero;
            GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().velocity = Vector3.zero;


            GM.GetComponent<AlKKAGIManager>().Crash();
        }
    }



    public void Win() //FPS�¸���
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.8f, ForceMode.Impulse);
        rb.AddForce(-SaveSpeed * 0.4f, ForceMode.Impulse);
    }

    public void lose()
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.4f, ForceMode.Impulse);
        rb.AddForce(-SaveSpeed * 0.8f, ForceMode.Impulse);
    }
}