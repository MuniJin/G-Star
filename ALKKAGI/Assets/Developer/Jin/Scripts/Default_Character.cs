using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� -> ���ڷ����� ������ ����ϱ� ���� ������ �߻� Ŭ����
public abstract class Default_Character : MonoBehaviour
{
    // ü��
    protected int _hp { get; set; }

    public void Attacked(int damage) { _hp -= damage; }

    public int GetHp() { return _hp; }

    // ��ų ��Ÿ��
    protected float _coolDown { get; set; }

    public float GetCoolDown() { return _coolDown; }

    // ���� ������
    protected int _damage { get; set; }

    public int GetDamage() { return _damage; }

    public void SetStatus(int hp, float coolDown, int damage)
    {
        _hp = hp;
        _coolDown = coolDown;
        _damage = damage;
    }

    public void GetStatus() { Debug.Log($"Name : {this.name} | Hp : {_hp} | CoolDown : {_coolDown} | Damage : {_damage}"); }

    // ������, ����, ����, ��ų �߻� �Լ�
    protected abstract void Move();
    protected abstract void Jump();
    public abstract void Attack(Vector3 bulpos, float shootPower);
    public abstract IEnumerator Skill(GameObject go);
}

public class Decorator_Character : Default_Character
{
    protected override void Move() => throw new System.NotImplementedException();
    protected override void Jump() => throw new System.NotImplementedException();
    public override void Attack(Vector3 bulpos, float shootPower) => throw new System.NotImplementedException();

    public override IEnumerator Skill(GameObject go) => throw new System.NotImplementedException();
}

// �� : �̵��ӵ� ����, �����ð� �ӵ� 2��
public class Pawn : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private void Start()
    {
        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
            Destroy(pScript);
        }
    }

    //public override void Attack(Vector3 bulpos, float shootPower)
    //{
    //    if (basePlayer.CompareTag("Player"))
    //        pScript.Attack(bulpos, shootPower);
    //    else if (basePlayer.CompareTag("Enemy"))
    //        eScript.Attack(bulpos, shootPower);
    //}

    public override IEnumerator Skill(GameObject go)
    {
        if (go.tag == "Player")
        {
            if (!useSkill)
            {
                useSkill = true;
                go.GetComponent<Player_Character>().speed *= 2f;

                yield return new WaitForSeconds(cooldown);

                go.GetComponent<Player_Character>().speed /= 2f;
                useSkill = false;
            }
        }
        else if(go.tag == "Enemy")
        {
            if (!useSkill)
            {
                useSkill = true;
                //go.GetComponent<EnemyAI2>().moveSpeed *= 2f;

                yield return new WaitForSeconds(cooldown);

                //go.GetComponent<EnemyAI2>().moveSpeed /= 2f;
                useSkill = false;
            }
        }
    }
}

// �� : ����
public class Rook : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private void Start()
    {
        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
            Destroy(pScript);
        }

        cooldown = this.GetCoolDown();
    }

    //public override void Attack(Vector3 bulpos, float shootPower)
    //{
    //    if (basePlayer.CompareTag("Player"))
    //        pScript.Attack(bulpos, shootPower);
    //    else if (basePlayer.CompareTag("Enemy"))
    //        eScript.Attack(bulpos, shootPower);
    //}

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            Debug.Log("UseSkill");
            useSkill = true;
            Vector3 forwardDirection = Vector3.zero;
            if (go.tag == "Player")
                forwardDirection = Camera.main.transform.forward;
            else if(go.tag == "Enemy")
                forwardDirection = go.transform.forward;

            go.GetComponent<Rigidbody>().AddForce(forwardDirection * 30f, ForceMode.Impulse);

            yield return new WaitForSeconds(cooldown);
            Debug.Log("EndSkill");
            useSkill = false;
        }
    }
}

// �� : ���� �̵��ӵ� ����
public class Elephant : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private void Start()
    {
        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
            Destroy(pScript);
        }
    }

    //public override void Attack(Vector3 bulpos, float shootPower)
    //{
    //    if (basePlayer.CompareTag("Player"))
    //        pScript.Attack(bulpos, shootPower);
    //    else if (basePlayer.CompareTag("Enemy"))
    //        eScript.Attack(bulpos, shootPower);
    //}

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}

