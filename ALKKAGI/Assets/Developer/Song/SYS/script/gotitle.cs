using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotitle : MonoBehaviour
{
    public void onClickReplayButton()
    {
        SceneManager.LoadScene("Title");
    }
  
}
