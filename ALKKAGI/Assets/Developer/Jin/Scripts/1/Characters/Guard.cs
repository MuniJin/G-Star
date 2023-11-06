using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Decorator_Character
{
    private float cooldown = 10f;
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
        if (!useSkill)
        {
            useSkill = true;

            if (go.tag == "Player")
            {
                Debug.Log("Use Skill");
                pScript.damagebuff = true;
                yield return new WaitForSeconds(cooldown);
                pScript.damagebuff = false;
            }
            else if (go.tag == "Enemy")
            {
                eScript.damagebuff = true;
                yield return new WaitForSeconds(cooldown);
                eScript.damagebuff = false;
            }

            useSkill = false;
        }
    }
}