using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsescctr : MonoBehaviour
{
    public GameObject PauseOption;
    private bool IsTrue;
    public bool IsPause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PushPauseButton();
        }
    }

    void PushPauseButton()
    {
        if (IsTrue == false)
        {
            PauseOption.SetActive(true);
            IsPause = true;
            IsTrue = true;
            Time.timeScale = 0f;
            FPSManager.Instance.ShowCursor();
            FPSManager.Instance.gs = GameState.Pause;
            //�ɼ� Ȱ��ȭ,���� �Ͻ�����
        }
        else
        {
            PauseOption.SetActive(false);
            IsPause = false;
            IsTrue = false;
            Time.timeScale = 1f;
            FPSManager.Instance.ShowCursor();
            FPSManager.Instance.gs = GameState.Run;
            //�ɼ� ��Ȱ��ȭ,���� �Ͻ����� ����
        }
    }
}
