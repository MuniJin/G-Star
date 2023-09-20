using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    public GameObject GM;
    private float MoveSpeed; //이동속도
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

    public void MoveStart() //기물 이동
    {
        Invoke("RocateRed", 1f);
    }

    private void MoveMath()
    {
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //이 기물과 상대 기물의 거리값
        MoveSpeed = ((float)Math.Floor(Pita)); //속도값
        Vector3 direction = new Vector3(DisX*100 - this.gameObject.transform.localPosition.x, 0, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;
        this.gameObject.GetComponent<Rigidbody>().AddForce(Arrow * MoveSpeed, ForceMode.Impulse);

        Debug.Log("파랑 움직임");
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
            Debug.Log("상대턴충돌");
         
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
    public void Redlose() //FPS승리시
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
    //        // 충돌한 물체의 위치
    //        Vector3 hitPoint = hitInfo.point;

    //        // 충돌한 물체의 노멀 벡터
    //        Vector3 hitNormal = hitInfo.normal;

    //        // 충돌한 물체의 게임 오브젝트
    //        GameObject hitObject = hitInfo.collider.gameObject;
    //    }
    //    yield return 0;
    //}
}