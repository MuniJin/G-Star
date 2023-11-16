using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using UnityEngine.UI;


public enum GameState
{
    Run,
    Pause
}

public class FPSManager : Singleton<FPSManager>
{
    private AlKKAGIManager am;
    private AudioManager aum;
    // 맵 리스트
    [SerializeField]
    private List<GameObject> Maps;
    public int mapNum;

    // 플레이어, AI의 스폰 위치
    private GameObject mySpawnPoint;
    private GameObject enemySpawnPoint;

    public string p;
    public string e;

    public Image ScopeImg;

    public GameState gs;

    public GameObject Tabui;
    public GameObject[] panners;
    [SerializeField] private GameObject Help;

    private void Awake()
    {
        aum = AudioManager.Instance;

        ShowCursor();
        MapInit();

        if (ScopeImg.gameObject.activeInHierarchy == true)
            ScopeImg.gameObject.SetActive(false);

        gs = GameState.Run;
    }

    private void Update()
    {
        // 각종 테스트때 마우스가 안보이기에 임의로 만들어 둔 조건문
        //if (Input.GetKeyDown(KeyCode.R))
        //    ShowCursor();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            Tabui.gameObject.SetActive(true);
            Help.gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.F1))
        {
            Tabui.gameObject.SetActive(false);
        }

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

        am = AlKKAGIManager.Instance;
        Init(am.CrashObjR.name, am.CrashObjB.name);
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
        myPbulPoint.transform.position = myP.transform.position + (Vector3.forward * 2);
        myPbulPoint.name = "bulPos";
        myPbulPoint.transform.SetParent(myP.transform);
        myPbulPoint.transform.SetAsFirstSibling();

        // 플레이어 오브젝트 카메라에 안보이게 설정
        foreach (Transform t in myP.transform)
            t.gameObject.layer = 3;

        myP.transform.GetChild(1).tag = "Player";

        pBulAS = myP.AddComponent<AudioSource>();

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

        // FPS 적 AI 추가
        EnemyAI3 ea = enemyP.AddComponent<EnemyAI3>();
        ea.player = myP.transform;

        enemyP.AddComponent<NavMeshAgent>();
        enemyP.GetComponent<NavMeshAgent>().baseOffset = 5f;
        enemyP.GetComponent<NavMeshAgent>().stoppingDistance = 30f;
        enemyP.GetComponent<NavMeshAgent>().speed = 8f;

        foreach (Transform t in enemyP.transform)
            t.gameObject.layer = 7;

        enemyP.transform.GetChild(1).tag = "Enemy";

        eBulAS = enemyP.AddComponent<AudioSource>();
    }

    

    // 임의로 캐릭터 선택 가능하게 해주는 함수, 버튼과 연결
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
                    if (go.tag == "Player")
                    {
                        PHPCTR.Instance.SetHpUI(100);
                        PHPCTR.Instance.skillImg.sprite = Resources.Load<Sprite>("Skillimg\\Solider");
                        panners[0].SetActive(true);
                    }
                    SelectBulSound(go.tag, aum.BulletSound[0], aum.BulletSound[5]);
                    break;
                case "Guard":
                    _d = go.gameObject.AddComponent<Guard>();
                    bullet = Resources.Load<GameObject>("Bullets\\Book");
                    _d.SetStatus(110, 6f, 15);
                    if (go.tag == "Player")
                    {
                        PHPCTR.Instance.SetHpUI(105);
                        PHPCTR.Instance.skillImg.sprite = Resources.Load<Sprite>("Skillimg\\Guard");
                        panners[1].SetActive(true);
                    }
                    SelectBulSound(go.tag, aum.BulletSound[1], aum.BulletSound[1]);
                    break;
                case "Elephant":
                    _d = go.gameObject.AddComponent<Elephant>();
                    bullet = Resources.Load<GameObject>("Bullets\\Ivory");
                    _d.SetStatus(130, 7f, 13);
                    if (go.tag == "Player")
                    {
                        PHPCTR.Instance.SetHpUI(130);
                        PHPCTR.Instance.skillImg.sprite = Resources.Load<Sprite>("Skillimg\\Elephant");
                        panners[4].SetActive(true);
                    }
                    SelectBulSound(go.tag, aum.BulletSound[2], aum.BulletSound[2]);
                    break;
                case "Chariot":
                    _d = go.gameObject.AddComponent<Chariot>();
                    _d.SetStatus(110, 8f, 11);
                    if (go.tag == "Player")
                    {
                        PHPCTR.Instance.SetHpUI(110);
                        PHPCTR.Instance.skillImg.sprite = Resources.Load<Sprite>("Skillimg\\Chariot");
                        panners[3].SetActive(true);
                    }
                    SelectBulSound(go.tag, aum.BulletSound[3], aum.BulletSound[3]);
                    break;
                case "Horse":
                    _d = go.gameObject.AddComponent<Horse>();
                    bullet = Resources.Load<GameObject>("Bullets\\HorseShoe");
                    _d.SetStatus(120, 9f, 12);
                    if (go.tag == "Player")
                    {
                        PHPCTR.Instance.SetHpUI(120);
                        PHPCTR.Instance.skillImg.sprite = Resources.Load<Sprite>("Skillimg\\Horse");
                        panners[5].SetActive(true);
                    }
                    SelectBulSound(go.tag, aum.BulletSound[4], aum.BulletSound[4]);
                    break;
                case "Cannon":
                    _d = go.gameObject.AddComponent<Cannon>();
                    bullet = Resources.Load<GameObject>("Bullets\\Dynamite");
                    _d.SetStatus(140, 10f, 14);
                    if (go.tag == "Player")
                    {
                        PHPCTR.Instance.SetHpUI(140);
                        PHPCTR.Instance.skillImg.sprite = Resources.Load<Sprite>("Skillimg\\Cannon");
                        panners[2].SetActive(true);
                    }
                    SelectBulSound(go.tag, aum.BulletSound[5], aum.BulletSound[5]);
                    break;
                case "King":
                    _d = go.gameObject.AddComponent<King>();
                    bullet = Resources.Load<GameObject>("Bullets\\KingBullets");
                    _d.SetStatus(160, 11f, 16);
                    if (go.tag == "Player")
                    {
                        PHPCTR.Instance.SetHpUI(160);
                        PHPCTR.Instance.skillImg.sprite = Resources.Load<Sprite>("Skillimg\\King");
                        panners[6].SetActive(true);
                    }
                    SelectBulSound(go.tag, aum.BulletSound[6], aum.BulletSound[6]);
                    break;
                default:
                    Debug.Log("it does not exist");
                    break;
            }
        }

        if (bullet == null)
            bullet = Resources.Load<GameObject>("Bullets\\Stone");
    }

    // 테스트용, 마우스 보이기와 숨기기 기능 함수
    public void ShowCursor()
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

    public void ScopeOnOff()
    {
        if (ScopeImg.gameObject.activeInHierarchy == false)
            ScopeImg.gameObject.SetActive(true);
        else
            ScopeImg.gameObject.SetActive(false);
    }

    [SerializeField] private AudioClip pBul, eBul;
    [SerializeField] private AudioClip pSkill, eSkill;
    [SerializeField] private AudioSource pBulAS, eBulAS;
    
    private void SelectBulSound(string tag, AudioClip ac, AudioClip sk)
    {
        if (tag == "Player")
        {
            pBul = ac;
            pSkill = sk;
            pBulAS.clip = pBul;
            pBulAS.volume = 0.3f;
        }
        if (tag == "Enemy")
        {
            eBul = ac;
            eSkill = sk;
            eBulAS.clip = eBul;
            eBulAS.volume = 0.3f;
        }
    }

    public void BulletSoundPlay(string tag)
    {
        if (tag == "Player")
        {
            pBulAS.clip = pBul;
            pBulAS.PlayOneShot(pBul);
        }
        if (tag == "Enemy")
        {
            eBulAS.clip = eBul;
            eBulAS.PlayOneShot(eBul);
        }
    }

    public void SkillSoundPlay(string tag)
    {
        if (tag == "Player")
        {
            pBulAS.clip = pSkill;
            pBulAS.PlayOneShot(pBul);
        }
        if (tag == "Enemy")
        {
            eBulAS.clip = eSkill;
            eBulAS.PlayOneShot(eBul);
        }
    }
}