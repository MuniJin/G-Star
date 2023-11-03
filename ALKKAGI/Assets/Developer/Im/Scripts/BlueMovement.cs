using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public bool redTurnCrash;

    private List<GameObject> redObjects = new List<GameObject>();
    private string targetTag = "RedPiece"; // 검색할 태그
    private float dieTime;
    GameObject[] uniqueRedPieces;
    public GameObject ArrowObj;

    public bool isdead;

    private void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
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

    public void MoveStart() //기물 이동
    {
        startRay();
    }

    private void MoveMath()
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
        GM.GetComponent<AlKKAGIManager>().CrashObjR = null;
        StartCoroutine(GM.GetComponent<AlKKAGIManager>().SoundPlay(1));

        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //이 기물과 상대 기물의 거리값
        Vector3 direction = new Vector3(DisX * 100 - this.gameObject.transform.localPosition.x, targetlocal.y, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;
        MoveSpeed = Arrow.magnitude;
        Debug.Log("원본 " + MoveSpeed);
        if (MoveSpeed < 9f)
        {
            Pita = Pita* 2;
            Debug.Log("진화 " + MoveSpeed);
        }


        this.gameObject.GetComponent<Rigidbody>().AddForce(Arrow * Pita, ForceMode.Impulse);

        redObjects.Clear(); //검색한 오브젝트 초기화

        Invoke("NotCrash", 1f);
    }
    private void NotCrash() 
    {
        if (!GM.GetComponent<AlKKAGIManager>().blueCrash)
        {
            Destroy(GameObject.Find("ArrowBlue(Clone)"));
            IFC();
        }
        else
        {
            Debug.Log("충돌!");
        }
    }

    private void startRay()
    {
        StartCoroutine(GetRedPiecesCoroutine()); //사정거리 내의 빨강 검색
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RedPiece" && this.gameObject.tag == "BluePiece" && GM.GetComponent<AlKKAGIManager>().CrashObjR != collision.gameObject
                && !GM.GetComponent<AlKKAGIManager>().IsMyTurn && GM.GetComponent<AlKKAGIManager>().IsFirstCrash)
        {
            GM.GetComponent<AlKKAGIManager>().blueCrash = true;

            GameObject collidedObject = collision.gameObject;

            GM.GetComponent<AlKKAGIManager>().CrashObjR = collidedObject;
            GM.GetComponent<AlKKAGIManager>().CrashObjB = this.gameObject;


            SaveSpeed = this.gameObject.GetComponent<Rigidbody>().velocity;
            totalSpeed = SaveSpeed.magnitude;
            dir = this.gameObject.transform.localPosition - collidedObject.transform.localPosition;
           
            //Debug.Log("totals - blue : " + totalSpeed);

            if (totalSpeed < 1f)
            {
                Debug.Log("제발");
                totalSpeed = 20f;
            }

            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().isKinematic = true;

            GM.GetComponent<AlKKAGIManager>().IsFirstCrash = false;
            StartCoroutine(GM.GetComponent<AlKKAGIManager>().Crash());

        }
    }

    public float rayLength = 20f;
    public float interval = 0.1f; // 레이를 발사하는 간격

    private IEnumerator GetRedPiecesCoroutine() //적 탐색
    {
        // 360도의 레이를 발사하는 루프
        for (float angle = 0f; angle < 360f; angle += 3f)
        {
            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));
            Vector3 rayOrigin = transform.position;

            // 디버그 레이를 그리기 위한 코드
            Debug.DrawRay(rayOrigin, direction * rayLength, Color.red);

            RaycastHit hit;

            if (angle >= 180)
                GM.GetComponent<AlKKAGIManager>().TurnObj.SetActive(false);

            if (Physics.Raycast(rayOrigin, direction, out hit, rayLength))
            {
                // 충돌한 오브젝트의 태그가 "RedPiece"인지 확인
                if (hit.collider.CompareTag("RedPiece"))
                {
                    redObjects.Add(hit.collider.gameObject);
                }

                yield return null;
            }
        }

        if (redObjects.Count == 0) //감지 실패시 다른 오브젝트로 이동 후 감지
        {

        }
        else //감지 성공시 배열 정렬 후 공격
        {
            redObjects = redObjects.Distinct().ToList();
            // 걸러진 RedPiece 오브젝트를 배열로 변환
            uniqueRedPieces = redObjects.ToArray();

            int targetNum = uniqueRedPieces.Length;
            GameObject Target = uniqueRedPieces[UnityEngine.Random.Range(0, targetNum)];
            targetlocal = Target.transform.localPosition;
            DisX = targetlocal.x / 100;
            DisZ = targetlocal.z / 100;

            BlueShootEffect(Target);

            yield return new WaitForSeconds(0.5f);

            MoveMath();
        }
    }

    private void BlueShootEffect(GameObject target)
    {
        GameObject newPiece = Instantiate(ArrowObj, (this.gameObject.transform.position + target.transform.position)/2+new Vector3(0,0.5f,0), Quaternion.identity);
        
        newPiece.transform.LookAt(target.transform);
    }
    
    public void RedWin() //FPS 승리시
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(-dir*totalSpeed* 0.4f, ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(dir * totalSpeed * 0.6f, ForceMode.Impulse);
        Invoke("IFC", 1f);
    }
    public void Redlose() //FPS 패배시
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(-dir * totalSpeed * 0.6f, ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(dir * totalSpeed * 0.4f, ForceMode.Impulse);
        Invoke("IFC", 1f);
    }
    private void IFC()
    {
        StartCoroutine(GM.GetComponent<AlKKAGIManager>().RedTurnChange());
        GM.GetComponent<AlKKAGIManager>().IsMyTurn = true;
        GM.GetComponent<AlKKAGIManager>().IsFirstCrash = true;
    }
}