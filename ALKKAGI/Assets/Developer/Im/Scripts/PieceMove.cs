using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    //기물을 실제로 이동시키는 스크립트

    public GameObject DragObj; //드래그되는 오브젝트(Trigger 오브젝트)
    private GameObject GM;
    private Rigidbody rb; // 해당 오브젝트의 리지드바디
    public Vector3 Arrow; //이동방향
    private float MoveSpeed; //이동속도
    private Vector3 SaveSpeed; //저장된 속도
    private bool IsCrash;
    private Vector3 dir;
    float totalSpeed;

    private void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
        rb = this.gameObject.GetComponent<Rigidbody>(); //오브젝트의 리지드바디를 자동으로 넣어주기
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
    private void NotCrash()
    {
        if (!IsCrash)
        {
            Debug.Log("헛스윙");
            GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
            GM.GetComponent<AlKKAGIManager>().CrashObjR = null;
            if (!GM.GetComponent<AlKKAGIManager>().blueT)
                GM.GetComponent<AlKKAGIManager>().BlueTurn();
        }
        else
        {
            Debug.Log("충돌!");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BluePiece" && this.gameObject.tag == "RedPiece" && GM.GetComponent<AlKKAGIManager>().CrashObjB != collision.gameObject && GM.GetComponent<AlKKAGIManager>().IsMyTurn)
        {
            Debug.Log("내턴충돌");
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

    public void Win() //FPS승리시
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce( -dir*totalSpeed* 0.7f, ForceMode.Impulse);
        rb.AddForce(dir * totalSpeed * 0.4f, ForceMode.Impulse);

        GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
        GM.GetComponent<AlKKAGIManager>().CrashObjR = null;

        if (!GM.GetComponent<AlKKAGIManager>().blueT)
            GM.GetComponent<AlKKAGIManager>().BlueTurn();
    }

    public void lose()
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjB.GetComponent<Rigidbody>().AddForce(-dir * totalSpeed * 0.4f, ForceMode.Impulse);
        rb.AddForce(dir * totalSpeed * 0.7f, ForceMode.Impulse);

        GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
        GM.GetComponent<AlKKAGIManager>().CrashObjR = null;

        if (!GM.GetComponent<AlKKAGIManager>().blueT)
            GM.GetComponent<AlKKAGIManager>().BlueTurn();
    }
}