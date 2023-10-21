using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlKKAGIManager : Singleton<AlKKAGIManager>
{
    private int leftBlue = 0; //파랑의 남은 기물 수
    private int BluePattern = 0; //AI 패턴
    private GameObject[] LeftBluePiece; //파랑의 남은 기물
    private AudioSource myAudioSource;

    public AudioClip ShootSound;
    public AudioClip CrashSound;
    public AudioClip DeathSound;

    public GameObject randomChildObject; //선택된 파랑의 기물
    public GameObject BluePieces; //파랑 기물
    public GameObject RedPieces;  //빨강 기물
    public GameObject[] LeftPieces; //모든 남은 기물
    public GameObject[] LeftRedPiece; //플레이어의 남은 기물

    public bool IsWin; //FPS 승패 체크
    public bool blueT; //파랑 턴 체크
    public bool IsMyTurn; // true일경우, Red턴 // false일경우, Blue턴
    public bool IsMove; //이동 체크
    public bool IsFirstCrash;
    public int CheatMode; //테스트용 치트모드

    public GameObject BoardObj;
    public GameObject CrashObjR; //빨강 충돌한 기물
    public GameObject CrashObjB; //파랑 충돌한 기물

    private void Start()
    {
        IsFirstCrash = true;
        IsMyTurn = true;
        IsMove = true;
        myAudioSource = GetComponent<AudioSource>();
    }

    public void Crash() //충돌
    {
        myAudioSource.PlayOneShot(CrashSound); //충돌음 재생

        SceneManager.LoadScene("cinemachintest");  //fps 씬 변환


        BoardObj.SetActive(false);
    }

    public void FPSResults()
    {
        Invoke("FPSResult", 1f);
    }

    public void FPSResult() //FPS종료
    {

        if (IsMyTurn)
        {
            if (IsWin) //승리했을시
            {
                Debug.Log("FPS 승리");
                CrashObjR.GetComponent<PieceMove>().Win();
            }
            else //패배했을시
            {
                Debug.Log("FPS 패배");
                CrashObjR.GetComponent<PieceMove>().lose();
            }
        }
        if (!IsMyTurn)
        {
            if (IsWin) //승리했을시
            {
                Debug.Log("FPS 승리 - 적턴");
                CrashObjB.GetComponent<BlueMovement>().RedWin();
            }
            else //패배했을신
            {
                Debug.Log("FPS 패배 - 적턴");
                CrashObjB.GetComponent<BlueMovement>().Redlose();
            }
        }
    }

    public void BlueSelect()
    {
        BluePattern = Random.Range(1, 5);

        if (BluePattern == 1) //패턴1 뒷열 오브젝트들
        {
            randomChildObject = LeftBluePiece[Random.Range(0, 8)]; //0~7
            repick();
            randomChildObject.GetComponent<BlueMovement>().MoveStart();
        }
        if (BluePattern == 2)//패턴2 포
        {
            randomChildObject = LeftBluePiece[Random.Range(9, 11)]; //9~10
            repick();
            randomChildObject.GetComponent<BlueMovement>().MoveStart();
        }
        if (BluePattern == 3)//패턴3 졸병
        {
            randomChildObject = LeftBluePiece[Random.Range(11, 16)]; //11~15
            repick();
            randomChildObject.GetComponent<BlueMovement>().MoveStart();
        }
        if (BluePattern == 4)//패턴4 왕
        {
            randomChildObject = LeftBluePiece[8]; //8
            if (leftBlue > 8) //왕이 공격당했거나 절반이상 죽었다면
            {
                randomChildObject.GetComponent<BlueMovement>().MoveStart();
            }
            else //아니면 왕은 안움직임
            {
                BlueSelect();
            }
        }
    }

    public void BlueTurn()
    {
        Invoke("BlueStart", 1f);
    }
    private void BlueStart()
    {
        IsMyTurn = false;
        Debug.Log("파랑턴 시작");
        BlueSelect();
        Invoke("RedTurn", 3f);
    }
    private void RedTurn()
    {
        Debug.Log("rt");
        blueT = false;
        IsMove = true;
    }
    private void repick()
    {
        if (randomChildObject == null)//선택된 대상이 null값일때
        {
            Debug.Log("repick");
            BlueSelect();//다시 고른다
        }
        else if (randomChildObject.transform.localPosition.z > -177f || randomChildObject.transform.localPosition.z < -200f | randomChildObject.transform.localPosition.x > 162f || randomChildObject.transform.localPosition.x < 142f)
        {
            Debug.Log("repick");
            BlueSelect();//다시 고른다
        }
    }
    public void PieceSet()
    {
        // "bluePieces" 아래의 모든 자식 GameObjects를 배열에 저장합니다.
        LeftBluePiece = new GameObject[BluePieces.transform.childCount];

        for (int i = 0; i < BluePieces.transform.childCount; i++)
        {
            LeftBluePiece[i] = BluePieces.transform.GetChild(i).gameObject;
        }

        // "bluePieces" 아래의 모든 자식 GameObjects를 배열에 저장합니다.
        LeftRedPiece = new GameObject[RedPieces.transform.childCount];

        for (int i = 0; i < RedPieces.transform.childCount; i++)
        {
            LeftRedPiece[i] = RedPieces.transform.GetChild(i).gameObject;
        }
    }

    private int a, b, c, d, e, f, g, h, i, j, k, l;
    public void Death(int deathPiece)
    {
        //데스 사운드 재생
        myAudioSource.PlayOneShot(DeathSound);

        //죽은 유닛 setactive false;
        if (deathPiece == 1)
        {
            LeftPieces[a].SetActive(false);
            a++;
        }
        if (deathPiece == 2)
        {
            LeftPieces[5 + b].SetActive(false);
            b++;
        }
        if (deathPiece == 3)
        {
            LeftPieces[7 + c].SetActive(false);
            c++;
        }
        if (deathPiece == 4)
        {
            LeftPieces[9 + d].SetActive(false);
            d++;
        }
        if (deathPiece == 5)
        {
            LeftPieces[11 + e].SetActive(false);
            e++;
        }
        if (deathPiece == 6)
        {
            LeftPieces[13 + f].SetActive(false);
            f++;
        }
        if (deathPiece > 6)
        {
            leftBlue++;
            if (deathPiece == 7)
            {
                LeftPieces[15 + g].SetActive(false);
                g++;
            }
            if (deathPiece == 8)
            {
                LeftPieces[20 + h].SetActive(false);
                h++;
            }
            if (deathPiece == 9)
            {
                LeftPieces[22 + i].SetActive(false);
                i++;
            }
            if (deathPiece == 10)
            {
                LeftPieces[24 + j].SetActive(false);
                j++;
            }
            if (deathPiece == 11)
            {
                LeftPieces[26 + k].SetActive(false);
                k++;
            }
            if (deathPiece == 12)
            {
                LeftPieces[28 + l].SetActive(false);
                l++;
            }
        }
    }
    public void GameOver(int who)
    {
        if (who == 0)
        {
            //Blue Is Win

        }

        if (who == 1)
        {
            //Red Is Win

        }
    }
}
