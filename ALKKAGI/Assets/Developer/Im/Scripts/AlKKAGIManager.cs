using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlKKAGIManager : Singleton<AlKKAGIManager>
{
    private int leftBlue = 0; //파랑의 남은 기물 수
    private int BluePattern = 0; //AI 패턴
    private GameObject[] LeftBluePiece; //파랑의 남은 기물

    [SerializeField] private GameObject randomChildObject; //선택된 파랑의 기물
    [SerializeField] private GameObject BluePieces; //파랑 기물
    [SerializeField] private GameObject RedPieces;  //빨강 기물
    [SerializeField] private GameObject[] LeftPieces; //모든 남은 기물
    [SerializeField] private GameObject[] LeftRedPiece; //플레이어의 남은 기물

    public bool IsWin; //FPS 승패 체크
    public bool blueT; //파랑 턴 체크
    public bool IsMyTurn; // true일경우, Red턴 // false일경우, Blue턴
    public bool IsMove; //이동 체크
    public bool IsFirstCrash;
    public bool blueCrash;
    public bool RedCrash;

    public GameObject BoardObj;
    public GameObject CrashObjR; //빨강 충돌한 기물
    public GameObject CrashObjB; //파랑 충돌한 기물
    public GameObject TurnObj;
    [SerializeField] private TMP_Text TurnText;
    [SerializeField] private GameObject GameOverObj;
    public GameObject audioManager; //SoundDataBase
    public AudioSource SFX;
    public bool isGetRedPiecesCoroutineRunning;


    private float timer = 0f;
    private bool forBlueTurn;
    private bool GOver;

    private void Start()
    {
        IsFirstCrash = true;
        IsMyTurn = true;
        IsMove = true;
        audioManager = GameObject.Find("SoundSource");

        SFX = audioManager.GetComponent<AudioManager>().SFXSource;
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().StartSound);
    }

    public IEnumerator Crash() //충돌
    {
        timer = 0;

        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().CrashSound);

        audioManager.GetComponent<AudioManager>().PauseBGM();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("cinemachintest");  //fps 씬 변환

        BoardObj.SetActive(false);
    }

    public void FPSResult() //FPS종료
    {
        audioManager.GetComponent<AudioManager>().ResumeBGM();
        if (IsMyTurn)
        {
            if (IsWin) //승리했을시
            {
                CrashObjR.GetComponent<PieceMove>().Win();
                if (!blueT && !forBlueTurn)
                {
                    CrashObjB = null;
                    CrashObjR = null;
                    StartCoroutine(BlueTurn());
                }
            }
            else //패배했을시
            {
                CrashObjR.GetComponent<PieceMove>().lose();
                if (!blueT && !forBlueTurn)
                {
                    CrashObjB = null;
                    CrashObjR = null;
                    StartCoroutine(BlueTurn());
                }
            }
        }
        if (!IsMyTurn)
        {
            if (IsWin) //승리했을시
            {
                //Debug.Log("FPS 승리 - 적턴");
                CrashObjB.GetComponent<BlueMovement>().RedWin();
                CrashObjB = null;
                CrashObjR = null;
            }
            else //패배했을시
            {
                //Debug.Log("FPS 패배 - 적턴");
                CrashObjB.GetComponent<BlueMovement>().Redlose();
                CrashObjB = null;
                CrashObjR = null;
            }
        }
    }
    public IEnumerator NotCrash()
    {
        yield return new WaitForSeconds(1.0f); // 1초 기다림

        if (!RedCrash)
        {
            CrashObjB = null;
            CrashObjR = null;
            if (!blueT)
                StartCoroutine(BlueTurn());
        }
        else
        {
            Debug.Log("충돌!");
        }
    }

    private void BlueSelect()
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
    public IEnumerator BlueTurn()
    {
        forBlueTurn = true;

        timer = 0f; // 타이머 초기화

        while (timer < 2f) // 2초를 기다림
        {
            if (SceneManager.GetActiveScene().name == "Board" || SceneManager.GetActiveScene().name == "AlkkagiScene")
            {
                timer += Time.deltaTime; // 타이머 증가
            }
            yield return null; // 다음 프레임까지 대기
        }
        //Debug.Log("파랑턴으로 넘어감...");
        StartCoroutine(BlueStart());

    }

    private IEnumerator BlueStart()
    {
        blueCrash = false;
        TurnObj.SetActive(true);
        TurnText.text = "<#6000FF>Blue Turn";
        if (!GOver)
        {
            IsMyTurn = false;
            forBlueTurn = false;
            for (int i = 0; i < LeftBluePiece.Length; i++)
                if (LeftBluePiece[i] != null)
                    LeftBluePiece[i].GetComponent<BlueMovement>().redTurnCrash = false;

            BlueSelect();
            yield return new WaitForSeconds(3f);

            RedTurn();
        }
    }

    private void RedTurn()
    {
        blueT = false;
        IsMove = true;
        RedCrash = false;
    }


    private void repick()
    {
        if (randomChildObject == null)//선택된 대상이 null값일때
        {
            //Debug.Log("repick");
            BlueSelect();//다시 고른다
        }
        else if (randomChildObject.transform.localPosition.z > -177f || randomChildObject.transform.localPosition.z < -200f ||
            randomChildObject.transform.localPosition.x > 162f || randomChildObject.transform.localPosition.x < 142f)
        {
            //Debug.Log("repick");
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
        for (int i = 0; i < LeftRedPiece.Length; i++)
        {
            LeftRedPiece[i].GetComponent<PieceMove>().DragObj.SetActive(true);
        }
    }

    private int a, b, c, d, e, f, g, h, i, j, k, l;
    public void Death(int deathPiece)
    {
        //데스 사운드 재생
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().DeathSound);

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

    public IEnumerator RedTurnChange()
    {
        TurnObj.SetActive(true);
        TurnText.text = "<color=red>My Turn";

        yield return new WaitForSeconds(1f);

        TurnObj.SetActive(false);
    }
    public void GameOver(int who)
    {
        if (who == 0)
        {
            //Blue Is Win
            GOver = true;
            Time.timeScale = 0;
            TurnObj.SetActive(false);
            GameOverObj.SetActive(true);
            GameOverObj.GetComponent<GameOver>().lose();
        }

        if (who == 1)
        {
            //Red Is Win player win
            GOver = true;
            Time.timeScale = 0;
            TurnObj.SetActive(false);
            GameOverObj.SetActive(true);
            GameOverObj.GetComponent<GameOver>().vic();
        }
    }

    public IEnumerator SoundPlay(int soundType) // 1 = shoot  2 = position 3 = button
    {
        if (soundType == 1)
        {
            audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ShootSound);
        }
        if (soundType == 2)
        {
            audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().PositionSound);
        }
        if (soundType == 3)
        {
            audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ButtonSound);
        }
        yield return null;
    }
}