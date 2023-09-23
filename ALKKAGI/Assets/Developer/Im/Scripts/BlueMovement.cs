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

    public List<GameObject> redObjects = new List<GameObject>();
    public string targetTag = "RedPiece"; // 검색할 태그
    public float rotationDuration = 1.0f; // 회전을 완료할 시간 (초)



    private void Start()
    {
        rotationDuration = 3f;
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
        Vector3 direction = new Vector3(DisX * 100 - this.gameObject.transform.localPosition.x, 0, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;
        this.gameObject.GetComponent<Rigidbody>().AddForce(Arrow * MoveSpeed, ForceMode.Impulse);
 
        redObjects.Clear(); //검색한 오브젝트 초기화
        Debug.Log("파랑 공격!");
    }

    private void RocateRed() //48라인
    {
        StartCoroutine(GetRedPiecesCoroutine()); //사정거리 내의 빨강 검색 //50라인

        GameObject Target = redObjects[UnityEngine.Random.Range(0, redObjects.Count)]; //52라인
        targetlocal = Target.transform.localPosition;
        DisX = targetlocal.x / 100;
        DisZ = targetlocal.z / 100;
        MoveMath();
        
        //GameObject Target = GM.GetComponent<AlKKAGIManager>().LeftRedPiece[UnityEngine.Random.Range(0, 15)];
        //if (Target == null)
        //{
        //    RocateRed();
        //}
        //else
        //{
        //    targetlocal = Target.transform.localPosition;
        //    DisX = targetlocal.x / 100;
        //    DisZ = targetlocal.z / 100;
        //    MoveMath();
        //}
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

    public float rotationSpeed = 60f; // 회전 속도 (60도/초)


    private IEnumerator GetRedPiecesCoroutine()
    {
        float elapsedTime = 0f;
        targetTag = "RedPiece";
        while (elapsedTime < rotationDuration)
        {
            // 회전을 나타내는 Quaternion을 계산
            Quaternion rotation = Quaternion.Euler(Vector3.up * Time.time * rotationSpeed);

            // 레이 발사 위치 계산
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = rotation * transform.forward;

            // 레이 발사
            Ray ray = new Ray(rayOrigin, rayDirection);
            RaycastHit hitInfo;

            Debug.DrawRay(rayOrigin, rayDirection * 10f, Color.red); // 10f는 레이의 길이


            if (Physics.Raycast(ray, out hitInfo,30f))
            {
                // 충돌한 오브젝트의 태그 확인
                if (hitInfo.collider.CompareTag(targetTag))
                {
                    if (!redObjects.Contains(hitInfo.collider.gameObject))
                    {
                        // RedPiece 태그가 있는 오브젝트를 redObjects 리스트에 추가
                        redObjects.Add(hitInfo.collider.gameObject);
                    }
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}