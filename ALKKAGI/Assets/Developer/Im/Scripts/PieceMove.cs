using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    //기물을 실제로 이동시키는 스크립트

    public GameObject DragObj; //드래그되는 오브젝트(Trigger 오브젝트)
    private GameObject GM; //알까기 매니저
    private Rigidbody rb; // 해당 오브젝트의 리지드바디
    public Vector3 Arrow; //이동방향
    private float MoveSpeed; //이동속도
    private Vector3 SaveSpeed; //저장된 속도
    private bool IsCrash; //충돌체크
    private Vector3 dir; //충돌방향
    float totalSpeed; //충돌속도
    float dieTime;

    private void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
        rb = this.gameObject.GetComponent<Rigidbody>(); //오브젝트의 리지드바디를 자동으로 넣어주기
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
    public void RotationReset() //기울기 초기화
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveStart() //기물 이동
    {
        IsCrash = false;
        MoveSpeed = DragObj.GetComponent<AlggagiDrag>().ShootPower;
        rb.AddForce(Arrow * MoveSpeed, ForceMode.Impulse);
        Invoke("NotCrash", 1f); //매니저로 뺴야함
    }
    private void NotCrash() //헛스윙 체크
    {
        if (!IsCrash)
        {
            GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
            GM.GetComponent<AlKKAGIManager>().CrashObjR = null;
            if (!GM.GetComponent<AlKKAGIManager>().blueT)
                StartCoroutine(GM.GetComponent<AlKKAGIManager>().BlueTurn());
        }
        else
        {
            Debug.Log("충돌!");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BluePiece" && this.gameObject.tag == "RedPiece" && GM.GetComponent<AlKKAGIManager>().CrashObjB != collision.gameObject
            && GM.GetComponent<AlKKAGIManager>().IsMyTurn)
        {
            GameObject collidedObject = collision.gameObject;
            GM.GetComponent<AlKKAGIManager>().CrashObjR = this.gameObject;
            GM.GetComponent<AlKKAGIManager>().CrashObjB = collidedObject;
            IsCrash = true;
            if (GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<BlueMovement>().redTurnCrash == false)
            {
                GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<BlueMovement>().redTurnCrash = true;

                SaveSpeed = rb.velocity;
                totalSpeed = SaveSpeed.magnitude/2;
                dir = this.gameObject.transform.localPosition - collidedObject.transform.localPosition;

                Debug.Log("totals - red : " + totalSpeed);
                if (totalSpeed < 1f)
                {
                    Debug.Log("제발 R");
                    totalSpeed = 20f;
                }
                rb.isKinematic = true;
                GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().velocity = Vector3.zero;

                GM.GetComponent<AlKKAGIManager>().Crash();
            }
        }
    }

    public void Win() //FPS 승리시
    {
        rb.isKinematic = false;
        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(-dir * totalSpeed * 0.7f, ForceMode.Impulse);
        rb.AddForce(dir * totalSpeed * 0.4f, ForceMode.Impulse);
    }

    public void lose() //FPS 패배시
    {
        rb.isKinematic = false;
        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(-dir * totalSpeed * 0.4f, ForceMode.Impulse);
        rb.AddForce(dir * totalSpeed * 0.7f, ForceMode.Impulse);
    }
}