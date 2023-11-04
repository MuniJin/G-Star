using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDB : MonoBehaviour
{
    public float BGMDB;
    public float SFXDB;
    public static SoundDB Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSlider()
    {
        GameObject.Find("Alkagisoundctr").GetComponent<SoundManagerForAlkkagi>().BGMSlider.value = BGMDB;
        GameObject.Find("Alkagisoundctr").GetComponent<SoundManagerForAlkkagi>().SFXSlider.value = SFXDB;
    }
}