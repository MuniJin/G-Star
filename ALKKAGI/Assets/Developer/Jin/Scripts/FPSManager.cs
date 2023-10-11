using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : Singleton<FPSManager>
{
    private AlKKAGIManager ALM;

    // �÷��̾�, AI�� ���� ��ġ
    public GameObject mySpawnPoint;
    public GameObject enemySpawnPoint;

    private void Awake()
    {
        ShowCursor();
        ALM = AlKKAGIManager.Instance;
        Init(ALM.CrashObjR.name, ALM.CrashObjB.name);
    }

    private void Update()
    {
        // ���� �׽�Ʈ�� ���콺�� �Ⱥ��̱⿡ ���Ƿ� ����� �� ���ǹ�
        if (Input.GetKeyDown(KeyCode.R))
            ShowCursor();
    }


    // ���� ���г���� ������ �Լ�
    public void Win()
    {
        ALM.BoardObj.SetActive(true);
        ALM.IsWin = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Board");
        ALM.FPSResult();

    }

    // ���� ���г���� ������ �Լ�
    public void Lose()
    {
        ALM.BoardObj.SetActive(true);
        Cursor.visible = false;
        ALM.IsWin = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Board");
        ALM.FPSResult();
    }

    // �⺻ �ʱ�ȭ �۾�
    public void Init(string myPiece, string enemyPiece)
    {
        string piece = myPiece.Split('_')[0];
        string piece2 = enemyPiece.Split('_')[0];

        string str = $"TestPrefabs/Red/{piece}_Red";
        string str2 = $"TestPrefabs/Blue/{piece}_Blue";

        GameObject myP = Instantiate(Resources.Load<GameObject>(str), mySpawnPoint.transform.position, Quaternion.identity);
        GameObject enemyP = Instantiate(Resources.Load<GameObject>(str2), enemySpawnPoint.transform.position, Quaternion.identity);

        myP.AddComponent<Player_Character>();
        // FPS �� AI �߰�
        enemyP.AddComponent<TestEnemyHp>();

        GameObject myPbulPoint = new GameObject();
        myPbulPoint.transform.position = myP.transform.position + Vector3.forward;
        myPbulPoint.name = "bulpos";
        myPbulPoint.transform.SetParent(myP.transform);
        myPbulPoint.transform.SetAsFirstSibling();

        // �÷��̾� ������Ʈ ī�޶� �Ⱥ��̰� ����
        foreach (Transform t in myP.transform)
            t.gameObject.layer = 3;

        enemyP.transform.GetChild(0).tag = "Enemy";
    }

    // ���� ��� ����
    public void CheckGameResult(GameObject p, GameObject e)
    {
        
    }


    // �׽�Ʈ��, ���콺 ���̱�� ����� ��� �Լ�
    private void ShowCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
