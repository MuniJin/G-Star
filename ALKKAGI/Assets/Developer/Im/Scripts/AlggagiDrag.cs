using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlggagiDrag : MonoBehaviour
{
    //드래그를 통해 이동에 필요한 값을 계산하는 스크립트

    private bool IsPieceSelected = false;
    private Vector3 MoveDis;
    private float DisX;
    private float DisZ;
    public float ShootPower = 0f;
    private float Pita = 0f;
    private Vector3 Direction = new Vector3(0, 0, 0);
    public GameObject MainObj;
    public GameObject Arrow;
    private GameObject GM;
    private GameObject PauseButton;

    private void Start()
    {
        PauseButton = GameObject.Find("PauseButton");
        GM = GameObject.Find("AlKKAGIManager");
    }

    void OnMouseDrag()
    {
        if (GM.GetComponent<AlKKAGIManager>().IsMyTurn && !PauseButton.GetComponent<PauseButton>().IsPause && GM.GetComponent<AlKKAGIManager>().IsMove) //내턴일때 드래그시
        {
            MainObj.GetComponent<PieceMove>().RotationReset(); //회전값 초기화
            float distance = Camera.main.WorldToScreenPoint(transform.position).z;

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); //마우스 포지션 가져오기
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos); //오브젝트 포지션에 마우스 포지션을 대입
            objPos.y = 0.5f;
            transform.position = objPos;

            Arrow.transform.position = new Vector3(-objPos.x, 0.5f, -objPos.z) + MainObj.transform.position * 2;

            Vector3 direction = Arrow.transform.position - MainObj.transform.position;
            Arrow.transform.rotation = Quaternion.LookRotation(direction);

            IsPieceSelected = true;
        }
    }
    private void OnMouseUp()
    {
        if (IsPieceSelected)
        {
            MoveDis = this.gameObject.transform.localPosition;
            this.gameObject.transform.localPosition = new Vector3(0, 0.15f, 0);
            Arrow.transform.localPosition = new Vector3(0, 0.15f, 0);
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
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //드래그 한 거리값
        ShootPower = ((float)Math.Floor(Pita * 100) / 100) * 2; //속도값
        Vector3 direction = new Vector3(DisX, 0, DisZ);
        MainObj.GetComponent<PieceMove>().Arrow = -direction;
    }
}
