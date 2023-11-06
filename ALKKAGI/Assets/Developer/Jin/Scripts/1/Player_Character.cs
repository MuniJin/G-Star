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
    private PHPCTR pc;

    // Default_Character를 상속받은 각각의 캐릭터(쫄, 상, 포, 마...)의 특성을 입힐 수 있게 선언
    private Decorator_Character _d;

    // 물리 계산을 위해 선언
    private Rigidbody rb;

    // 총알, 플레이어 오브젝트, 왕 스킬(추후 다른방식으로 프리팹 불러와서 사용할 예정)
    public GameObject bullet;
    // 총구 위치
    public GameObject bulPos;

    public float pCoolDown;

    public bool damagebuff;

    private void Start()
    {
        am = AlKKAGIManager.Instance;
        fm = FPSManager.Instance;
        pc = PHPCTR.Instance;

        cam = Camera.main;
        bulPos = this.gameObject.transform.GetChild(0).gameObject;

        // 플레이어 오브젝트와 rigidbody 받아오기
        rb = this.gameObject.GetComponent<Rigidbody>();

        this.speed = 15f;
        this.jumpForce = 10f;

        fm.ChooseCharacter(ref _d, ref bullet, this.gameObject);

        pCoolDown = _d.GetCoolDown();
        damagebuff = false;
        Debug.Log(pCoolDown);
        ObjPullingBullet();
    }

    private void Update()
    {
        // AI 완성 전까지 게임 승패내기용으로 임의로 만들어 둔 조건문
        if (Input.GetKeyDown(KeyCode.O))
            fm.Win();
        if (Input.GetKeyDown(KeyCode.P))
            fm.Lose();

        // 플레이어 움직임
        Move();
        // 마우스 움직임에 따른 카메라 회전값 변경
        CamMove();

        // 점프, velocity가 없을때 점프 가능하게
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
            Jump();

        // 총구 위치에서 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            Attack(bulPos.transform.position);
            fm.BulletSoundPlay(this.tag);
        }

        // 스킬 사용
        if (Input.GetKeyDown(KeyCode.Q))
            UseSkill();
    }

    public void Hitted(int damage)
    {
        _d.Attacked(damage);
        _d.GetStatus();
        pc.PlayerHitted(damage);

        if (_d.GetHp() <= 0f)
            fm.Lose();
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

    private float limitMinX = -90f;
    private float limitMaxX = 90f;

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
    public List<GameObject> bullets = new List<GameObject>();

    private void ObjPullingBullet()
    {
        if (this.name.Split('_')[0] == "King")
        {
            for (int i = 0; i < 6; ++i)
            {
                GameObject go = Instantiate(bullet, bulPos.transform.position, bullet.transform.rotation);

                for (int j = 0; j < 6; ++j)
                {
                    GameObject go2 = go.transform.GetChild(0).gameObject;

                    go2.transform.SetParent(bulPos.transform);
                    go2.AddComponent<Bullet>();
                    go2.GetComponent<Bullet>().damage = _d.GetDamage();
                    go2.GetComponent<Bullet>().bulPos = bulPos.transform;

                    go2.GetComponent<Bullet>().parentPlayer = this.tag;

                    bullets.Add(go2);
                    go2.SetActive(false);
                }

                Destroy(go);
            }
        }
        else
        {
            for (int i = 0; i < 36; ++i)
            {
                GameObject go = Instantiate(bullet, bulPos.transform.position, bullet.transform.rotation);

                go.transform.parent = bulPos.transform;

                go.GetComponent<Bullet>().damage = _d.GetDamage();
                go.GetComponent<Bullet>().bulPos = bulPos.transform;

                go.GetComponent<Bullet>().parentPlayer = this.tag;

                if (this.name.Split('_')[0] == "Guard")
                    if (i > 26)
                        go.GetComponent<Bullet>().guardBuffDamage = 5;

                bullets.Add(go);
                go.SetActive(false);
            }
        }

        if (this.name.Split('_')[0] == "Horse")
        {
            bullet = Resources.Load<GameObject>("Bullets\\HorseShoe2");
            GameObject go = Instantiate(bullet, bulPos.transform.position, bullet.transform.rotation);

            go.transform.parent = bulPos.transform;

            go.GetComponent<Bullet>().damage = _d.GetDamage() * 2;
            go.GetComponent<Bullet>().bulPos = bulPos.transform;

            go.GetComponent<Bullet>().parentPlayer = this.tag;

            bullets.Add(go);
            go.SetActive(false);
        }
    }

    private int AttackingBulletSelect()
    {
        int rand = Random.Range(0, 36);

        while (true)
        {
            if (bullets[rand].activeInHierarchy == false)
            {
                bullets[rand].SetActive(true);

                break;
            }
            else
                rand = Random.Range(0, 36);
        }

        return rand;
    }

    private float bulletSpeed = 80f;

    public override void Attack(Vector3 bulpos)
    {
        if (fm.ScopeImg.gameObject.activeInHierarchy == true)
        {
            fm.ScopeImg.gameObject.SetActive(false);
            cam.fieldOfView *= 3;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (damagebuff)
            {
                if (this.gameObject.name.Split('_')[0] == "Horse")
                {
                    bullets[bullets.Count - 1].SetActive(true);

                    ShootBul(bullets.Count - 1, bulpos, hit);

                    damagebuff = false;
                }
                else if (this.gameObject.name.Split('_')[0] == "Guard")
                {
                    int temp = 0;
                    while (true)
                    {
                        temp = Random.Range(27, bullets.Count - 1);

                        if (temp >= 27)
                        {
                            bullets[temp].SetActive(true);
                            break;
                        }
                    }

                    ShootBul(temp, bulpos, hit);
                }
            }
            else
            {
                int temp = 0;
                if (this.gameObject.name.Split('_')[0] == "Guard")
                {
                    temp = Random.Range(0, 27);
                    bullets[temp].SetActive(true);
                }
                else
                    temp = AttackingBulletSelect();

                ShootBul(temp, bulpos, hit);
            }
        }
    }

    private void ShootBul(int bulNum, Vector3 bulpos, RaycastHit hit)
    {

        bullets[bulNum].transform.parent = null;
        bullets[bulNum].transform.position = bulpos;

        Vector3 direction = (hit.point - bullets[bulNum].transform.position).normalized;
        Rigidbody brb = bullets[bulNum].GetComponent<Rigidbody>();
        brb.interpolation = RigidbodyInterpolation.Interpolate;
        brb.velocity = direction * bulletSpeed;
    }

    // 스킬 사용
    private void UseSkill()
    {
        if (_d != null)
            StartCoroutine(_d.Skill(this.gameObject));
        else
            Debug.Log("Not Decorator");
    }

    // 스킬(Default_Character를 상속받아서 존재하나 필요없어서 예외처리로 해둠)
    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}