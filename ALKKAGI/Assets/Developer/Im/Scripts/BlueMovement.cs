using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    private GameObject GM;
    private float MoveSpeed; //�̵��ӵ�
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
    private string targetTag = "RedPiece"; // �˻��� �±�
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

    public void MoveStart() //�⹰ �̵�
    {
        startRay();
    }

    private void MoveMath()
    {
        GM.GetComponent<AlKKAGIManager>().SFX.PlayOneShot(GM.GetComponent<AlKKAGIManager>().audioManager.GetComponent<AudioManager>().ShootSound);

        GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
        GM.GetComponent<AlKKAGIManager>().CrashObjR = null;
        StartCoroutine(GM.GetComponent<AlKKAGIManager>().SoundPlay(1));

        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�� �⹰�� ��� �⹰�� �Ÿ���
        Vector3 direction = new Vector3(DisX * 100 - this.gameObject.transform.localPosition.x, 0, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;

        MoveSpeed = Arrow.magnitude;
        //  Debug.Log("���� " + MoveSpeed);

        if (MoveSpeed < 6f)
        {
            Pita = Pita * 2;
            // Debug.Log("��ȭ " + Pita);
        }

        Vector3 blueSPD = Arrow * Pita;
        //blueSPD.y = 0;
        //Debug.Log("�� �̵���" + blueSPD);
        //this.gameObject.transform.localRotation = Quaternion.identity; 

        this.gameObject.GetComponent<Rigidbody>().AddForce(blueSPD * this.gameObject.GetComponent<Rigidbody>().mass, ForceMode.Impulse);

        redObjects.Clear(); //�˻��� ������Ʈ �ʱ�ȭ

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
            Debug.Log("�浹!");
        }
    }

    private void startRay()
    {
        StartCoroutine(GetRedPiecesCoroutine()); //�����Ÿ� ���� ���� �˻�
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
    public float interval = 0.1f; // ���̸� �߻��ϴ� ����

    private IEnumerator GetRedPiecesCoroutine() //�� Ž��
    {
        if (GM.GetComponent<AlKKAGIManager>().isGetRedPiecesCoroutineRunning)
        {
            yield break; // �̹� ���� ���̸� �ߺ� ���� ����
        }
        GM.GetComponent<AlKKAGIManager>().isGetRedPiecesCoroutineRunning = true;


        // 360���� ���̸� �߻��ϴ� ����
        for (float angle = 0f; angle < 360f; angle += 3f)
        {
            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));
            Vector3 rayOrigin = transform.position;

            // ����� ���̸� �׸��� ���� �ڵ�
            Debug.DrawRay(rayOrigin, direction * rayLength, Color.red);

            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, direction, out hit, rayLength))
            {
                // �浹�� ������Ʈ�� �±װ� "RedPiece"���� Ȯ��
                if (hit.collider.CompareTag("RedPiece"))
                {
                    redObjects.Add(hit.collider.gameObject);
                }

                yield return null;
            }
        }

        if (redObjects.Count == 0) //���� ���н� �ٸ� ������Ʈ�� �̵� �� ����
        {
            Debug.Log("special");
            GM.GetComponent<AlKKAGIManager>().BlueSelect();
        }
        else //���� ������ �迭 ���� �� ����
        {
            redObjects = redObjects.Distinct().ToList();
            // �ɷ��� RedPiece ������Ʈ�� �迭�� ��ȯ
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

    public void RedWin() //FPS �¸���
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
            Debug.Log("�ܰ�");
            redObj.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().AddForce(dir * totalSpeed * 0.5f, ForceMode.Impulse);
            StartCoroutine(IFC());
        }
    }
    public void Redlose() //FPS �й��
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
            Debug.Log("�ܰ�");
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