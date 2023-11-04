using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //사용법//
    //audioManager.GetComponent<AudioManager>().SFXSource.clip = audioManager.GetComponent<AudioManager>().TestSound;
    //audioManager.GetComponent<AudioManager>().SFXSource.Play();

    public AudioSource bgmSource;
    public AudioSource SFXSource;

    [Header(" ----------- Common Clip -----------")]
    public AudioClip MainBGM;     //메인메뉴 bgm
    public AudioClip ButtonSound; //버튼 효과음
    public AudioClip TestSound; //효과음 조절시 테스트 효과음

    [Header(" ----------- Alkkagi Clip -----------")]
    public AudioClip ShootSound; //발사 효과음
    public AudioClip StartSound; //시작 효과음
    public AudioClip CrashSound; //충돌 효과음
    public AudioClip DeathSound; //사망 효과음 O
    public AudioClip PositionSound; //배치 효과음 O
    public AudioClip GameOverSound; //end 효과음 

    [Header(" ----------- FPS Clip -----------")] //여기서 불러올 시 3D 효과 없음
    public AudioClip FPSBGM; //FPS bgm

    public AudioClip[] Skills;
    public AudioClip HitSound;
    public AudioClip BulletSound;

}
