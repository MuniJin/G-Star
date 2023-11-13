using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlggagiDrag : MonoBehaviour
{
    //�巡�׸� ���� �̵��� �ʿ��� ���� ����ϴ� ��ũ��Ʈ

    private bool IsPieceSelected = false;
    private Vector3 MoveDis; //�̵� ������ ���ϱ� ���� ���Ͱ�
    private float DisX;
    private float DisZ;
    private float Pita = 0f; //��Ÿ��󽺸� �̿��Ͽ� ������ ���̸� ���� �� �̸� ������ �ٲ۴�
    private GameObject GM;
    private GameObject PauseButton;

    [SerializeField] private GameObject MainObj;   //���� ������Ʈ(�θ� ������Ʈ)
    [SerializeField] private GameObject Arrow;     //������Ʈ�� ���� ���� ����
    [SerializeField] private GameObject ConCircle; //������Ʈ�� ���� �����ִ� ���ɿ�
    public float ShootPower = 0f; //�߻� ��
    private float trigo; //�ڻ���, ���ΰ��� ������ ��
    double angleA; //���ɿ��� ������ ���ϱ� ���� float��

    private void Start()
    {
        PauseButton = GameObject.Find("PauseButton");
        GM = GameObject.Find("AlKKAGIManager");
    }

    void OnMouseDrag()
    {
        if (GM.GetComponent<AlKKAGIManager>().IsMyTurn && !PauseButton.GetComponent<PauseButton>().IsPause && GM.GetComponent<AlKKAGIManager>().IsMove) // �����϶� �巡�׽�
        {
            float distance = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); // �콺 ������ ��������
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos); //������Ʈ �����ǿ� ���콺 �������� ����

            MainObj.GetComponent<PieceMove>().RotationReset(); //ȸ���� �ʱ�ȭ

            //Arrow�� ������ �����ִ� ����
            Arrow.transform.position = new Vector3(-objPos.x, 0.5f, -objPos.z) + MainObj.transform.position * 2;
            Vector3 direction = Arrow.transform.position - MainObj.transform.position;
            Arrow.transform.rotation = Quaternion.LookRotation(direction);

            // ���ɿ��� ����, ũ�⸦ �����ִ� ����(~76����)
            Vector3 dir = direction * -1;
            float hypotenuse = (float)Math.Sqrt(direction.x * direction.x + direction.z * direction.z); //Trigger�� ���������� ������ �ﰢ���� ����

            if (Math.Abs(dir.x) > Math.Abs(dir.z))
                trigo = dir.z / hypotenuse;
            else
                trigo = dir.x / hypotenuse;

            //Ʈ������ ���⿡ ���� Sin�� ������� Cos�� ������� �����ִ� ���ǹ�
            if (dir.x > 0 && dir.z > 0)            //1�ù���
            {
                if (dir.z < dir.x)
                    angleA = Math.Acos(trigo);
                else
                    angleA = Math.Asin(trigo);
            }
            else if (dir.x < 0 && dir.z < 0)       //7�� ����
            {
                if (Math.Abs(dir.x) < Math.Abs(dir.z))
                    angleA = Math.Acos(trigo);
                else
                    angleA = Math.Asin(trigo);
            }
            else if (dir.x < 0 && dir.z > 0)       //11�� ����
                angleA = Math.Asin(trigo);
            else                                   //5�� ����
                angleA = Math.Acos(trigo);
            double degreesA = angleA * 180 / Math.PI; //cos��    �� �̿��Ͽ� ����(���� ����)�� �����ش�

            if (hypotenuse > 5f)
            {
                // 5f �Ÿ��� ���� �ʵ��� ��ġ�� ����
                float scaleFactor = 5f / hypotenuse;
                objPos = MainObj.transform.position - direction * scaleFactor;
                hypotenuse = 5f;
                Arrow.transform.position = new Vector3(-objPos.x, 0.5f, -objPos.z) + MainObj.transform.position * 2; // Arrow ��ġ�� ������Ʈ
            }

            ConCircle.transform.localRotation = Quaternion.Euler(0, (float)degreesA, 0); //���� �ֽ�ȭ
            objPos.y = 0.5f;
            transform.position = objPos;

            ConCircle.transform.localScale = new Vector3(0.125f * hypotenuse * 1.67f, 0.125f, 0.125f * hypotenuse * 1.67f); // ũ�� �ֽ�ȭ

            IsPieceSelected = true;

            if (Input.GetMouseButtonDown(1)) // ��Ŭ���� or �ʹ� ������ �巡�� ���� ����
            {
                Debug.Log("���� ���");
                GM.GetComponent<AlKKAGIManager>().IsMove = false;
                IsPieceSelected = false;
                this.gameObject.transform.localPosition = new Vector3(0, 0.15f, 0);
                Arrow.transform.localPosition = new Vector3(0, 0.2f, 0);
                Arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                ConCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                Invoke("DragReset", 1f);
            }
        }
    }


    private void DragReset()
    {
        GM.GetComponent<AlKKAGIManager>().IsMove = true;
        IsPieceSelected = true;
    }
    private void OnMouseUp()
    {
        MoveDis = this.gameObject.transform.localPosition;
        this.gameObject.transform.localPosition = new Vector3(0, 0.15f, 0);
        Arrow.transform.localPosition = new Vector3(0, 0.2f, 0);
        Arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        DisX = MoveDis.x;
        DisZ = MoveDis.z;
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�巡�� �� �Ÿ���
        if (IsPieceSelected)
        {
            if (Pita < 1.5f)
            {
                Debug.Log("���� ���");
                GM.GetComponent<AlKKAGIManager>().IsMove = false;
                IsPieceSelected = false;
                this.gameObject.transform.localPosition = new Vector3(0, 0.15f, 0);
                Arrow.transform.localPosition = new Vector3(0, 0.2f, 0);
                Arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                ConCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                Invoke("DragReset", 1f);
            }
            else
            {
                ConCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                MainObj.GetComponent<Rigidbody>().isKinematic = false;
                GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
                GM.GetComponent<AlKKAGIManager>().CrashObjR = null;

                MoveMath();
                MainObj.GetComponent<PieceMove>().MoveStart();
                IsPieceSelected = false;
                GM.GetComponent<AlKKAGIManager>().IsMove = false;
            }
        }
    }
    private void MoveMath()
    {
        //Debug.Log(Pita + "this is pita");
        ShootPower = ((float)Math.Floor(Pita * 100) / 100) * 2; //�ӵ���

        //Debug.Log(ShootPower + " SHOPS");

        //Debug.Log(ShootPower + " SHOPSs");
        //Debug.Log(DisX + "  this is x ");
        //Debug.Log(DisZ+ "  this is z ");

        Vector3 direction = new Vector3(DisX, 0, DisZ);
        MainObj.GetComponent<PieceMove>().Arrow = -direction;
    }
}
