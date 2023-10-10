using System;
using System.Collections;
using System.Collections.Generic;
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
    private bool IsCrash;

    private List<GameObject> redObjects = new List<GameObject>();
    private string targetTag = "RedPiece"; // �˻��� �±�
    private float rotationDuration; // ȸ���� �Ϸ��� �ð� (��)
    private float rotationSpeed;


    private void Start()
    {
           rotationSpeed = 360f;
           rotationDuration = 1f;
           GM = GameObject.Find("AlKKAGIManager");
    }

    public void MoveStart() //�⹰ �̵�
    {
        Invoke("RocateRed", 1f);
    }
    private void NotCrash() //�꽺�� üũ
    {
        if (!IsCrash)
        {
            Debug.Log("�Ķ� �꽺��");
            GM.GetComponent<AlKKAGIManager>().IsMyTurn = true;
            GM.GetComponent<AlKKAGIManager>().IsFirstCrash = true;
        }
        else
        {
            Debug.Log("�浹!");
        }
    }

    private void MoveMath()
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
        GM.GetComponent<AlKKAGIManager>().CrashObjR = null;

        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�� �⹰�� ��� �⹰�� �Ÿ���
        MoveSpeed = ((float)Math.Floor(Pita)); //�ӵ���
        Vector3 direction = new Vector3(DisX * 100 - this.gameObject.transform.localPosition.x, 0, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;
        if (MoveSpeed < 2f)
        {
            Debug.Log("2����");
            MoveSpeed = 5f;
        }

        this.gameObject.GetComponent<Rigidbody>().AddForce(Arrow * MoveSpeed, ForceMode.Impulse);

        redObjects.Clear(); //�˻��� ������Ʈ �ʱ�ȭ
    }

    private void RocateRed() //�� Ž��
    {
        StartCoroutine(GetRedPiecesCoroutine()); //�����Ÿ� ���� ���� �˻�
        Invoke("attack", 1f);
    }

    private void attack()
    {
        Invoke("NotCrash", 1f);
        if (redObjects.Count == 0) //RAY�� ������ ������Ʈ�� ������
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



    public void RedWin() //FPS �¸���
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.4f, ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(-SaveSpeed * 0.7f, ForceMode.Impulse);
        IFC();
    }
    public void Redlose() //FPS �й��
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

    private IEnumerator GetRedPiecesCoroutine() //�� Ž��
    {
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            Quaternion rotation = Quaternion.Euler(Vector3.up * Time.time * rotationSpeed); // ȸ���� ��Ÿ���� Quaternion�� ���

            Vector3 rayOrigin = transform.position;  // ���� �߻� ��ġ ��� (������ ������)

            Vector3 rayDirection = rotation * Vector3.forward * 20f; // ���� ���� ��� (���� ������ ȸ���� ���� ����)

            Ray ray = new Ray(rayOrigin, rayDirection); // ���� �߻�
            RaycastHit hitInfo;

            Debug.DrawRay(rayOrigin, rayDirection, Color.red);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.CompareTag(targetTag)) // �浹�� ������Ʈ�� �±� Ȯ��
                {
                    GameObject hitObject = hitInfo.collider.gameObject;
                    if (!redObjects.Contains(hitObject))  // RedPiece �±װ� �ִ� ������Ʈ�� redObjects ����Ʈ�� �߰�
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