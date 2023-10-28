using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Character : Default_Character
{
    private AlKKAGIManager am;
    private FPSManager fm;

    Default_Character _d;

    private GameObject bullet;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Map1")
            am = AlKKAGIManager.Instance;
        fm = FPSManager.Instance;

        fm.ChooseCharacter(ref _d, ref bullet, this.gameObject);
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