// �� : ����
public class Knight : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private void Start()
    {
        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
            Destroy(pScript);
        }
    }

    //public override void Attack(Vector3 bulpos, float shootPower)
    //{
    //    if (basePlayer.CompareTag("Player"))
    //        pScript.Attack(bulpos, shootPower);
    //    else if (basePlayer.CompareTag("Enemy"))
    //        eScript.Attack(bulpos, shootPower);
    //}

    public override IEnumerator Skill(GameObject go)
    {
        Vector3 b1, b2;

        if (go.tag == "Player")
        {
            if (!useSkill)
            {
                useSkill = true;

                for (int i = 0; i < 3; i++)
                {
                    b1 = pScript.bulPos.transform.position + go.transform.right * -0.5f;
                    b2 = pScript.bulPos.transform.position + go.transform.right * 0.5f;

                    pScript.Attack(b1, 60f);
                    pScript.Attack(b2, 60f);

                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
        else if(go.tag == "Enemy")
        {
            if (!useSkill)
            {
                useSkill = true;

                for (int i = 0; i < 3; i++)
                {
                    b1 = go.transform.GetChild(0).transform.position + go.transform.right * -0.5f;
                    b2 = go.transform.GetChild(0).transform.position + go.transform.right * 0.5f;

                    go.GetComponent<EnemyAI2>().Attack(b1, 60f);
                    go.GetComponent<EnemyAI2>().Attack(b2, 60f);
                    //eScript.Attack(b1, 60f);
                    //eScript.Attack(b2, 60f);

                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        yield return new WaitForSeconds(cooldown - 0.6f);
        useSkill = false;
    }
}

// �� : �����̵�
public class Cannon : Decorator_Character
{
    private float cooldown;
    private float damage;
    private bool useSkill = false;

    private LayerMask groundLayer;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");

        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            damage = this.GetDamage();

            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();

            Destroy(pScript);
        }

        cooldown = this.GetCoolDown();
    }

    //public override void Attack(Vector3 bulpos, float shootPower)
    //{
    //    if (basePlayer.CompareTag("Player"))
    //        pScript.Attack(bulpos, shootPower);
    //    else if (basePlayer.CompareTag("Enemy"))
    //        eScript.Attack(bulpos, shootPower);
    //}

    public override IEnumerator Skill(GameObject go)
    {
        if (basePlayer.tag == "Player")
        {
            if (!useSkill)
            {
                useSkill = true;
                while (true)
                {
                    Cursor.lockState = CursorLockMode.None; // ���콺�� �߾ӿ� ����
                    Cursor.visible = true; // ���콺�� ������ �ʰ� ����

                    if (Input.GetMouseButtonDown(1))
                    {
                        // ���콺 ��ġ�� ��ũ�� ��ǥ���� ���� ��ǥ�� ��ȯ�մϴ�.
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        // ����ĳ��Ʈ�� �߻��Ͽ� �浹�� ��ü�� �����մϴ�.
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                        {
                            // �浹�� ������ ����3 ���� ����ϴ�.
                            Vector3 hitPoint = hit.point;

                            // hitPoint ������ ����Ͽ� ���ϴ� �۾��� ������ �� �ֽ��ϴ�.
                            go.transform.position = hitPoint + Vector3.up;

                            Cursor.lockState = CursorLockMode.Locked; // ���콺�� �߾ӿ� ����
                            Cursor.visible = false; // ���콺�� ������ �ʰ� ����
                        }

                        break;
                    }

                    yield return null;
                }
            }
        }
        if (basePlayer.tag == "Enemy")
        {
            if (!useSkill)
            {
                useSkill = true;
                float rayLength = 100f;

                Vector3 randomDirection = Random.onUnitSphere;

                Ray ray = new Ray(transform.position, randomDirection);
                
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, rayLength, groundLayer))
                {
                    Vector3 hitPoint = hit.point;

                    go.transform.position = hitPoint + Vector3.up;
                }
            }
        }

        yield return new WaitForSeconds(cooldown);
        useSkill = false;
    }
}

// �� : ����ź
public class Guards : Decorator_Character
{
    private float cooldown = 2f;
    private bool useSkill = false;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private void Start()
    {
        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();


            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
            Destroy(pScript);
        }
    }

    //public override void Attack(Vector3 bulpos, float shootPower)
    //{
    //    if (basePlayer.CompareTag("Player"))
    //        pScript.Attack(bulpos, shootPower);
    //    else if (basePlayer.CompareTag("Enemy"))
    //        eScript.Attack(bulpos, shootPower);
    //}

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;
            

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}

// �� : ���� �������
public class King : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private GameObject ks;

    private void Start()
    {
        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            ks = Resources.Load<GameObject>("KingSkill_Red");
            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
            ks = Resources.Load<GameObject>("KingSkill_Red");
            Destroy(pScript);
        }
    }

    //public override void Attack(Vector3 bulpos, float shootPower)
    //{
    //    if (basePlayer.CompareTag("Player"))
    //        pScript.Attack(bulpos, shootPower);
    //    else if (basePlayer.CompareTag("Enemy"))
    //        eScript.Attack(bulpos, shootPower);
    //}

    public override IEnumerator Skill(GameObject go)
    {
        if (go.tag == "Player")
        {
            if (!useSkill)
            {
                useSkill = true;

                Instantiate(ks, go.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(cooldown);
                useSkill = false;
            }
        }
        else if(go.tag == "Enemy")
        {
            if (!useSkill)
            {
                useSkill = true;

                Instantiate(ks, go.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(cooldown);
                useSkill = false;
            }
        }
    }
}
