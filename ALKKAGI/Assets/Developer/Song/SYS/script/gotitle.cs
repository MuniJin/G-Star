using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotitle : MonoBehaviour
{
    public GameObject BaseObj;
    public GameObject soundOption;

    public void onClickReplayButton()
    {
        Destroy(BaseObj);
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
        GameObject.Find("Alkagisoundctr").GetComponent<SoundManagerForAlkkagi>().DB = GameObject.Find("SoundSource");
    }
    public void SoundOptionTrue()
    {
        soundOption.SetActive(true);
        StartCoroutine(ActivateSoundManager());
    }

    private IEnumerator ActivateSoundManager()
    {
        yield return null; // 다음 프레임을 기다립니다.
        soundOption.GetComponent<SoundManagerForAlkkagi>().DB = GameObject.Find("SoundSource");
        soundOption.GetComponent<SoundManagerForAlkkagi>().BGMSlider.value = PlayerPrefs.GetFloat("BGMVol");
        soundOption.GetComponent<SoundManagerForAlkkagi>().SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
    }
}
