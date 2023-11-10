using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundFps : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    public Slider SFXSlider;
    public GameObject DB;

    private void Start()
    {
        DB = GameObject.Find("SoundSource");

        this.gameObject.SetActive(false);
        LoadVol();
        SetSFX();
    }

    public void SetSFX()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVol", volume);
        DB.GetComponent<SoundDB>().SFXDB = SFXSlider.value;

        DB.GetComponent<AudioManager>().SFXSource.PlayOneShot(DB.GetComponent<AudioManager>().TestSound);
    }

    private void LoadVol()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        SetSFX();
    }
    public void SoundOptionTrue()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(ActivateSoundManager());
    }

    private IEnumerator ActivateSoundManager()
    {
        yield return null; // 다음 프레임을 기다립니다.
        DB = GameObject.Find("SoundSource");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
    }

    public void BackButton()
    {
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        clickedButton.transform.parent.gameObject.SetActive(false);
    }
}
