using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotitle : MonoBehaviour
{
    public GameObject BaseObj;
    public void onClickReplayButton()
    {
        Destroy(BaseObj);
        SceneManager.LoadScene("Title");
    }
  
}
