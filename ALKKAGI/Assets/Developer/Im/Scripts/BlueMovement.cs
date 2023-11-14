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
        GM.GetComponent<AlKKAGIManager>().SFX.PlayOneShot(GM.GetComponent<AlKKAGIManager>().audioManager.GetComponent<AudioManager>().ShootSound);

        GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
        GM.GetComponent<AlKKAGIManager>().CrashObjR = null;
        StartCoroutine(GM.GetComponent<AlKKAGIManager>().SoundPlay(1));

        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //이 기물과 상대 기물의 거리값
        Vector3 direction = new Vector3(DisX * 100 - this.gameObject.transform.localPosition.x, 0, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;

        MoveSpeed = Arrow.magnitude;
        //  Debug.Log("원본 " + MoveSpeed);

        if (MoveSpeed < 6f)
        {
            Pita = Pita * 2;
            // Debug.Log("진화 " + Pita);
        }

        Vector3 blueSPD = Arrow * Pita;
        //blueSPD.y = 0;
        //Debug.Log("총 이동량" + blueSPD);
        //this.gameObject.transform.localRotation = Quaternion.identity; 

        this.gameObject.GetComponent<Rigidbody>().AddForce(blueSPD * this.gameObject.GetComponent<Rigidbody>().mass, ForceMode.Impulse);

        redObjects.Clear(); //검색한 오브젝트 초기화

        Invoke("NotCrash", 1.5f);
    }
    private void NotCrash()
    {
        if (!GM.GetComponent<AlKKAGIManager>().blueCrash)
        {
            Destroy(GameObject.Find("ArrowBlue(Clone)"));
            StartCoroutine(IFC());
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
        if (GM.GetComponent<AlKKAGIManager>().isGetRedPiecesCoroutineRunning)
        {
            yield break; // 이미 실행 중이면 중복 실행 방지
        }
        GM.GetComponent<AlKKAGIManager>().isGetRedPiecesCoroutineRunning = true;


        // 360도의 레이를 발사하는 루프
        for (float angle = 0f; angle < 360f; angle += 3f)
        {
            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));
            Vector3 rayOrigin = transform.position;

            // 디버그 레이를 그리기 위한 코드
            Debug.DrawRay(rayOrigin, direction * rayLength, Color.red);

            RaycastHit hit;

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
            Debug.Log("special");
            GM.GetComponent<AlKKAGIManager>().BlueSelect();
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


            yield return new WaitForSeconds(0.3f);
            BlueShootEffect(Target);
            yield return new WaitForSeconds(0.3f);

            MoveMath();
            GM.GetComponent<AlKKAGIManager>().isGetRedPiecesCoroutineRunning = false;
        }
    }

    private void BlueShootEffect(GameObject target)
    {
        GM.GetComponent<AlKKAGIManager>().TurnObj.SetActive(false);
        GameObject newPiece = Instantiate(ArrowObj, (this.gameObject.transform.position + target.transform.position) / 2 + new Vector3(0, 0.5f, 0), Quaternion.identity);

        newPiece.transform.LookAt(target.transform);
    }

    public void RedWin() //FPS 승리시
    {
        GameObject redObj = GM.GetComponent<AlKKAGIManager>().CrashObjR;
        if (redObj.transform.position.x <= 16 && redObj.transform.position.x >= 0 && redObj.transform.position.z >= -18 && redObj.transform.position.z <= 0)
        {
            Debug.Log("SHOW : " +redObj.transform.position);
            redObj.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().AddForce(dir * totalSpeed * 0.5f, ForceMode.Impulse);
            StartCoroutine(IFC());
        }
        else
        {
            Debug.Log("외곽");
            redObj.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().AddForce(dir * totalSpeed * 0.5f, ForceMode.Impulse);
            StartCoroutine(IFC());
        }
    }
    public void Redlose() //FPS 패배시
    {
        GameObject redObj = GM.GetComponent<AlKKAGIManager>().CrashObjR;
        if (redObj.transform.localPosition.x <= 160 || redObj.transform.localPosition.x >= 144 || redObj.transform.localPosition.z >= -144 || redObj.transform.localPosition.z <= -180)
        {
            GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(-dir * totalSpeed * 0.5f, ForceMode.Impulse);
            StartCoroutine(IFC());
        }
        else
        {
            Debug.Log("외곽");
            GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(-dir * totalSpeed * 0.5f * 1.2f, ForceMode.Impulse);
            StartCoroutine(IFC());
        }
    }
    IEnumerator IFC()
    {
        yield return new WaitForSeconds(1f);
        GM.GetComponent<AlKKAGIManager>().RTC();
        GM.GetComponent<AlKKAGIManager>().IsMyTurn = true;
        GM.GetComponent<AlKKAGIManager>().IsFirstCrash = true;
    }
}