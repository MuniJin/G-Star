using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Character : Default_Character
{
    private AlKKAGIManager am;
    private FPSManager fm;

    private Decorator_Character _d;

    private Rigidbody rb;

    public GameObject bullet;
    public GameObject bulPos;

    public float eCoolDown;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Map1")
            am = AlKKAGIManager.Instance;
        fm = FPSManager.Instance;

        bulPos = this.gameObject.transform.GetChild(0).gameObject;

        rb = this.gameObject.GetComponent<Rigidbody>();

        this.speed = 8f;
        this.jumpForce = 8f;

        fm.ChooseCharacter(ref _d, ref bullet, this.gameObject);

        eCoolDown = _d.GetCoolDown();

        ObjPullingBullet();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            EAttack();
        if (Input.GetKeyDown(KeyCode.X))
            EUseSkill();
    }

    public void Hitted(int damage)
    {
        _d.Attacked(damage);
        _d.GetStatus();

        if (_d.GetHp() <= 0f)
            fm.Win();
    }

    public void EAttack()
    {
        Attack(bulPos.transform.position, bulletSpeed);
    }

    [SerializeField]
    private List<GameObject> bullets = new List<GameObject>();

    private void ObjPullingBullet()
    {
        if (this.name.Split('_')[0] == "King")
        {
            for (int i = 0; i < 6; ++i)
            {
                GameObject go = Instantiate(bullet, bulPos.transform.position, bullet.transform.rotation);

                for (int j = 0; j < 6; ++j)
                {
                    GameObject go2 = go.transform.GetChild(6).gameObject;

                    go2.transform.SetParent(bulPos.transform);
                    go2.AddComponent<Bullet>();
                    go2.GetComponent<Bullet>().damage = _d.GetDamage();
                    go2.GetComponent<Bullet>().bulPos = bulPos.transform;

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

                bullets.Add(go);
                go.SetActive(false);
            }
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

    public void EUseSkill()
    {
        if (_d != null)
            StartCoroutine(_d.Skill(this.gameObject));
        else
            Debug.Log("Not Decorator");
    }

    public void EJump()
    {
        Jump();
    }

    private float bulletSpeed = 80f;

    public override void Attack(Vector3 bulpos, float shootPower)
    {
        int temp = AttackingBulletSelect();
        bullets[temp].transform.parent = null;

        GameObject go = bullets[temp];

        Vector3 direction = (GameObject.FindWithTag("Player").transform.position - bulpos).normalized;

        float r1, r2, r3;
        r1 = Random.Range(-0.2f, 0.2f);
        r2 = Random.Range(-0.2f, 0.2f);
        r3 = Random.Range(-0.2f, 0.2f);

        direction = new Vector3(direction.x + r1, direction.y + r2, direction.z + r3);

        Rigidbody brb = bullets[temp].GetComponent<Rigidbody>();
        brb.interpolation = RigidbodyInterpolation.Interpolate;
        brb.velocity = direction * bulletSpeed;
    }

    protected override void Jump() => rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);


    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}
