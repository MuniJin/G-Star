using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player_Character : Default_Character
{
    // 알까기 매니저는 추후 싱글톤으로 변경할 예정
    public GameObject ALM;
    // Default_Character를 상속받은 각각의 캐릭터(쫄, 상, 포, 마...)의 특성을 입힐 수 있게 선언
    private Default_Character _d;
    // 물리 계산을 위해 선언
    private Rigidbody rb;
    // 총알, 플레이어 오브젝트, 왕 스킬(추후 다른방식으로 프리팹 불러와서 사용할 예정)
    private GameObject bullet;
    //public GameObject playerObj;
    //public GameObject kingSkill;

    // 속도, 점프 힘
    public float speed = 5f;
    public float jumpForce = 5f;

    // 총구 위치
    public GameObject bulPos;

    private void Start()
    {
        ALM = GameObject.Find("AlKKAGIManager");
        cam = Camera.main;
        bulPos = this.gameObject.transform.GetChild(0).gameObject;
        ChooseCharacter();

        // 플레이어 초기 체력 세팅
        this._hp = 100;

        // 플레이어 오브젝트와 rigidbody 받아오기
        rb = this.gameObject.GetComponent<Rigidbody>();
        
        // 플레이어 태그 변경
        this.gameObject.tag = "Player";
        
        // 총알 Resources폴더에서 불러와서 사용(추후 변경 예정, 성능 문제, 대안 : AssetBundle)
        bullet = Resources.Load<GameObject>("TESTBUL 0");
        if (bullet.GetComponent<Bullet>() == false)
            bullet.AddComponent<Bullet>();
    }

    private void Update()
    {
        Debug.Log(_d.name);
        // 점프, velocity가 없을때 점프 가능하게
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
            Jump();
        // 총구 위치에서 총알 발사
        if (Input.GetMouseButtonDown(0))
            Attack(bulPos.transform.position, 40f);
        // 스킬 사용
        if (Input.GetKeyDown(KeyCode.Q))
            UseSkill();
        
        // AI 완성 전까지 게임 승패내기용으로 임의로 만들어 둔 조건문
        if (Input.GetKeyDown(KeyCode.O))
            Win();
        if (Input.GetKeyDown(KeyCode.P))
            Lose();
    }

    // 게임 승패내기용 임의의 함수
    public void Win()
    {
        ALM.GetComponent<AlKKAGIManager>().BoardObj.SetActive(true);
        ALM.GetComponent<AlKKAGIManager>().IsWin = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("Board"); 
        ALM.GetComponent<AlKKAGIManager>().FPSResult();

    }

    // 게임 승패내기용 임의의 함수
    public void Lose()
    {
        ALM.GetComponent<AlKKAGIManager>().BoardObj.SetActive(true);
        Cursor.visible = false;
        ALM.GetComponent<AlKKAGIManager>().IsWin = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("Board");  
        ALM.GetComponent<AlKKAGIManager>().FPSResult();
    }

    private void FixedUpdate()
    {
        // 플레이어 움직임
        Move();
        // 마우스 움직임에 따른 카메라 회전값 변경
        CamMove();
    }

    float h, v;

    protected override void Move()
    {
        CamMove();
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.forward * v + transform.right * h;

        this.transform.position += dir * speed * Time.deltaTime;
    }

    private float mouseX, mouseY;
    private float eulerAngleX, eulerAngleY;

    private float rotCamX = 5f;
    private float rotCamY = 3f;

    private float limitMinX = -40f;
    private float limitMaxX = 40f;

    private Camera cam;

    public float sensitivity = 2f;

    private void CamMove()
    {
        cam.transform.position = this.gameObject.transform.position;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        eulerAngleX -= mouseY * sensitivity;
        eulerAngleY += mouseX * sensitivity;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        this.transform.rotation = Quaternion.Euler(0f, eulerAngleY, 0f);
        cam.transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0f);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }

    // 점프
    protected override void Jump() => rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    
    // 공격
    public override void Attack(Vector3 bulpos, float shootPower)
    {
        GameObject go = Instantiate(bullet, bulpos, new Quaternion(-90f, 0f, 0f, 0f));
        
        go.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * shootPower, ForceMode.Impulse);
    }

    // 스킬(Default_Character를 상속받아서 존재하나 필요없어서 예외처리로 해둠)
    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }

    // 스킬 사용
    private void UseSkill()
    {
        if (_d != null)
            StartCoroutine(_d.Skill(this.gameObject));
        else
            Debug.Log("Not Decorator");
    }

    // 임의로 캐릭터 선택 가능하게 해주는 함수, 버튼과 연결
    public void ChooseCharacter()
    {
        //GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        //string str = clickedBtn.name;

        string str = this.gameObject.name.Split('_')[0];

        if (_d == null)
        {
            Debug.Log("Select " + str);

            switch (str)
            {
                case "Solider":
                    _d = this.gameObject.AddComponent<Pawn>();
                    bullet = Resources.Load<GameObject>("TESTBUL 1");
                    break;
                case "Chariot":
                    _d = this.gameObject.AddComponent<Rook>();
                    bullet = Resources.Load<GameObject>("TESTBUL 2");
                    break;
                case "Horse":
                    _d = this.gameObject.AddComponent<Knight>();
                    //
                    break;
                case "Elephant":
                    _d = this.gameObject.AddComponent<Elephant>();
                    //
                    break;
                case "Cannon":
                    _d = this.gameObject.AddComponent<Cannon>();
                    //
                    break;
                case "Guard":
                    _d = this.gameObject.AddComponent<Guards>();
                    //
                    break;
                case "King":
                    _d = this.gameObject.AddComponent<King>();
                    //
                    break;
                default:
                    Debug.Log("it does not exist");
                    break;
            }
        }
        else
        {
            Debug.Log("Deselect " + str);
            bullet = Resources.Load<GameObject>("TESTBUL 0");
            Destroy(_d);
        }
    }
}
