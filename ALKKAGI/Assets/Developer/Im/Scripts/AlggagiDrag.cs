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
    public float DisX;
    public float DisZ;
    public float ShootPower = 0f;
    public float Pita = 0f;
    public  Vector3 Direction = new Vector3 (0,0,0);
    public GameObject MainObj;
    public GameObject GM;

    private void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
    }

    void OnMouseDrag()
    {
        if (GM.GetComponent<AlKKAGIManager>().IsMyTurn)
        {
            float distance = Camera.main.WorldToScreenPoint(transform.position).z;
    
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
            objPos.y = 0.5f;
            transform.position = objPos;
    
            IsPieceSelected = true;
        }
    }
    private void OnMouseUp()
    {
        if (IsPieceSelected)
        {
            MoveDis = this.gameObject.transform.localPosition;
            this.gameObject.transform.localPosition = new Vector3(0,0,0);
            DisX = MoveDis.x;
            DisZ = MoveDis.z;
            MoveMath();
            MainObj.GetComponent<PieceMove>().MoveStart();
            IsPieceSelected = false;
        }
    }
    private void MoveMath()
    {
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�巡�� �� �Ÿ���
        ShootPower = ((float)Math.Floor(Pita * 100) / 100)*2; //�ӵ���
        Vector3 direction = new Vector3 (DisX, 0, DisZ);
        MainObj.GetComponent<PieceMove>().Arrow = -direction;
    }
}
