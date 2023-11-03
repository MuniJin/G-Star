using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Character : Default_Character
{
    private AlKKAGIManager am;
    private FPSManager fm;

    private Default_Character _d;

    public GameObject bullet;
    public GameObject bulPos;

    public float eCoolDown;

    private Rigidbody rb;

    // ¼Óµµ, Á¡ÇÁ Èû
    public float speed = 8f;
    public float jumpForce = 8f;

    public GameObject particle;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Map1")
            am = AlKKAGIManager.Instance;
        fm = FPSManager.Instance;

        bulPos = this.gameObject.transform.GetChild(0).gameObject;

        rb = this.gameObject.GetComponent<Rigidbody>();

        fm.ChooseCharacter(ref _d, ref bullet, ref particle, this.gameObject);

        eCoolDown = _d.GetCoolDown();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            EAttack();
    }

    public void Hitted(int damage)
    {
        _d.Attacked(damage);
        
        _d.GetStatus();

        if (_d.GetHp() <= 0f)
        {
            Debug.Log("Game Over");
            fm.Win();
        }
    }

    public void EAttack()
    {
        Attack(bulPos.transform.position, bulletSpeed);
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
    private float angle = 0f;

    public override void Attack(Vector3 bulpos, float shootPower)
    {
        GameObject go = Instantiate(bullet, bulpos, bullet.transform.rotation);

        Vector3 direction = (GameObject.FindWithTag("Player").transform.position - bulpos).normalized;

        float r1, r2, r3;
        r1 = Random.Range(-0.2f, 0.2f);
        r2 = Random.Range(-0.2f, 0.2f);
        r3 = Random.Range(-0.2f, 0.2f);

        direction = new Vector3(direction.x + r1, direction.y + r2, direction.z + r3);

        Rigidbody rb2 = go.GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb2.velocity = direction * bulletSpeed;

        go.GetComponent<Bullet>().damage = _d.GetDamage();
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
