using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject PauseOption;
    private bool IsTrue;

    private void PushPauseButton()
    {
        if (IsTrue == false)
        {
            PauseOption.SetActive(true);
            IsTrue = true;
        }
        else
        {
            PauseOption.SetActive(false);
            IsTrue = false;
        }
    }
}
