using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlKKAGIManager : MonoBehaviour
{
    private GameObject randomChildObject;
    public GameObject BluePieces;
    public GameObject[] LeftPieces;

    public AudioClip ShootSound;
    public AudioClip CrashSound;
    public AudioClip DeathSound;

    private AudioSource myAudioSource;

    private bool IsWin = false;
    public bool IsMyTurn = false; // true�ϰ��, Red�� // false�ϰ��, Blue��

    public GameObject CrashObjR;
    public GameObject CrashObjB;
    public Vector3 RedPieceLocal = new Vector3(0, 0, 0);
    public Vector3 BluePieceLocal = new Vector3(0, 0, 0);
    public int childCount = 0;

    private void Start()
    {
        IsMyTurn = true;
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Crash()
    {
        //�浹�� ���
        myAudioSource.PlayOneShot(CrashSound);
        //fps �� ��ȯ
        SceneManager.LoadScene("FPS�� / �ӽú�����");

    }

    public void FPSResult()
    {
        if (Random.Range(1, 4) > 2)
            IsWin = false;
        else
            IsWin = true;

        if (IsWin) //�¸�
        {
            Debug.Log("FPS �¸�");
            CrashObjR.GetComponent<PieceMove>().Win();
        }
        else //�й�
        {
            Debug.Log("FPS �й�");
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
        if (childCount > 0)
        {
            randomChildObject = BluePieces.transform.GetChild(Random.Range(1, 17)).gameObject;
        }
        else
        {
            Debug.LogError("No child objects to select.");
        }
    }
    private void BlueTurn()
    {
        BlueSelect();
        randomChildObject.GetComponent<BlueMovement>().BlueMove();
        IsMyTurn = true;
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
