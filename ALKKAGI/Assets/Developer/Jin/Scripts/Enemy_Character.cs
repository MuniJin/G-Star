using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Character : Default_Character
{
    private AlKKAGIManager am;
    private FPSManager fm;

    public Default_Character _d;

    private GameObject bullet;
    private Vector3 bulPos;

    public float eCoolDown;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Map1")
            am = AlKKAGIManager.Instance;
        fm = FPSManager.Instance;

        fm.ChooseCharacter(ref _d, ref bullet, this.gameObject);

        eCoolDown = _d.GetCoolDown();
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
        Attack(bulPos, 60f);
    }

    public override void Attack(Vector3 bulpos, float shootPower)
    {
        throw new System.NotImplementedException();
    }

    public void EUseSkill()
    {
        UseSkill();
    }

    private void UseSkill()
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


    protected override void Jump()
    {
        Debug.Log("Jump");
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }
}
