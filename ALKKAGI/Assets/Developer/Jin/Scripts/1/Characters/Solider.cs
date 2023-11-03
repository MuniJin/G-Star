using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : Decorator_Character
{
    private float cooldown;
    private bool useSkill = false;

    private GameObject basePlayer;

    public Player_Character pScript;
    public Enemy_Character eScript;

    private void Start()
    {
        basePlayer = base.gameObject;

        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            cooldown = pScript.pCoolDown;
            Debug.Log(cooldown);
            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
            cooldown = eScript.eCoolDown;
            Debug.Log(cooldown);
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
                pScript.speed *= 2f;

                yield return new WaitForSeconds(cooldown / 2);

                pScript.speed /= 2f;

                yield return new WaitForSeconds(cooldown / 2);
                useSkill = false;
            }
        }
        else if (go.tag == "Enemy")
        {
            if (!useSkill)
            {
                useSkill = true;
                eScript.speed *= 2f;

                yield return new WaitForSeconds(cooldown / 2);

                eScript.speed /= 2f;

                yield return new WaitForSeconds(cooldown / 2);
                useSkill = false;
            }
        }
    }
}
