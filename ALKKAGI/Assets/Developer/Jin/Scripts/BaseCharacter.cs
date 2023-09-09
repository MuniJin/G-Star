using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public abstract void Move();
    public abstract void Skill();
}

public class Deco : BaseCharacter
{
    public override void Move()
    {
        Debug.Log("Deco");
    }
    public override void Skill()
    {
        throw new System.NotImplementedException();
    }
}
