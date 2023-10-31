using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class FPSManager : Singleton<FPSManager>
{
    private AlKKAGIManager am;

    // 맵 리스트
    [SerializeField]
    private List<GameObject> Maps;
    public int mapNum;

    // 플레이어, AI의 스폰 위치
    private GameObject mySpawnPoint;
    private GameObject enemySpawnPoint;

    private void Awake()
    {
        ShowCursor();
        MapInit();
    }

    private void Update()
    {
        // 각종 테스트때 마우스가 안보이기에 임의로 만들어 둔 조건문
        if (Input.GetKeyDown(KeyCode.R))
            ShowCursor();
    }

    private void MapInit()
    {
        int rand = 0;
        if (mapNum == 0)
            rand = Random.Range(0, Maps.Count);
        else
            rand = mapNum;

        for (int i = 0; i < Maps.Count; ++i)
        {
            if (i != rand)
                Maps[i].gameObject.SetActive(false);
            if (i == rand)
            {
                mySpawnPoint = Maps[i].transform.GetChild(0).gameObject;
                enemySpawnPoint = Maps[i].transform.GetChild(1).gameObject;
            }
        }

        // 테스트 씬과 메인 게임 씬 분리
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Map1")
            Init("King", "King");
        else
        {
            am = AlKKAGIManager.Instance;
            Init(am.CrashObjR.name, am.CrashObjB.name);
        }
    }

    // 기본 초기화 작업
    public void Init(string myPiece, string enemyPiece)
    {
        string piece = myPiece.Split('_')[0];
        string piece2 = enemyPiece.Split('_')[0];

        string p = $"PiecePrefabs/Red/{piece}_Red";
        string e = $"PiecePrefabs/Blue/{piece2}_Blue";

        EnemyInit(e, PlayerInit(p));
    }

    private GameObject PlayerInit(string p)
    {
        GameObject myP = Instantiate(Resources.Load<GameObject>(p), mySpawnPoint.transform.position, Quaternion.identity);
        myP.AddComponent<Player_Character>();

        GameObject myPbulPoint = new GameObject();
        myPbulPoint.transform.position = myP.transform.position + Vector3.forward;
        myPbulPoint.name = "bulpos";
        myPbulPoint.transform.SetParent(myP.transform);
        myPbulPoint.transform.SetAsFirstSibling();

        // 플레이어 오브젝트 카메라에 안보이게 설정
        foreach (Transform t in myP.transform)
            t.gameObject.layer = 3;

        myP.transform.GetChild(1).tag = "Player";

        return myP;
    }

    private void EnemyInit(string e, GameObject myP)
    {
        GameObject enemyP = Instantiate(Resources.Load<GameObject>(e), enemySpawnPoint.transform.position, Quaternion.identity);
        enemyP.transform.Rotate(new Vector3(0f, 180f, 0f));
        enemyP.AddComponent<Enemy_Character>();
        // FPS 적 AI 추가
        EnemyAI2 ea = enemyP.AddComponent<EnemyAI2>();
        ea.Target = myP.transform;

        enemyP.AddComponent<NavMeshAgent>();
        enemyP.GetComponent<NavMeshAgent>().baseOffset = 1;

        GameObject EPbulPoint = new GameObject();
        EPbulPoint.transform.position = enemyP.transform.position - Vector3.forward;
        EPbulPoint.name = "bulpos";
        EPbulPoint.transform.SetParent(enemyP.transform);
        EPbulPoint.transform.SetAsFirstSibling();
        ea.firePoint = EPbulPoint.transform;

        GameObject bullet = Resources.Load<GameObject>("Bullets\\Stone");
        ea.projectilePrefab = bullet;

        enemyP.transform.GetChild(1).tag = "Enemy";
    }

    // 임의로 캐릭터 선택 가능하게 해주는 함수, 버튼과 연결
    public void ChooseCharacter(ref Default_Character _d, ref GameObject bullet, GameObject go)
    {
        string str = go.gameObject.name.Split('_')[0];

        if (_d == null)
        {
            switch (str)
            {
                case "Solider":
                    _d = go.gameObject.AddComponent<Pawn>();
                    bullet = Resources.Load<GameObject>("Bullets\\Stone");
                    _d.SetStatus(100, 10f, 10);
                    break;
                case "Chariot":
                    _d = go.gameObject.AddComponent<Rook>();
                    bullet = go.gameObject.transform.GetChild(2).GetChild(0).gameObject;
                    _d.SetStatus(100, 1.5f, 30);
                    break;
                case "Horse":
                    _d = go.gameObject.AddComponent<Knight>();
                    _d.SetStatus(100, 10f, 10);
                    break;
                case "Elephant":
                    _d = go.gameObject.AddComponent<Elephant>();
                    _d.SetStatus(100, 10f, 10);
                    break;
                case "Cannon":
                    _d = go.gameObject.AddComponent<Cannon>();
                    _d.SetStatus(150, 3f, 15);
                    break;
                case "Guard":
                    _d = go.gameObject.AddComponent<Guards>();
                    bullet = Resources.Load<GameObject>("Bullets\\Book");
                    _d.SetStatus(100, 10f, 10);
                    break;
                case "King":
                    _d = go.gameObject.AddComponent<King>();
                    _d.SetStatus(100, 10f, 10);
                    break;
                default:
                    Debug.Log("it does not exist");
                    break;
            }
        }

        if(bullet == null)
            bullet = Resources.Load<GameObject>("Bullets\\Stone");
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

    // 게임 승패내기용 임의의 함수
    public void Win()
    {
        am.BoardObj.SetActive(true);
        am.IsWin = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Board");
        am.FPSResult();

    }

    // 게임 승패내기용 임의의 함수
    public void Lose()
    {
        am.BoardObj.SetActive(true);
        Cursor.visible = false;
        am.IsWin = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Board");
        am.FPSResult();
    }
}