using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyHp : Default_Character
{
    private void Start()
    {
        this._hp = 100;
    }

    public override void Attack(Vector3 bulpos, float shootPower)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }

    protected override void Jump()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }
}
