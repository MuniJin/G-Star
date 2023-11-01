using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //����//
    //DB.GetComponent<AudioManager>().SFXSource.clip = DB.GetComponent<AudioManager>().TestSound;
    //DB.GetComponent<AudioManager>().SFXSource.Play();
     
    public AudioSource bgmSource;
    public AudioSource SFXSource;

    [Header(" ----------- Common Clip -----------")]
    public AudioClip MainBGM;     //���θ޴� bgm
    public AudioClip ButtonSound; //��ư ȿ����
    public AudioClip TestSound; //ȿ���� ������ �׽�Ʈ ȿ����

    [Header(" ----------- Alkkagi Clip -----------")]
    public AudioClip ShootSound; //�߻� ȿ����
    public AudioClip CrashSound; //�浹 ȿ����
    public AudioClip DeathSound; //��� ȿ����
    public AudioClip PositionSound; //��ġ ȿ����

    [Header(" ----------- FPS Clip -----------")]
    public AudioClip FPSBGM; //FPS bgm
}
