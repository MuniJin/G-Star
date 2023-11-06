using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //����//
    //audioManager.GetComponent<AudioManager>().SFXSource.clip = audioManager.GetComponent<AudioManager>().TestSound;
    //audioManager.GetComponent<AudioManager>().SFXSource.Play();

    //audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().TestSound);
    //GM.GetComponent<AlKKAGIManager>().audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(GM.GetComponent<AlKKAGIManager>().audioManager.GetComponent<AudioManager>().ShootSound);

    public AudioSource bgmSource;
    public AudioSource SFXSource;
    //public AudioSource EnemySource;

    [Header(" ----------- Common Clip -----------")]
    public AudioClip MainBGM;     //���θ޴� bgm O
    public AudioClip ButtonSound; //��ư ȿ����
    public AudioClip TestSound; //ȿ���� ������ �׽�Ʈ ȿ����

    [Header(" ----------- Alkkagi Clip -----------")]
    public AudioClip ShootSound; //�߻� ȿ����
    public AudioClip StartSound; //���� ȿ����
    public AudioClip CrashSound; //�浹 ȿ����
    public AudioClip DeathSound; //��� ȿ���� O
    public AudioClip PositionSound; //��ġ ȿ���� O
    public AudioClip GameOverSound; //end ȿ���� 

    [Header(" ----------- FPS Clip -----------")] //���⼭ �ҷ��� �� 3D ȿ�� ����
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
    {   // BGM �Ͻ� ����
        bgmSource.Pause();
    }

    public void ResumeBGM()
    {   // BGM �ٽ� ���
        bgmSource.UnPause();
    }
}
