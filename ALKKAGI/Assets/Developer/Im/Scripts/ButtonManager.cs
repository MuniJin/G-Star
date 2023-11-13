using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject audioManager;
    [SerializeField] private GameObject help;
    [SerializeField] private GameObject soundctr;
    [SerializeField] private GameObject OptionsImg;
    [SerializeField] private GameObject SkillExplain;
    [SerializeField] private Animator   startani;
    

    private void Update()
    {
        if (audioManager == null)
            audioManager = GameObject.Find("SoundSource");
    }

    public void HelpButton()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ButtonSound);

        help.gameObject.SetActive(true);
    }
    public void SkillButton()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ButtonSound);

        SkillExplain.gameObject.SetActive(true);
    }
    public void SoundButton()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ButtonSound);
        soundctr.GetComponent<SoundManagerForAlkkagi>().DB = audioManager;
        soundctr.SetActive(true);
    }
    public void OptionButton()
    {
        StartCoroutine(loadOption());
    }

    IEnumerator loadOption()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ButtonSound);
        yield return new WaitForSeconds(1.0f);
        OptionsImg.SetActive(true);
    }

    public void BackButton()
    {
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ButtonSound);

        clickedButton.transform.parent.gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ButtonSound);

        StartCoroutine(CORQuitButton());
    }
    IEnumerator CORQuitButton()
    {
        yield return new WaitForSeconds(1.0f);
        Application.Quit();
        Debug.Log("Quit");
    }

    public void StartButton()
    {
        StartCoroutine(loadScene());
    }
    IEnumerator loadScene()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ButtonSound);
        startani.SetTrigger("New Trigger1");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("AlkkagiScene");
    }

    public void TempButton()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(audioManager.GetComponent<AudioManager>().ButtonSound);

    }
}
