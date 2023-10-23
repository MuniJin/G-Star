using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� -> ���ڷ����� ������ ����ϱ� ���� ������ �߻� Ŭ����
public abstract class Default_Character : MonoBehaviour
{
    // ü��
    protected int _hp { get; set; }

    public int GetHp() { return _hp; }

    public int InitHp(int hp)
    {
        _hp = hp;

        return _hp;
    }

    public int SetHp(int damage)
    {
        _hp = _hp - damage;
        
        return _hp;
    }

    // ��ų ��Ÿ��
    public float _coolDown { get; set; }
    // ���� ������
    public float _damage { get; set; }

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

    public override IEnumerator Skill(GameObject go)
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
}

// �� : ����
public class Rook : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;
            
            Vector3 forwardDirection = Camera.main.transform.forward;
            go.GetComponent<Rigidbody>().AddForce(forwardDirection * 20f, ForceMode.Impulse);

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}

// �� : ���� �̵��ӵ� ����
public class Elephant : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

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

    public override IEnumerator Skill(GameObject go)
    {
        Vector3 b1, b2;
        Player_Character g = go.GetComponent<Player_Character>();

        if (!useSkill)
        {
            useSkill = true;

            for (int i = 0; i < 3; i++)
            {
                b1 = g.bulPos.transform.position + go.transform.right * -0.5f;
                b2 = g.bulPos.transform.position + go.transform.right * 0.5f;

                g.Attack(b1, 60f);
                g.Attack(b2, 60f);

                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(cooldown - 0.6f);
            useSkill = false;
        }
    }
}

// �� : �����̵�
public class Cannon : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    private LayerMask groundLayer;

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    public override IEnumerator Skill(GameObject go)
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

                        // ���� hitPoint �������� �浹�� ������ ���� ��ǥ�� ���Ե˴ϴ�.
                        Debug.Log("Hit point: " + hitPoint);

                        // hitPoint ������ ����Ͽ� ���ϴ� �۾��� ������ �� �ֽ��ϴ�.
                        go.transform.position = hitPoint + Vector3.up;

                        Cursor.lockState = CursorLockMode.Locked; // ���콺�� �߾ӿ� ����
                        Cursor.visible = false; // ���콺�� ������ �ʰ� ����
                    }

                    break;
                }

                yield return null;
            }

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}

// �� : ����ź
public class Guards : Decorator_Character
{
    private float cooldown = 2f;
    private bool useSkill = false;

    public GameObject bang;

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;
            
            GameObject b = Instantiate(bang, go.GetComponent<Player_Character>().bulPos.transform.position, Quaternion.identity);
            b.AddComponent<Rigidbody>();
            //b.AddComponent<Rigidbody>().AddForce(go.GetComponent<PlayerMovement1>().playerCam.transform.forward * 20f, ForceMode.Impulse);

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

    public override IEnumerator Skill(GameObject go)
    {
        Player_Character g = go.GetComponent<Player_Character>();

        if (!useSkill)
        {
            useSkill = true;

            GameObject p = go.transform.GetChild(0).gameObject;
            Vector3 forwardDirection = Camera.main.transform.forward;

            Vector3 ksPos = p.transform.position + (Camera.main.transform.forward * 2f);
            //GameObject ks = Instantiate(g.kingSkill, ksPos, Camera.main.transform.rotation);

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}
