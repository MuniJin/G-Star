using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //����//
    //audioManager.GetComponent<AudioManager>().SFXSource.clip = audioManager.GetComponent<AudioManager>().TestSound;
    //audioManager.GetComponent<AudioManager>().SFXSource.Play();

    public AudioSource bgmSource;
    public AudioSource SFXSource;

    [Header(" ----------- Common Clip -----------")]
    public AudioClip MainBGM;     //���θ޴� bgm
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
    public AudioClip HitSound;
    public AudioClip BulletSound;

}
