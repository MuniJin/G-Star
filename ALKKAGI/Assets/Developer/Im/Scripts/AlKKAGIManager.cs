using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlKKAGIManager : Singleton<AlKKAGIManager>
{
    private int leftBlue = 0; //�Ķ��� ���� �⹰ ��
    private int BluePattern = 0; //AI ����
    private GameObject[] LeftBluePiece; //�Ķ��� ���� �⹰
    private AudioSource myAudioSource;

    public AudioClip ShootSound;
    public AudioClip CrashSound;
    public AudioClip DeathSound;

    public GameObject randomChildObject; //���õ� �Ķ��� �⹰
    public GameObject BluePieces; //�Ķ� �⹰
    public GameObject RedPieces;  //���� �⹰
    public GameObject[] LeftPieces; //��� ���� �⹰
    public GameObject[] LeftRedPiece; //�÷��̾��� ���� �⹰

    public bool IsWin; //FPS ���� üũ
    public bool blueT; //�Ķ� �� üũ
    public bool IsMyTurn; // true�ϰ��, Red�� // false�ϰ��, Blue��
    public bool IsMove; //�̵� üũ
    public bool IsFirstCrash;
    public int CheatMode; //�׽�Ʈ�� ġƮ���

    public GameObject BoardObj;
    public GameObject CrashObjR; //���� �浹�� �⹰
    public GameObject CrashObjB; //�Ķ� �浹�� �⹰

    private void Start()
    {
        IsFirstCrash = true;
        IsMyTurn = true;
        IsMove = true;
        myAudioSource = GetComponent<AudioSource>();
    }

    public void Crash() //�浹
    {
        myAudioSource.PlayOneShot(CrashSound); //�浹�� ���

        SceneManager.LoadScene("cinemachintest");  //fps �� ��ȯ


        BoardObj.SetActive(false);
    }

    public void FPSResults()
    {
        Invoke("FPSResult", 1f);
    }

    public void FPSResult() //FPS����
    {

        if (IsMyTurn)
        {
            if (IsWin) //�¸�������
            {
                Debug.Log("FPS �¸�");
                CrashObjR.GetComponent<PieceMove>().Win();
            }
            else //�й�������
            {
                Debug.Log("FPS �й�");
                CrashObjR.GetComponent<PieceMove>().lose();
            }
        }
        if (!IsMyTurn)
        {
            if (IsWin) //�¸�������
            {
                Debug.Log("FPS �¸� - ����");
                CrashObjB.GetComponent<BlueMovement>().RedWin();
            }
            else //�й�������
            {
                Debug.Log("FPS �й� - ����");
                CrashObjB.GetComponent<BlueMovement>().Redlose();
            }
        }
    }

    public void BlueSelect()
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

    public void BlueTurn()
    {
        Invoke("BlueStart", 1f);
    }
    private void BlueStart()
    {
        IsMyTurn = false;
        Debug.Log("�Ķ��� ����");
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
        if (randomChildObject == null)//���õ� ����� null���϶�
        {
            Debug.Log("repick");
            BlueSelect();//�ٽ� ����
        }
        else if (randomChildObject.transform.localPosition.z > -177f || randomChildObject.transform.localPosition.z < -200f | randomChildObject.transform.localPosition.x > 162f || randomChildObject.transform.localPosition.x < 142f)
        {
            Debug.Log("repick");
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
    }

    private int a, b, c, d, e, f, g, h, i, j, k, l;
    public void Death(int deathPiece)
    {
        //���� ���� ���
        myAudioSource.PlayOneShot(DeathSound);

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
