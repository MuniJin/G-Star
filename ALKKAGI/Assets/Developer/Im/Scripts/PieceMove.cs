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
    private bool RotateZero = false; //�⹰�� ���Ⱑ 0,0,0�� �´��� Ȯ���ϴ� ����


    private void Start()
    {
        GM = GameObject.Find("GameManager");
        RotateZero = true; // ó���� 0,0,0�� �⺻���̱⿡ true
        rb = this.gameObject.GetComponent<Rigidbody>(); //������Ʈ�� ������ٵ� �ڵ����� �־��ֱ�
    }

    private void RotationReset() //���� �ʱ�ȭ
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveStart() //�⹰ �̵�
    {
        GM.GetComponent<GameManager>().IsMyTurn = false;
        MoveSpeed = DragObj.GetComponent<AlggagiDrag>().ShootPower;
        rb.AddForce(Arrow * MoveSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BluePiece" && this.gameObject.tag == "RedPiece" && GM.GetComponent<GameManager>().CrashObjB != collision.gameObject)
        {
            GameObject collidedObject = collision.gameObject;

            GM.GetComponent<GameManager>().CrashObjR = this.gameObject;
            GM.GetComponent<GameManager>().CrashObjB = collidedObject;
            GM.GetComponent<GameManager>().RedPieceLocal = this.gameObject.transform.position;
            GM.GetComponent<GameManager>().BluePieceLocal = collidedObject.transform.position;

            // Save the velocity before setting it to zero
            SaveSpeed = rb.velocity;

            // Set the velocity of both objects to zero
            rb.velocity = Vector3.zero;
            GM.GetComponent<GameManager>().CrashObjB.GetComponent<Rigidbody>().velocity = Vector3.zero;

            RotationReset();

            GM.GetComponent<GameManager>().FPSResult();
        }
    }



    public void Win() //FPS�¸���
    {
        GM.GetComponent<GameManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.8f);
        rb.AddForce(-SaveSpeed * 0.2f);
        SaveSpeed = new Vector3(0, 0, 0);
    }

    public void lose()
    {
        GM.GetComponent<GameManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.2f);
        rb.AddForce(-SaveSpeed * 0.8f);
        SaveSpeed = new Vector3(0, 0, 0);
    }
}