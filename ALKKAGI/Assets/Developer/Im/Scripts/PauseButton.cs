using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject PauseOption;
    private bool IsTrue;
    public bool IsPause;

    private void PushPauseButton()
    {
        if (IsTrue == false)
        {
            PauseOption.SetActive(true);
            IsPause = true;
            Time.timeScale = 0;
            IsTrue = true;


        }
        else
        {
            PauseOption.SetActive(false);
            IsPause = false;
            Time.timeScale = 1;
            IsTrue = false;


        }
    }
}
