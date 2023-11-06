using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : Decorator_Character
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


    public override IEnumerator Skill(GameObject go)
    {
        if (go.tag == "Player")
        {
            if (!useSkill)
            {
                useSkill = true;

                FPSManager.Instance.ScopeImg.gameObject.SetActive(true);
                Camera.main.fieldOfView /= 3;

                pScript.damagebuff = true;
            }
        }
        else if (go.tag == "Enemy")
        {
            if (!useSkill)
            {
                useSkill = true;
            }
        }

        yield return new WaitForSeconds(cooldown);
        useSkill = false;
    }
}