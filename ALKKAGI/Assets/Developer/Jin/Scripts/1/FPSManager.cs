using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using UnityEngine.UI;

public class FPSManager : Singleton<FPSManager>
{
    private AlKKAGIManager am;

    // �� ����Ʈ
    [SerializeField]
    private List<GameObject> Maps;
    public int mapNum;

    // �÷��̾�, AI�� ���� ��ġ
    private GameObject mySpawnPoint;
    private GameObject enemySpawnPoint;

    public string p;
    public string e;

    public Image ScopeImg;

    private void Awake()
    {
        ShowCursor();
        MapInit();

        if (ScopeImg.gameObject.activeInHierarchy == true)
            ScopeImg.gameObject.SetActive(false);
    }

    private void Update()
    {
        // ���� �׽�Ʈ�� ���콺�� �Ⱥ��̱⿡ ���Ƿ� ����� �� ���ǹ�
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

        // �׽�Ʈ ���� ���� ���� �� �и�
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Map1")
            Init(p, e);
        else
        {
            am = AlKKAGIManager.Instance;
            Init(am.CrashObjR.name, am.CrashObjB.name);
        }
    }

    // �⺻ �ʱ�ȭ �۾�
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
        myPbulPoint.transform.position = myP.transform.position + (Vector3.forward * 2);
        myPbulPoint.name = "bulPos";
        myPbulPoint.transform.SetParent(myP.transform);
        myPbulPoint.transform.SetAsFirstSibling();

        // �÷��̾� ������Ʈ ī�޶� �Ⱥ��̰� ����
        foreach (Transform t in myP.transform)
            t.gameObject.layer = 3;

        myP.transform.GetChild(1).tag = "Player";

        return myP;
    }

    private void EnemyInit(string e, GameObject myP)
    {
        GameObject enemyP = Instantiate(Resources.Load<GameObject>(e), enemySpawnPoint.transform.position, Quaternion.identity);
        enemyP.transform.Rotate(new Vector3(0f, 180f, 0f));

        Enemy_Character ec = enemyP.AddComponent<Enemy_Character>();

        GameObject bulPos = new GameObject();
        bulPos.transform.position = enemyP.transform.position - (Vector3.forward * 2);
        bulPos.name = "bulPos";
        bulPos.tag = enemyP.tag;
        bulPos.transform.SetParent(enemyP.transform);
        bulPos.transform.SetAsFirstSibling();

        // FPS �� AI �߰�
        EnemyAI3 ea = enemyP.AddComponent<EnemyAI3>();
        ea.player = myP.transform;

        enemyP.AddComponent<NavMeshAgent>();
        enemyP.GetComponent<NavMeshAgent>().baseOffset = 1.5f;

        foreach (Transform t in enemyP.transform)
            t.gameObject.layer = 7;

        enemyP.transform.GetChild(1).tag = "Enemy";
    }

    // ���Ƿ� ĳ���� ���� �����ϰ� ���ִ� �Լ�, ��ư�� ����
    public void ChooseCharacter(ref Decorator_Character _d, ref GameObject bullet, GameObject go)
    {
        string str = go.gameObject.name.Split('_')[0];

        if (_d == null)
        {
            switch (str)
            {
                case "Solider":
                    _d = go.gameObject.AddComponent<Solider>();
                    bullet = Resources.Load<GameObject>("Bullets\\Stone");
                    _d.SetStatus(100, 5f, 10);
                    break;
                case "Chariot":
                    _d = go.gameObject.AddComponent<Chariot>();

                    _d.SetStatus(110, 5f, 11);
                    break;
                case "Horse":
                    _d = go.gameObject.AddComponent<Horse>();
                    bullet = Resources.Load<GameObject>("Bullets\\HorseShoe");
                    _d.SetStatus(120, 5f, 12);
                    break;
                case "Elephant":
                    _d = go.gameObject.AddComponent<Elephant>();
                    bullet = Resources.Load<GameObject>("Bullets\\Ivory");
                    _d.SetStatus(130, 5f, 13);
                    break;
                case "Cannon":
                    _d = go.gameObject.AddComponent<Cannon>();
                    bullet = Resources.Load<GameObject>("Bullets\\Dynamite");
                    _d.SetStatus(140, 5f, 14);
                    break;
                case "Guard":
                    _d = go.gameObject.AddComponent<Guard>();
                    bullet = Resources.Load<GameObject>("Bullets\\Book");
                    _d.SetStatus(150, 5f, 15);
                    break;
                case "King":
                    _d = go.gameObject.AddComponent<King>();
                    bullet = Resources.Load<GameObject>("Bullets\\KingBullets");
                    _d.SetStatus(160, 5f, 16);
                    break;
                default:
                    Debug.Log("it does not exist");
                    break;
            }
        }

        if (bullet == null)
            bullet = Resources.Load<GameObject>("Bullets\\Stone");
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

    // ���� ���г���� ������ �Լ�
    public void Win()
    {
        am.BoardObj.SetActive(true);
        am.IsWin = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Board");
        am.FPSResult();

    }

    // ���� ���г���� ������ �Լ�
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

    public void ScopeOnOff()
    {
        if (ScopeImg.gameObject.activeInHierarchy == false)
            ScopeImg.gameObject.SetActive(true);
        else
            ScopeImg.gameObject.SetActive(false);
    }
}