using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerForAlkkagi : MonoBehaviour
{
    //�������
    //
    //ȿ����
    //�浹�� ȿ����
    //�߻�� ȿ��������� ȿ����
    //���� ����� ȿ����
    //��ư Ŭ������ ȿ����
    //ȿ���� �������� �׽�Ʈ ȿ����
    //���� ��ġ ȿ����

    public AudioClip ShootSound;
    public AudioClip CrashSound;
    public AudioClip DeathSound;
    public AudioClip ButtonSound;
    public AudioClip PositionSound;
    public AudioClip TestSound;
    public AudioClip BGM;

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,  // �ƹ��͵� �ƴ�. �׳� Sound enum�� ���� ���� ���� �߰�. (0, 1, '2' �̷��� 2��) 
    }

    AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Clear()
    {
        // ����� ���� ��� ��ž, ���� ����
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // ȿ���� Dictionary ����
        _audioClips.Clear();
    }
}
