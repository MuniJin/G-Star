using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player_Character : Default_Character
{
    // 알까기 매니저는 추후 싱글톤으로 변경할 예정
    private AlKKAGIManager am;
    private FPSManager fm;
    // Default_Character를 상속받은 각각의 캐릭터(쫄, 상, 포, 마...)의 특성을 입힐 수 있게 선언
    private Default_Character _d;
    // 물리 계산을 위해 선언
    private Rigidbody rb;
    // 총알, 플레이어 오브젝트, 왕 스킬(추후 다른방식으로 프리팹 불러와서 사용할 예정)
    public GameObject bullet;

    // 속도, 점프 힘
    public float speed = 8f;
    public float jumpForce = 8f;

    // 총구 위치
    public GameObject bulPos;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Map1")
            am = AlKKAGIManager.Instance;
        fm = FPSManager.Instance;

        cam = Camera.main;
        bulPos = this.gameObject.transform.GetChild(0).gameObject;

        // 플레이어 오브젝트와 rigidbody 받아오기
        rb = this.gameObject.GetComponent<Rigidbody>();

        fm.ChooseCharacter(ref _d, ref bullet, this.gameObject);
        if (bullet.GetComponent<Bullet>() == false)
            bullet.AddComponent<Bullet>();
    }

    private void Update()
    {
        // 플레이어 움직임
        Move();
        // 마우스 움직임에 따른 카메라 회전값 변경
        CamMove();

        // 점프, velocity가 없을때 점프 가능하게
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
            Jump();
        // 총구 위치에서 총알 발사
        if (Input.GetMouseButtonDown(0))
            _d.Attack(bulPos.transform.position, 40f);
        // 스킬 사용
        if (Input.GetKeyDown(KeyCode.Q))
            UseSkill();

        // AI 완성 전까지 게임 승패내기용으로 임의로 만들어 둔 조건문
        if (Input.GetKeyDown(KeyCode.O))
            fm.Win();
        if (Input.GetKeyDown(KeyCode.P))
            fm.Lose();

        if (Input.GetKeyDown(KeyCode.Z))
            _d.GetStatus();
    }

    public void Hitted(int damage)
    {
        _d.Attacked(18);
        //_d.Attacked(damage);

        _d.GetStatus();

        if (_d.GetHp() <= 0f)
        {
            Debug.Log("Game Over");
            fm.Lose();
        }
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

    public float sensitivity = 0.5f;

    private void CamMove()
    {
        cam.transform.position = this.gameObject.transform.position;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        eulerAngleX -= mouseY * rotCamX * sensitivity;
        eulerAngleY += mouseX * rotCamY * sensitivity;

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
        GameObject go = Instantiate(bullet, bulpos, Quaternion.identity);
        go.GetComponent<Bullet>().damage = _d.GetDamage();
        
        go.GetComponent<Rigidbody>().AddForce(cam.transform.forward * shootPower, ForceMode.Impulse);
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
}