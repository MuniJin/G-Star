using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesCh : MonoBehaviour
{
    public void onClickReplayButton()
    {    
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("AlkkagiScene");
    }

}
