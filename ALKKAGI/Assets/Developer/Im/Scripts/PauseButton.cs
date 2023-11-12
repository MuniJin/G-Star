using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void PushPauseButton() //�����ư
    {
        if (IsTrue == false)
        {
            PauseOption.SetActive(true);
            IsPause = true;
            Time.timeScale = 0;
            IsTrue = true;
            //�ɼ� Ȱ��ȭ,���� �Ͻ�����

        }
        else
        {
            PauseOption.SetActive(false);
            PauseOption2.SetActive(false);
            IsPause = false;
            Time.timeScale = 1;
            IsTrue = false;
            if(SceneManager.GetActiveScene().name =="Map1")
                FPSManager.Instance.ShowCursor();
            //�ɼ� ��Ȱ��ȭ,���� �Ͻ����� ����

        }
    }
}
