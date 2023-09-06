using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject DirSys;
    public GameObject PowerSys;
    public GameObject MyGameObj;

    public AudioClip ShootSound;
    public AudioClip CrashSound;

    private AudioSource myAudioSource;

    private bool IsWin = false;
    private bool IsDir = false;
    public bool IsMyTurn = false;

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

        }
        else //패배
        {

        }
    }

    public void Death(int deathPiece)
    {
        //데스 사운드 재생
        //죽은 유닛 setactive false;

    }

    public void GameOver(int who)
    {

    }
}
