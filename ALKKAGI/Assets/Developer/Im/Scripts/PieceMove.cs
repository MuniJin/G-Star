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
    private bool RotateZero = false; //�⹰�� ���Ⱑ 0,0,0�� �´��� Ȯ���ϴ� ����


    private void Start()
    {
        GM = GameObject.Find("GameManager");
        RotateZero = true; // ó���� 0,0,0�� �⺻���̱⿡ true
        rb = this.gameObject.GetComponent<Rigidbody>(); //������Ʈ�� ������ٵ� �ڵ����� �־��ֱ�
    }

    //private void RotationReset() //���� �ʱ�ȭ
    //{
    //    this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    //}

    public void MoveStart() //�⹰ �̵�
    {
        GM.GetComponent<GameManager>().IsMyTurn = false;
        MoveSpeed = DragObj.GetComponent<AlggagiDrag>().ShootPower;
        rb.AddForce(Arrow* MoveSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BluePiece" && this.gameObject.tag == "RedPiece")
        {
            GameObject collidedObject = collision.gameObject;

            //rb.velocity = Vector3.zero;
            //RotationReset();
            GM.GetComponent<GameManager>().CrashObjR = this.gameObject;
            GM.GetComponent<GameManager>().CrashObjB = collidedObject;
            GM.GetComponent<GameManager>().RedPieceLocal = this.gameObject.transform.position;
            GM.GetComponent<GameManager>().BluePieceLocal = collidedObject.transform.position;
        }

        if (collision.gameObject.tag == "death")
        {
            Invoke("Destroy", 3f);
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

}
