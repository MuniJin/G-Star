using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlKKAGIManager : Singleton<AlKKAGIManager>
{
    private int leftBlue = 0; //�Ķ��� ���� �⹰ ��
    private int BluePattern = 0; //AI ����
    private GameObject[] LeftBluePiece; //�Ķ��� ���� �⹰

    [SerializeField] private GameObject randomChildObject; //���õ� �Ķ��� �⹰
    [SerializeField] private GameObject BluePieces; //�Ķ� �⹰
    [SerializeField] private GameObject RedPieces;  //���� �⹰
    [SerializeField] private GameObject[] LeftPieces; //��� ���� �⹰
    [SerializeField] private GameObject[] LeftRedPiece; //�÷��̾��� ���� �⹰

    public bool IsWin; //FPS ���� üũ
    public bool blueT; //�Ķ� �� üũ
    public bool IsMyTurn; // true�ϰ��, Red�� // false�ϰ��, Blue��
    public bool IsMove; //�̵� üũ
    public bool IsFirstCrash;
    public bool blueCrash;
    public bool RedCrash;

    public GameObject BoardObj;
    public GameObject CrashObjR; //���� �浹�� �⹰
    public GameObject CrashObjB; //�Ķ� �浹�� �⹰
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

    public IEnumerator Crash() //�浹
    {
        timer = 0;

        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().CrashSound);

        audioManager.GetComponent<AudioManager>().PauseBGM();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("cinemachintest");  //fps �� ��ȯ

        BoardObj.SetActive(false);
    }

    public void FPSResult() //FPS����
    {
        audioManager.GetComponent<AudioManager>().ResumeBGM();
        if (IsMyTurn)
        {
            if (IsWin) //�¸�������
            {
                CrashObjR.GetComponent<PieceMove>().Win();
                if (!blueT && !forBlueTurn)
                {
                    CrashObjB = null;
                    CrashObjR = null;
                    StartCoroutine(BlueTurn());
                }
            }
            else //�й�������
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
            if (IsWin) //�¸�������
            {
                //Debug.Log("FPS �¸� - ����");
                CrashObjB.GetComponent<BlueMovement>().RedWin();
                CrashObjB = null;
                CrashObjR = null;
            }
            else //�й�������
            {
                //Debug.Log("FPS �й� - ����");
                CrashObjB.GetComponent<BlueMovement>().Redlose();
                CrashObjB = null;
                CrashObjR = null;
            }
        }
    }
    public IEnumerator NotCrash()
    {
        yield return new WaitForSeconds(1.0f); // 1�� ��ٸ�

        if (!RedCrash)
        {
            CrashObjB = null;
            CrashObjR = null;
            if (!blueT)
                StartCoroutine(BlueTurn());
        }
        else
        {
            Debug.Log("�浹!");
        }
    }

    private void BlueSelect()
    {
        BluePattern = Random.Range(1, 5);
        if (BluePattern == 1) //����1 �޿� ������Ʈ��
        {
            randomChildObject = LeftBluePiece[Random.Range(0, 8)]; //0~7
            repick();
            randomChildObject.GetComponent<BlueMovement>().MoveStart();
        }
        if (BluePattern == 2)//����2 ��
        {
            randomChildObject = LeftBluePiece[Random.Range(9, 11)]; //9~10
            repick();
            randomChildObject.GetComponent<BlueMovement>().MoveStart();
        }
        if (BluePattern == 3)//����3 ����
        {
            randomChildObject = LeftBluePiece[Random.Range(11, 16)]; //11~15
            repick();
            randomChildObject.GetComponent<BlueMovement>().MoveStart();
        }
        if (BluePattern == 4)//����4 ��
        {
            randomChildObject = LeftBluePiece[8]; //8
            if (leftBlue > 8) //���� ���ݴ��߰ų� �����̻� �׾��ٸ�
            {
                randomChildObject.GetComponent<BlueMovement>().MoveStart();
            }
            else //�ƴϸ� ���� �ȿ�����
            {
                BlueSelect();
            }
        }
    }
    public IEnumerator BlueTurn()
    {
        forBlueTurn = true;

        timer = 0f; // Ÿ�̸� �ʱ�ȭ

        while (timer < 2f) // 2�ʸ� ��ٸ�
        {
            if (SceneManager.GetActiveScene().name == "Board" || SceneManager.GetActiveScene().name == "AlkkagiScene")
            {
                timer += Time.deltaTime; // Ÿ�̸� ����
            }
            yield return null; // ���� �����ӱ��� ���
        }
        //Debug.Log("�Ķ������� �Ѿ...");
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
        if (randomChildObject == null)//���õ� ����� null���϶�
        {
            //Debug.Log("repick");
            BlueSelect();//�ٽ� ����
        }
        else if (randomChildObject.transform.localPosition.z > -177f || randomChildObject.transform.localPosition.z < -200f ||
            randomChildObject.transform.localPosition.x > 162f || randomChildObject.transform.localPosition.x < 142f)
        {
            //Debug.Log("repick");
            BlueSelect();//�ٽ� ����
        }
    }

    public void PieceSet()
    {
        // "bluePieces" �Ʒ��� ��� �ڽ� GameObjects�� �迭�� �����մϴ�.
        LeftBluePiece = new GameObject[BluePieces.transform.childCount];

        for (int i = 0; i < BluePieces.transform.childCount; i++)
        {
            LeftBluePiece[i] = BluePieces.transform.GetChild(i).gameObject;
        }

        // "bluePieces" �Ʒ��� ��� �ڽ� GameObjects�� �迭�� �����մϴ�.
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
        //���� ���� ���
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().DeathSound);

        //���� ���� setactive false;
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