using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class FPSManager : Singleton<FPSManager>
{
    private AlKKAGIManager ALM;

    // 플레이어, AI의 스폰 위치
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
        // 각종 테스트때 마우스가 안보이기에 임의로 만들어 둔 조건문
        if (Input.GetKeyDown(KeyCode.R))
            ShowCursor();
    }


    // 게임 승패내기용 임의의 함수
    public void Win()
    {
        ALM.BoardObj.SetActive(true);
        ALM.IsWin = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Board");
        ALM.FPSResult();

    }

    // 게임 승패내기용 임의의 함수
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

    // 기본 초기화 작업
    public void Init(string myPiece, string enemyPiece)
    {
        string piece = myPiece.Split('_')[0];
        string piece2 = enemyPiece.Split('_')[0];

        string str = $"TestPrefabs/Red/{piece}_Red";
        string str2 = $"TestPrefabs/Blue/{piece}_Blue";

        GameObject myP = Instantiate(Resources.Load<GameObject>(str), mySpawnPoint.transform.position, Quaternion.identity);
        GameObject enemyP = Instantiate(Resources.Load<GameObject>(str2), enemySpawnPoint.transform.position, Quaternion.identity);

        myP.AddComponent<Player_Character>();
        // FPS 적 AI 추가
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

        // 플레이어 오브젝트 카메라에 안보이게 설정
        foreach (Transform t in myP.transform)
            t.gameObject.layer = 3;

        enemyP.transform.GetChild(0).tag = "Enemy";
    }

    // 게임 결과 판정
    public void CheckGameResult(GameObject p, GameObject e)
    {
        
    }


    // 테스트용, 마우스 보이기와 숨기기 기능 함수
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
