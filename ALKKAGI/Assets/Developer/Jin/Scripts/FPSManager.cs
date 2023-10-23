using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

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
        //EnemyAI2 ea = enemyP.AddComponent<EnemyAI2>();
        //enemyP.AddComponent<NavMeshAgent>();
        //ea.Target = myP.transform;
        
        //GameObject EPbulPoint = new GameObject();
        //EPbulPoint.transform.position = enemyP.transform.position + Vector3.forward;
        //EPbulPoint.name = "bulpos";
        //EPbulPoint.transform.SetParent(enemyP.transform);
        //EPbulPoint.transform.SetAsFirstSibling();
        //ea.firePoint = EPbulPoint.transform;

        //GameObject bullet = Resources.Load<GameObject>("TESTBUL 0");
        //ea.projectilePrefab = bullet;


        GameObject myPbulPoint = new GameObject();
        myPbulPoint.transform.position = myP.transform.position + Vector3.forward;
        myPbulPoint.name = "bulpos";
        myPbulPoint.transform.SetParent(myP.transform);
        myPbulPoint.transform.SetAsFirstSibling();

        enemyP.transform.Rotate(new Vector3(0f, 180f, 0f));

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
