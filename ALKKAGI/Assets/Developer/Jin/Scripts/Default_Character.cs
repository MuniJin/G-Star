using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Default_Character : MonoBehaviour
{
    public abstract void Move();
    public abstract void Jump();
    public abstract void Attack();

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
    public override void Attack()
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
        if (!useSkill)
        {
            useSkill = true;

            yield return new WaitForSeconds(cooldown);
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
