using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManagerForAlkkagi : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public GameObject DB;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Title")
        {
            this.gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("BGMVol"))
        {
            Invoke("LoadVol", 0.3f);
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            Invoke("LoadVol", 0.3f);
        }
        else
        {
            SetBGM();
            SetSFX();
        }
    }
    public void SetBGM()
    {
        float volume = BGMSlider.value;
        myMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVol", volume);
        DB.GetComponent<SoundDB>().BGMDB = BGMSlider.value;
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
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVol");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        SetBGM();
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
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVol");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
    }
}