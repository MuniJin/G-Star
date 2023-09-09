using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : BaseCharacter
{
    Deco _d;
    private void Update()
    {
        if (_d == null && Input.GetKeyDown(KeyCode.W))
        {
            _d = this.gameObject.AddComponent<Deco>();
        }

        if (Input.GetKeyDown(KeyCode.Q))
            Move();
    }

    public override void Move()
    {
        if (_d != null)
            _d.Move();
        else
            Debug.Log("asdf");
    }
    public override void Skill()
    {
        throw new System.NotImplementedException();
    }
}
