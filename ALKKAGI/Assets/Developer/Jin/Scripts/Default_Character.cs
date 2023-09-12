using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Default_Character : MonoBehaviour
{
    public abstract void Move();
    public abstract void Jump();
    public abstract void Attack(Vector3 bulpos, float shootPower);

    public abstract IEnumerator Skill(GameObject go);
}

public class Decorator_Character : Default_Character
{

    public override void Move()
    {
        throw new System.NotImplementedException();
    }
    public override void Jump()
    {
        throw new System.NotImplementedException();
    }
    public override void Attack(Vector3 bulpos, float shootPower)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}

// �� : �̵��ӵ� ����
public class Pawn : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;
            go.GetComponent<Character>().speed *= 2f;
         
            yield return new WaitForSeconds(cooldown);
            
            go.GetComponent<Character>().speed /= 2f;
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
            Vector3 forwardDirection = Vector3.forward;
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

        if (!useSkill)
        {
            useSkill = true;

            for (int i = 0; i < 3; i++)
            {
                b1 = go.GetComponent<Character>().bulParent2.position;
                b2 = go.GetComponent<Character>().bulParent3.position;

                go.GetComponent<Character>().Attack(b1, 60f);
                go.GetComponent<Character>().Attack(b2, 60f);

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

// �� : ����ź
public class Guards : Decorator_Character
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

// �� : ?
public class King : Decorator_Character
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
