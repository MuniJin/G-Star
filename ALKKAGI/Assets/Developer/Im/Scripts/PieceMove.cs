using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    //기물을 실제로 이동시키는 스크립트

    public GameObject DragObj; //드래그되는 오브젝트(Trigger 오브젝트)
    public GameObject GM;
    private Rigidbody rb; // 해당 오브젝트의 리지드바디
    public Vector3 Arrow; //이동방향
    public float MoveSpeed; //이동속도
    public Vector3 SaveSpeed; //저장된 속도
    private bool RotateZero = false; //기물의 기울기가 0,0,0이 맞는지 확인하는 변수


    private void Start()
    {
        GM = GameObject.Find("GameManager");
        RotateZero = true; // 처음엔 0,0,0이 기본값이기에 true
        rb = this.gameObject.GetComponent<Rigidbody>(); //오브젝트의 리지드바디를 자동으로 넣어주기
    }

    private void RotationReset() //기울기 초기화
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveStart() //기물 이동
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



    public void Win() //FPS승리시
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