using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Unit : MonoBehaviour
{
    public GameObject DirSys;
    public GameObject PowerSys;
    public GameObject MyGameObj;
    public GameObject CrashObj;
    public GameObject GameManager;

    public AudioClip ShootSound;
    public AudioClip CrashSound;

    private AudioSource myAudioSource;

    private bool IsWin = false;
    private bool IsDir = false;
    private bool IsPower = false;
    private bool IsMyTurn = false;
    private bool IsPieceSelected = false;

    public Vector3 MyPieceLocal = new Vector3(0, 0, 0);
    public Vector3 BluePieceLocal = new Vector3(0, 0, 0);

    private void Start()
    {
        IsMyTurn = true;
        MyGameObj = GetComponent<GameObject>();
        myAudioSource = GetComponent<AudioSource>();
    }
    void OnMouseDrag()
    {
        if (IsMyTurn)
        {
            float distance = Camera.main.WorldToScreenPoint(transform.position).z;

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
            objPos.y = -0.5f;
            transform.position = objPos;
        }
    }
    private void shoot()
    {
        if (IsPieceSelected) //기물이 선택되었다면
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsDir == false && IsPower == false) //스페이스바를 눌렀다면
            {
                //방향조절 시스템이 먼저 활성화 된다.
                DirSys.SetActive(true);
                IsDir = true;
                if (Input.GetKeyDown(KeyCode.Space) && IsDir == true && IsPower == false) //스페이스바를 다시한번 눌렀다면
                {
                    //방향이 확정된다

                    //방향시스템이 비활성화된다
                    DirSys.SetActive(false);
                    //파워시스템이 활성화된다.
                    PowerSys.SetActive(true);
                    IsPower = true;
                    if (Input.GetKeyDown(KeyCode.Space) && IsDir == true && IsPower == true) //스페이스바를 다시한번 눌렀다면
                    {
                        //파워가 확정된다.

                        //튕기는 소리 재생
                        myAudioSource.PlayOneShot(ShootSound);
                        //발사(이동)

                        //내 턴 종료
                        IsMyTurn = false;
                    }
                }
            }
        }
    }

    private void FPSResult()
    {
        if (IsWin)
        {
            //CrashObj가 튕겨져 나가기

        }
        else
        {
            //내가 반대방향으로 튕겨져 나가기

        }
    }

    //충돌체크
    private void OnCollisionEnter(Collision collision)
    {
        //충돌음 재생
        myAudioSource.PlayOneShot(CrashSound);

        //충돌한 위치값 저장
        MyPieceLocal = MyGameObj.transform.position;
        CrashObj = collision.gameObject;
        BluePieceLocal = CrashObj.transform.position;


        //충돌된 기물들의 태그 넘겨주기

        //GameManager.GetComponent<GameManager>().CrashCheck(); // 크래시 체크에 MyPieceLocal BluePieceLocal값 가져오는 함수 만들기
        if (collision.gameObject.tag == "Solider")
        {

        }
        if (collision.gameObject.tag == "Cannon")
        {

        }
        if (collision.gameObject.tag == "Horse")
        {

        }
        if (collision.gameObject.tag == "Chariot")
        {

        }
        if (collision.gameObject.tag == "Elephant")
        {

        }
        if (collision.gameObject.tag == "Guard")
        {

        }
        if (collision.gameObject.tag == "King")
        {

        }
    }
}
