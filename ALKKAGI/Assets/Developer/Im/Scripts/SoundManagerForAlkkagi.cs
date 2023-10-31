using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerForAlkkagi : MonoBehaviour
{
    //배경음악
    //
    //효과음
    //충돌시 효과음
    //발사시 효과음사망시 효과음
    //게임 종료시 효과음
    //버튼 클릭시의 효과음
    //효과음 조절시의 테스트 효과음
    //장기알 배치 효과음

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
        MaxCount,  // 아무것도 아님. 그냥 Sound enum의 개수 세기 위해 추가. (0, 1, '2' 이렇게 2개) 
    }

    AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Clear()
    {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 Dictionary 비우기
        _audioClips.Clear();
    }
}
