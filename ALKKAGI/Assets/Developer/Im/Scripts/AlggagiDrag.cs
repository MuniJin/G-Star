using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlggagiDrag : MonoBehaviour
{
    //�巡�׸� ���� �̵��� �ʿ��� ���� ����ϴ� ��ũ��Ʈ

    private bool IsPieceSelected = false;
    private Vector3 MoveDis;
    private float DisX;
    private float DisZ;
    private float Pita = 0f;
    private GameObject GM;
    private GameObject PauseButton;

    public GameObject MainObj;
    public GameObject Arrow;
    public GameObject ConCircle;
    public float ShootPower = 0f;
    private float cosA;
    double angleA;
    private void Start()
    {
        PauseButton = GameObject.Find("PauseButton");
        GM = GameObject.Find("AlKKAGIManager");
    }

    void OnMouseDrag()
    {
        if (GM.GetComponent<AlKKAGIManager>().IsMyTurn && !PauseButton.GetComponent<PauseButton>().IsPause && GM.GetComponent<AlKKAGIManager>().IsMove) //�����϶� �巡�׽�
        {
            float distance = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); //���콺 ������ ��������
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos); //������Ʈ �����ǿ� ���콺 �������� ����
            objPos.y = 0.5f;
            transform.position = objPos;



            MainObj.GetComponent<PieceMove>().RotationReset(); //ȸ���� �ʱ�ȭ

            //Arrow�� ������ �����ִ� ����
            Arrow.transform.position = new Vector3(-objPos.x, 0.5f, -objPos.z) + MainObj.transform.position * 2;
            Vector3 direction = Arrow.transform.position - MainObj.transform.position;
            Arrow.transform.rotation = Quaternion.LookRotation(direction);

            //���ɿ��� ����, ũ�⸦ �����ִ� ����(~76����)
            Vector3 dir = direction * -1;
            float hypotenuse = (float)Math.Sqrt(direction.x * direction.x + direction.z * direction.z); //Trigger�� ���������� ������ �ﰢ���� ����
            if (Math.Abs(dir.x) > Math.Abs(dir.z))
                cosA = dir.z / hypotenuse;
            else
                cosA = dir.x / hypotenuse;
            //Ʈ������ ������ ���� Sin�� ������� Cos�� ������� �����ִ� ���ǹ�
            if (dir.x > 0 && dir.z > 0)             //1�ù���
            {
                if (dir.z < dir.x)
                    angleA = Math.Acos(cosA);
                else
                    angleA = Math.Asin(cosA);
            }
            else if (dir.x < 0 && dir.z < 0)        //7�� ����
            {
                if (Math.Abs(dir.x) < Math.Abs(dir.z))
                    angleA = Math.Acos(cosA);
                else
                    angleA = Math.Asin(cosA);
            }
            else if (dir.x < 0 && dir.z > 0)       //11�� ����
                angleA = Math.Asin(cosA);
            else                                         //5�� ����
                angleA = Math.Acos(cosA);
            double degreesA = angleA * 180 / Math.PI; //cos��    �� �̿��Ͽ� ����(���� ����)�� �����ش�
            ConCircle.transform.localRotation = Quaternion.Euler(0, (float)degreesA, 0); //���� �ֽ�ȭ
            ConCircle.transform.localScale = new Vector3(0.125f * hypotenuse * 1.67f, 0.125f, 0.125f * hypotenuse * 1.67f); //ũ�� �ֽ�ȭ

            IsPieceSelected = true;
            if (Input.GetMouseButtonDown(1)) //��Ŭ���� �巡�� ���� ����
            {
                Debug.Log("���� ���");
                GM.GetComponent<AlKKAGIManager>().IsMove = false;
                IsPieceSelected = false;
                this.gameObject.transform.localPosition = new Vector3(0, 0.15f, 0);
                Arrow.transform.localPosition = new Vector3(0, 0.2f, 0);
                Arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                ConCircle.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
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
        if (IsPieceSelected)
        {
            ConCircle.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
            MainObj.GetComponent<Rigidbody>().isKinematic = false;
            GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
            GM.GetComponent<AlKKAGIManager>().CrashObjR = null;

            MoveDis = this.gameObject.transform.localPosition;
            this.gameObject.transform.localPosition = new Vector3(0, 0.15f, 0);
            Arrow.transform.localPosition = new Vector3(0, 0.2f, 0);
            Arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
            DisX = MoveDis.x;
            DisZ = MoveDis.z;
            MoveMath();
            MainObj.GetComponent<PieceMove>().MoveStart();
            IsPieceSelected = false;
            GM.GetComponent<AlKKAGIManager>().IsMove = false;
        }
    }
    private void MoveMath()
    {
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�巡�� �� �Ÿ���
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
