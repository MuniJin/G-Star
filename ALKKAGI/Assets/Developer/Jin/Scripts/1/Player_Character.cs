using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player_Character : Default_Character
{
    // �˱�� �Ŵ����� ���� �̱������� ������ ����
    private AlKKAGIManager am;
    private FPSManager fm;
    private PHPCTR pc;

    // Default_Character�� ��ӹ��� ������ ĳ����(��, ��, ��, ��...)�� Ư���� ���� �� �ְ� ����
    private Decorator_Character _d;

    // ���� ����� ���� ����
    private Rigidbody rb;

    // �Ѿ�, �÷��̾� ������Ʈ, �� ��ų(���� �ٸ�������� ������ �ҷ��ͼ� ����� ����)
    public GameObject bullet;
    // �ѱ� ��ġ
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

        // �÷��̾� ������Ʈ�� rigidbody �޾ƿ���
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
        // AI �ϼ� ������ ���� ���г�������� ���Ƿ� ����� �� ���ǹ�
        if (Input.GetKeyDown(KeyCode.O))
            fm.Win();
        if (Input.GetKeyDown(KeyCode.P))
            fm.Lose();

        // �÷��̾� ������
        Move();
        // ���콺 �����ӿ� ���� ī�޶� ȸ���� ����
        CamMove();

        // ����, velocity�� ������ ���� �����ϰ�
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
            Jump();

        // �ѱ� ��ġ���� �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0))
        {
            Attack(bulPos.transform.position);
            fm.BulletSoundPlay(this.tag);
        }

        // ��ų ���
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

    // ����
    protected override void Jump() => rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    // ����
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

    // ��ų ���
    private void UseSkill()
    {
        if (_d != null)
            StartCoroutine(_d.Skill(this.gameObject));
        else
            Debug.Log("Not Decorator");
    }

    // ��ų(Default_Character�� ��ӹ޾Ƽ� �����ϳ� �ʿ��� ����ó���� �ص�)
    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}