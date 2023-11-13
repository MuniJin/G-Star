using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlggagiDrag : MonoBehaviour
{
    //드래그를 통해 이동에 필요한 값을 계산하는 스크립트

    private bool IsPieceSelected = false;
    private Vector3 MoveDis; //이동 방향을 구하기 위한 벡터값
    private float DisX;
    private float DisZ;
    private float Pita = 0f; //피타고라스를 이용하여 빗변의 길이를 구한 뒤 이를 힘으로 바꾼다
    private GameObject GM;
    private GameObject PauseButton;

    [SerializeField] private GameObject MainObj;   //메인 오브젝트(부모 오브젝트)
    [SerializeField] private GameObject Arrow;     //오브젝트의 예상 진행 방향
    [SerializeField] private GameObject ConCircle; //오브젝트의 힘을 보여주는 동심원
    public float ShootPower = 0f; //발사 힘
    private float trigo; //코사인, 사인값을 가지는 값
    double angleA; //동심원의 각도를 구하기 위한 float값

    private void Start()
    {
        PauseButton = GameObject.Find("PauseButton");
        GM = GameObject.Find("AlKKAGIManager");
    }

    void OnMouseDrag()
    {
        if (GM.GetComponent<AlKKAGIManager>().IsMyTurn && !PauseButton.GetComponent<PauseButton>().IsPause && GM.GetComponent<AlKKAGIManager>().IsMove) // 내턴일때 드래그시
        {
            float distance = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); // 우스 포지션 가져오기
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos); //오브젝트 포지션에 마우스 포지션을 대입

            MainObj.GetComponent<PieceMove>().RotationReset(); //회전값 초기화

            //Arrow의 방향을 정해주는 라인
            Arrow.transform.position = new Vector3(-objPos.x, 0.5f, -objPos.z) + MainObj.transform.position * 2;
            Vector3 direction = Arrow.transform.position - MainObj.transform.position;
            Arrow.transform.rotation = Quaternion.LookRotation(direction);

            // 동심원의 방향, 크기를 정해주는 라인(~76라인)
            Vector3 dir = direction * -1;
            float hypotenuse = (float)Math.Sqrt(direction.x * direction.x + direction.z * direction.z); //Trigger를 꼭직점으로 가지는 삼각형의 빗변

            if (Math.Abs(dir.x) > Math.Abs(dir.z))
                trigo = dir.z / hypotenuse;
            else
                trigo = dir.x / hypotenuse;

            //트리거의 방향에 따라서 Sin을 사용할지 Cos를 사용할지 가려주는 조건문
            if (dir.x > 0 && dir.z > 0)            //1시방향
            {
                if (dir.z < dir.x)
                    angleA = Math.Acos(trigo);
                else
                    angleA = Math.Asin(trigo);
            }
            else if (dir.x < 0 && dir.z < 0)       //7시 방향
            {
                if (Math.Abs(dir.x) < Math.Abs(dir.z))
                    angleA = Math.Acos(trigo);
                else
                    angleA = Math.Asin(trigo);
            }
            else if (dir.x < 0 && dir.z > 0)       //11시 방향
                angleA = Math.Asin(trigo);
            else                                   //5시 방향
                angleA = Math.Acos(trigo);
            double degreesA = angleA * 180 / Math.PI; //cos값    을 이용하여 각도(원의 기울기)를 구해준다

            if (hypotenuse > 5f)
            {
                // 5f 거리를 넘지 않도록 위치를 제한
                float scaleFactor = 5f / hypotenuse;
                objPos = MainObj.transform.position - direction * scaleFactor;
                hypotenuse = 5f;
                Arrow.transform.position = new Vector3(-objPos.x, 0.5f, -objPos.z) + MainObj.transform.position * 2; // Arrow 위치를 업데이트
            }

            ConCircle.transform.localRotation = Quaternion.Euler(0, (float)degreesA, 0); //기울기 최신화
            objPos.y = 0.5f;
            transform.position = objPos;

            ConCircle.transform.localScale = new Vector3(0.125f * hypotenuse * 1.67f, 0.125f, 0.125f * hypotenuse * 1.67f); // 크기 최신화

            IsPieceSelected = true;

            if (Input.GetMouseButtonDown(1)) // 우클릭시 or 너무 가까우면 드래그 상태 해제
            {
                Debug.Log("선택 취소");
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
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //드래그 한 거리값
        if (IsPieceSelected)
        {
            if (Pita < 1.5f)
            {
                Debug.Log("선택 취소");
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
        ShootPower = ((float)Math.Floor(Pita * 100) / 100) * 2; //속도값

        //Debug.Log(ShootPower + " SHOPS");

        //Debug.Log(ShootPower + " SHOPSs");
        //Debug.Log(DisX + "  this is x ");
        //Debug.Log(DisZ+ "  this is z ");

        Vector3 direction = new Vector3(DisX, 0, DisZ);
        MainObj.GetComponent<PieceMove>().Arrow = -direction;
    }
}
