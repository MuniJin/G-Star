using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlKKAGIManager : MonoBehaviour
{
    private int leftBlue = 0;
    private int BluePattern = 0;
    public GameObject randomChildObject;
    public GameObject BluePieces;
    public GameObject[] LeftPieces;
    public GameObject[] LeftBluePiece;

    public AudioClip ShootSound;
    public AudioClip CrashSound;
    public AudioClip DeathSound;

    private AudioSource myAudioSource;

    private bool IsWin = false;
    public bool IsMyTurn = false; // true일경우, Red턴 // false일경우, Blue턴

    public GameObject CrashObjR;
    public GameObject CrashObjB;

    private void Start()
    {
        IsMyTurn = true;
        myAudioSource = GetComponent<AudioSource>();
    }

    public void Crash()
    {
        //충돌음 재생
        myAudioSource.PlayOneShot(CrashSound);
        //fps 씬 변환
        //SceneManager.LoadScene("FPS씬 / 임시변수임");


        FPSResult(); // test
    }

    public void FPSResult()
    {
        if (Random.Range(1, 4) > 2) //test
            IsWin = false;               //test
        else                                //test
            IsWin = true;                //test

        if (IsWin) //승리
        {
            Debug.Log("FPS 승리");
            CrashObjR.GetComponent<PieceMove>().Win();
        }
        else //패배
        {
            Debug.Log("FPS 패배");
            CrashObjR.GetComponent<PieceMove>().lose();
        }
        BlueTurn();
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

    public void BlueSelect()
    {
        BluePattern = Random.Range(1, 5);
        if (BluePattern == 1) //패턴1 뒷열
        {
            randomChildObject = LeftBluePiece[Random.Range(0, 8)];
        }
        if (BluePattern == 2)//패턴2 포
        {
            randomChildObject = LeftBluePiece[Random.Range(9, 11)];
        }
        if (BluePattern == 3)//패턴3 졸병
        {
            randomChildObject = LeftBluePiece[Random.Range(11,16)];
        }
        if(BluePattern == 4)//패턴4 왕
        {
            if (LeftBluePiece[8].transform.localPosition != new Vector3(0, 0, 0)) //왕이 공격당했다면
            {

            }
            else if (leftBlue > 8) // 절반이상 죽었다면
            {

            }
            else //아니면 왕은 안움직임
            {
                BlueSelect();
            }
        }
        if(randomChildObject == null)//선택된 대상이 null값일때
        {
            Debug.Log("repick");
            BlueSelect();//다시 고른다
        }
    }
    private void BlueTurn()
    {
        BlueSelect();
        randomChildObject.GetComponent<BlueMovement>().BlueMove();
        CrashObjB = null;
        IsMyTurn = true;
    }

    public void BluePieceSet()
    {
        // "bluePieces" 아래의 모든 자식 GameObjects를 배열에 저장합니다.
        LeftBluePiece = new GameObject[BluePieces.transform.childCount];

        for (int i = 0; i < BluePieces.transform.childCount; i++)
        {
            LeftBluePiece[i] = BluePieces.transform.GetChild(i).gameObject;
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
}
