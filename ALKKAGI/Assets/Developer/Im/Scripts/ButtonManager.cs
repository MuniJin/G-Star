using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject audioManager;
    [SerializeField] private GameObject helpButton;
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject backButton;


    public void HelpButton()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.clip = audioManager.GetComponent<AudioManager>().TestSound;
        audioManager.GetComponent<AudioManager>().SFXSource.Play();
        if (!helpButton.activeSelf)
        {
            helpButton.SetActive(true);
        }
        else
        {
            helpButton.SetActive(false);
        }
    }
    public void SoundButton()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.clip = audioManager.GetComponent<AudioManager>().TestSound;
        audioManager.GetComponent<AudioManager>().SFXSource.Play();
        if (!soundButton.activeSelf)
        {
            soundButton.SetActive(true);
        }
        else
        {
            soundButton.SetActive(false);
        }
    }
    public void BackButton()
    {
        audioManager.GetComponent<AudioManager>().SFXSource.clip = audioManager.GetComponent<AudioManager>().TestSound;
        audioManager.GetComponent<AudioManager>().SFXSource.Play();
        if (!backButton.activeSelf)
        {
            backButton.SetActive(true);
        }
        else
        {
            backButton.SetActive(false);
        }
    }
}
