using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 디자인 패턴 -> 데코레이터 패턴을 사용하기 위해 만들어둔 추상 클래스
public abstract class Default_Character : MonoBehaviour
{
    // 체력
    protected int _hp { get; set; }

    public void Attacked(int damage) { _hp -= damage; }

    public int GetHp() { return _hp; }

    // 스킬 쿨타임
    protected float _coolDown { get; set; }

    public float GetCoolDown() { return _coolDown; }

    // 공격 데미지
    protected int _damage { get; set; }

    public int GetDamage() { return _damage; }

    public void SetStatus(int hp, float coolDown, int damage)
    {
        _hp = hp;
        _coolDown = coolDown;
        _damage = damage;
    }

    public void GetStatus() { Debug.Log($"Name : {this.name} | Hp : {_hp} | CoolDown : {_coolDown} | Damage : {_damage}"); }

    // 움직임, 점프, 공격, 스킬 추상 함수
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

// 쫄 : 이동속도 버프, 일정시간 속도 2배
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

// 차 : 돌진
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

// 상 : 상대방 이동속도 감소
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

// 마 : 연사
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

// 포 : 순간이동
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
                    Cursor.lockState = CursorLockMode.None; // 마우스를 중앙에 고정
                    Cursor.visible = true; // 마우스를 보이지 않게 설정

                    if (Input.GetMouseButtonDown(1))
                    {
                        // 마우스 위치를 스크린 좌표에서 월드 좌표로 변환합니다.
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        // 레이캐스트를 발사하여 충돌한 객체를 감지합니다.
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                        {
                            // 충돌한 지점의 벡터3 값을 얻습니다.
                            Vector3 hitPoint = hit.point;

                            // hitPoint 변수를 사용하여 원하는 작업을 수행할 수 있습니다.
                            go.transform.position = hitPoint + Vector3.up;

                            Cursor.lockState = CursorLockMode.Locked; // 마우스를 중앙에 고정
                            Cursor.visible = false; // 마우스를 보이지 않게 설정
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

// 사 : 섬광탄
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

// 왕 : 병사 벽세우기
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
