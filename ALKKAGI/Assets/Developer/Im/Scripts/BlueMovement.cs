using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    private GameObject GM;
    private float MoveSpeed; //이동속도
    private float Pita = 0f;
    private float DisX;
    private float DisZ;
    private Vector3 targetlocal;
    private Vector3 Arrow;
    public Vector3 SaveSpeed;
    private Vector3 dir;
    private float totalSpeed;
    private bool IsCrash;

    private List<GameObject> redObjects = new List<GameObject>();
    private string targetTag = "RedPiece"; // 검색할 태그
    private float rotationDuration; // 회전을 완료할 시간 (초)
    private float rotationSpeed;


    private void Start()
    {
           rotationSpeed = 360f;
           rotationDuration = 1f;
           GM = GameObject.Find("AlKKAGIManager");
    }

    public void MoveStart() //기물 이동
    {
        Invoke("RocateRed", 1f);
    }
    private void NotCrash() //헛스윙 체크
    {
        if (!IsCrash)
        {
            Debug.Log("파랑 헛스윙");
            GM.GetComponent<AlKKAGIManager>().IsMyTurn = true;
            GM.GetComponent<AlKKAGIManager>().IsFirstCrash = true;
        }
        else
        {
            Debug.Log("충돌!");
        }
    }

    private void MoveMath()
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
        GM.GetComponent<AlKKAGIManager>().CrashObjR = null;

        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //이 기물과 상대 기물의 거리값
        MoveSpeed = ((float)Math.Floor(Pita)); //속도값
        Vector3 direction = new Vector3(DisX * 100 - this.gameObject.transform.localPosition.x, 0, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;
        if (MoveSpeed < 2f)
        {
            Debug.Log("2이하");
            MoveSpeed = 5f;
        }

        this.gameObject.GetComponent<Rigidbody>().AddForce(Arrow * MoveSpeed, ForceMode.Impulse);

        redObjects.Clear(); //검색한 오브젝트 초기화
    }

    private void RocateRed() //적 탐색
    {
        StartCoroutine(GetRedPiecesCoroutine()); //사정거리 내의 빨강 검색
        Invoke("attack", 1f);
    }

    private void attack()
    {
        Invoke("NotCrash", 1f);
        if (redObjects.Count == 0) //RAY가 감지한 오브젝트가 없을때
        {
            GameObject Target = GM.GetComponent<AlKKAGIManager>().LeftRedPiece[UnityEngine.Random.Range(0, 15)];
            if (Target == null)
            {
                 Target = GM.GetComponent<AlKKAGIManager>().LeftRedPiece[UnityEngine.Random.Range(0, 15)];
            }
            else
            {
                targetlocal = Target.transform.localPosition;
                DisX = targetlocal.x / 100;
                DisZ = targetlocal.z / 100;
                MoveMath();
            }     
        }
        else
        {
            GameObject target = redObjects[UnityEngine.Random.Range(0, redObjects.Count)];
            targetlocal = target.transform.localPosition;
            DisX = targetlocal.x / 100;
            DisZ = targetlocal.z / 100;
            MoveMath();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RedPiece" && this.gameObject.tag == "BluePiece" && GM.GetComponent<AlKKAGIManager>().CrashObjR != collision.gameObject 
                && !GM.GetComponent<AlKKAGIManager>().IsMyTurn && GM.GetComponent<AlKKAGIManager>().IsFirstCrash)
        {
            IsCrash = true;

            GameObject collidedObject = collision.gameObject;

            GM.GetComponent<AlKKAGIManager>().CrashObjR = collidedObject;
            GM.GetComponent<AlKKAGIManager>().CrashObjB = this.gameObject;


            SaveSpeed = this.gameObject.GetComponent<Rigidbody>().velocity;
            totalSpeed = SaveSpeed.magnitude;
            dir = this.gameObject.transform.localPosition - collidedObject.transform.localPosition;

            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().velocity = Vector3.zero;

            GM.GetComponent<AlKKAGIManager>().IsFirstCrash = false;
            GM.GetComponent<AlKKAGIManager>().Crash();
        }
    }



    public void RedWin() //FPS 승리시
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.4f, ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(-SaveSpeed * 0.7f, ForceMode.Impulse);
        IFC();
    }
    public void Redlose() //FPS 패배시
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.7f *2f, ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(-SaveSpeed * 0.4f, ForceMode.Impulse);
        IFC();
    }
    private void IFC()
    {
        GM.GetComponent<AlKKAGIManager>().IsMyTurn = true;
        GM.GetComponent<AlKKAGIManager>().IsFirstCrash = true;
    }

    private IEnumerator GetRedPiecesCoroutine() //적 탐색
    {
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            Quaternion rotation = Quaternion.Euler(Vector3.up * Time.time * rotationSpeed); // 회전을 나타내는 Quaternion을 계산

            Vector3 rayOrigin = transform.position;  // 레이 발사 위치 계산 (레이의 시작점)

            Vector3 rayDirection = rotation * Vector3.forward * 20f; // 레이 방향 계산 (레이 방향을 회전에 따라 변경)

            Ray ray = new Ray(rayOrigin, rayDirection); // 레이 발사
            RaycastHit hitInfo;

            Debug.DrawRay(rayOrigin, rayDirection, Color.red);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.CompareTag(targetTag)) // 충돌한 오브젝트의 태그 확인
                {
                    GameObject hitObject = hitInfo.collider.gameObject;
                    if (!redObjects.Contains(hitObject))  // RedPiece 태그가 있는 오브젝트를 redObjects 리스트에 추가
                    {
                        redObjects.Add(hitObject);
                    }
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}