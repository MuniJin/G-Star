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
    float dieTime;

    public bool isdead;

    private void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
        rb = this.gameObject.GetComponent<Rigidbody>(); //������Ʈ�� ������ٵ� �ڵ����� �־��ֱ�
    }

    private void Update()
    {
        if (this.gameObject.transform.position.y < -5)
        {
            dieTime += Time.deltaTime;
            if (dieTime > 3f)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void RotationReset() //���� �ʱ�ȭ
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveStart() //�⹰ �̵�
    {
        GM.GetComponent<AlKKAGIManager>().RedCrash = false;

        StartCoroutine(GM.GetComponent<AlKKAGIManager>().SoundPlay(1));
        MoveSpeed = DragObj.GetComponent<AlggagiDrag>().ShootPower;
        rb.AddForce(Arrow * MoveSpeed, ForceMode.Impulse);
        StartCoroutine(GM.GetComponent<AlKKAGIManager>().NotCrash());
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

            if (GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<BlueMovement>().redTurnCrash == false)
            {
                GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<BlueMovement>().redTurnCrash = true;

                SaveSpeed = rb.velocity;
                totalSpeed = SaveSpeed.magnitude / 2;
                dir = this.gameObject.transform.localPosition - collidedObject.transform.localPosition;

                //Debug.Log("totals - red : " + totalSpeed);
                //if (totalSpeed < 1f)
                //{
                //    Debug.Log("���� R");
                //    totalSpeed = 20f;
                //}

                rb.isKinematic = true;
                GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().velocity = Vector3.zero;
                StartCoroutine(GM.GetComponent<AlKKAGIManager>().Crash());
            }
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