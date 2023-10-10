using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : Singleton<FPSManager>
{
    // 플레이어, AI의 스폰 위치
    public GameObject mySpawnPoint;
    public GameObject enemySpawnPoint;

    private void Awake()
    {
        Init(AlKKAGIManager.Instance.CrashObjR, AlKKAGIManager.Instance.CrashObjB);

        ShowCursor();
    }

    private void Update()
    {
        // 각종 테스트때 마우스가 안보이기에 임의로 만들어 둔 조건문
        if (Input.GetKeyDown(KeyCode.R))
            ShowCursor();
    }

    // 기본 초기화 작업
    public void Init(GameObject myPiece, GameObject enemyPiece)
    {
        string str = "TestPrefabs/Red/Cannon_Red";
        
        GameObject myP = Instantiate(Resources.Load<GameObject>(str), mySpawnPoint.transform.position, Quaternion.identity);
        GameObject enemyP = Instantiate(enemyPiece, enemySpawnPoint.transform.position, Quaternion.identity);

        myP.transform.localScale = new Vector3(9f, 9f, 9f);

        myP.AddComponent<Player_Character>();
        // FPS 적 AI 추가
        enemyP.AddComponent<TestEnemyHp>();

        GameObject myPbulPoint = new GameObject();
        myPbulPoint.transform.position = myP.transform.position + Vector3.forward;
        myPbulPoint.name = "bulpos";
        myPbulPoint.transform.SetParent(myP.transform);
        myPbulPoint.transform.SetAsFirstSibling();

        // 플레이어 오브젝트 카메라에 안보이게 설정
        foreach (Transform t in myP.transform)
            t.gameObject.layer = 3;
    }

    // 게임 결과 판정
    public void CheckGameResult(GameObject p, GameObject e)
    {
        Debug.Log(p.GetComponent<Player_Character>()._hp);
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
