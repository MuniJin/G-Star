using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject DirSys;
    public GameObject PowerSys;
    public GameObject MyGameObj;
    public GameObject[] LeftPieces;

    public AudioClip ShootSound;
    public AudioClip CrashSound;
    public AudioClip DeathSound;

    private AudioSource myAudioSource;

    private bool IsWin = false;
    private bool IsDir = false;
    public bool IsMyTurn = false;

    private int i = 0;
    public GameObject CrashObjR;
    public GameObject CrashObjB;
    public Vector3 RedPieceLocal = new Vector3(0, 0, 0);
    public Vector3 BluePieceLocal = new Vector3(0, 0, 0);

    private void Start()
    {
        IsMyTurn = true;
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Crash()
    {
        //충돌음 재생
        myAudioSource.PlayOneShot(CrashSound);

        
    }

    private void FPSResult()
    {
        if (IsWin) //승리
        {
            CrashObjB.GetComponent<BlueMovement>();
        }
        else //패배
        {
            CrashObjR.GetComponent<PieceMove>().lose();
        }
    }

    public void Death(int deathPiece)
    {
        //데스 사운드 재생
        //죽은 유닛 setactive false;
        myAudioSource.PlayOneShot(DeathSound);
        if(deathPiece == 1) //red Sol
        {
            LeftPieces[i].SetActive(false);
            i++;
        }
        if (deathPiece == 2)
        {
            LeftPieces[5].SetActive(false);
        }
        if (deathPiece == 3)
        {
            LeftPieces[7].SetActive(false);
        }
        if (deathPiece == 4)
        {
            LeftPieces[9].SetActive(false);
        }
        if (deathPiece == 5)
        {
            LeftPieces[11].SetActive(false);

        }
        if (deathPiece == 6)
        {
            LeftPieces[13].SetActive(false);

        }
        if (deathPiece == 7)
        {
            LeftPieces[15].SetActive(false);

        }
        if (deathPiece == 8)
        {
            LeftPieces[20].SetActive(false);

        }
        if (deathPiece == 9)
        {
            LeftPieces[22].SetActive(false);

        }
        if (deathPiece == 10)
        {
            LeftPieces[24].SetActive(false);

        }
        if (deathPiece == 11)
        {
            LeftPieces[26].SetActive(false);

        }
        if (deathPiece == 12)
        {
            LeftPieces[28].SetActive(false);

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
