using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject PauseOption;
    public GameObject PauseOption2;
    private bool IsTrue;
    public bool IsPause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PushPauseButton();
        }
    }
    private void PushPauseButton() //퍼즈버튼
    {
        if (IsTrue == false)
        {
            PauseOption.SetActive(true);
            IsPause = true;
            Time.timeScale = 0;
            IsTrue = true;
            //옵션 활성화,게임 일시정지

        }
        else
        {
            PauseOption.SetActive(false);
            PauseOption2.SetActive(false);
            IsPause = false;
            Time.timeScale = 1;
            IsTrue = false;
            //옵션 비활성화,게임 일시정지 해제

        }
    }
}
