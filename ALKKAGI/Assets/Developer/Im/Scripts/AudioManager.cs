using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //사용법//
    //DB.GetComponent<AudioManager>().SFXSource.clip = DB.GetComponent<AudioManager>().TestSound;
    //DB.GetComponent<AudioManager>().SFXSource.Play();
     
    public AudioSource bgmSource;
    public AudioSource SFXSource;

    [Header(" ----------- Common Clip -----------")]
    public AudioClip MainBGM;     //메인메뉴 bgm
    public AudioClip ButtonSound; //버튼 효과음
    public AudioClip TestSound; //효과음 조절시 테스트 효과음

    [Header(" ----------- Alkkagi Clip -----------")]
    public AudioClip ShootSound; //발사 효과음
    public AudioClip CrashSound; //충돌 효과음
    public AudioClip DeathSound; //사망 효과음
    public AudioClip PositionSound; //배치 효과음

    [Header(" ----------- FPS Clip -----------")]
    public AudioClip FPSBGM; //FPS bgm
}
