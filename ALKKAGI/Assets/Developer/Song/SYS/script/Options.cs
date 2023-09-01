using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject OptionsImg;
    public void onClickReplayButton()
    {
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(1.0f);
        OptionsImg.SetActive(true);
    }
}
