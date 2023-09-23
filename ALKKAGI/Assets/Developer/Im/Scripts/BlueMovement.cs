using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    public GameObject GM;
    private float MoveSpeed; //�̵��ӵ�
    public float Pita = 0f;
    public float DisX;
    public float DisZ;
    private Vector3 targetlocal;
    private Vector3 Arrow;
    public Vector3 SaveSpeed;
    private Vector3 dir;
    float totalSpeed;

    public List<GameObject> redObjects = new List<GameObject>();
    public string targetTag = "RedPiece"; // �˻��� �±�
    public float rotationDuration = 1.0f; // ȸ���� �Ϸ��� �ð� (��)



    private void Start()
    {
        rotationDuration = 3f;
        GM = GameObject.Find("AlKKAGIManager");
    }

    public void MoveStart() //�⹰ �̵�
    {
        Invoke("RocateRed", 1f);
    }

    private void MoveMath()
    {
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�� �⹰�� ��� �⹰�� �Ÿ���
        MoveSpeed = ((float)Math.Floor(Pita)); //�ӵ���
        Vector3 direction = new Vector3(DisX * 100 - this.gameObject.transform.localPosition.x, 0, DisZ * 100 - this.gameObject.transform.localPosition.z);
        Arrow = direction;
        this.gameObject.GetComponent<Rigidbody>().AddForce(Arrow * MoveSpeed, ForceMode.Impulse);
 
        redObjects.Clear(); //�˻��� ������Ʈ �ʱ�ȭ
        Debug.Log("�Ķ� ����!");
    }

    private void RocateRed() //48����
    {
        StartCoroutine(GetRedPiecesCoroutine()); //�����Ÿ� ���� ���� �˻� //50����

        GameObject Target = redObjects[UnityEngine.Random.Range(0, redObjects.Count)]; //52����
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
            Debug.Log("������浹");

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
    public void Redlose() //FPS�¸���
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjR.GetComponent<Rigidbody>().AddForce(SaveSpeed * 0.7f, ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(-SaveSpeed * 0.4f, ForceMode.Impulse);
    }

    public float rotationSpeed = 60f; // ȸ�� �ӵ� (60��/��)


    private IEnumerator GetRedPiecesCoroutine()
    {
        float elapsedTime = 0f;
        targetTag = "RedPiece";
        while (elapsedTime < rotationDuration)
        {
            // ȸ���� ��Ÿ���� Quaternion�� ���
            Quaternion rotation = Quaternion.Euler(Vector3.up * Time.time * rotationSpeed);

            // ���� �߻� ��ġ ���
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = rotation * transform.forward;

            // ���� �߻�
            Ray ray = new Ray(rayOrigin, rayDirection);
            RaycastHit hitInfo;

            Debug.DrawRay(rayOrigin, rayDirection * 10f, Color.red); // 10f�� ������ ����


            if (Physics.Raycast(ray, out hitInfo,30f))
            {
                // �浹�� ������Ʈ�� �±� Ȯ��
                if (hitInfo.collider.CompareTag(targetTag))
                {
                    if (!redObjects.Contains(hitInfo.collider.gameObject))
                    {
                        // RedPiece �±װ� �ִ� ������Ʈ�� redObjects ����Ʈ�� �߰�
                        redObjects.Add(hitInfo.collider.gameObject);
                    }
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}