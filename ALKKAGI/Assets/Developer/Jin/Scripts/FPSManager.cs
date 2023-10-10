using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : Singleton<FPSManager>
{
    // �÷��̾�, AI�� ���� ��ġ
    public GameObject mySpawnPoint;
    public GameObject enemySpawnPoint;

    private void Awake()
    {
        Init(AlKKAGIManager.Instance.CrashObjR, AlKKAGIManager.Instance.CrashObjB);

        ShowCursor();
    }

    private void Update()
    {
        // ���� �׽�Ʈ�� ���콺�� �Ⱥ��̱⿡ ���Ƿ� ����� �� ���ǹ�
        if (Input.GetKeyDown(KeyCode.R))
            ShowCursor();
    }

    // �⺻ �ʱ�ȭ �۾�
    public void Init(GameObject myPiece, GameObject enemyPiece)
    {
        string str = "TestPrefabs/Red/Cannon_Red";
        
        GameObject myP = Instantiate(Resources.Load<GameObject>(str), mySpawnPoint.transform.position, Quaternion.identity);
        GameObject enemyP = Instantiate(enemyPiece, enemySpawnPoint.transform.position, Quaternion.identity);

        myP.transform.localScale = new Vector3(9f, 9f, 9f);

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
    }

    // ���� ��� ����
    public void CheckGameResult(GameObject p, GameObject e)
    {
        Debug.Log(p.GetComponent<Player_Character>()._hp);
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
