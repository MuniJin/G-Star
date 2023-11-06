using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //사용법//
    //audioManager.GetComponent<AudioManager>().SFXSource.clip = audioManager.GetComponent<AudioManager>().TestSound;
    //audioManager.GetComponent<AudioManager>().SFXSource.Play();

    //audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().TestSound);
    //GM.GetComponent<AlKKAGIManager>().audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(GM.GetComponent<AlKKAGIManager>().audioManager.GetComponent<AudioManager>().ShootSound);

    public AudioSource bgmSource;
    public AudioSource SFXSource;
    //public AudioSource EnemySource;

    [Header(" ----------- Common Clip -----------")]
    public AudioClip MainBGM;     //메인메뉴 bgm O
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
    public AudioClip[] BulletSound;
    public AudioClip HitSound;

    private void Start()
    {
        bgmSource.clip = MainBGM;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    //private void Update()
    //{
    //    if(SceneManager.GetActiveScene().name == "Map1")
    //    {

    //    }
    //}

    public void PauseBGM()
    {   // BGM 일시 정지
        bgmSource.Pause();
    }

    public void ResumeBGM()
    {   // BGM 다시 재생
        bgmSource.UnPause();
    }
}
