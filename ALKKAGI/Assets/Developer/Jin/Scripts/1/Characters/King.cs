using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 왕 : 병사 벽세우기
public class King : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private GameObject ks;

    private void Start()
    {
        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            ks = Resources.Load<GameObject>("Skills\\KingSkill_Red");
            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
            ks = Resources.Load<GameObject>("Skills\\KingSkill_Blue");
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

                Instantiate(ks, go.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(cooldown);
                useSkill = false;
            }
        }
        else if (go.tag == "Enemy")
        {
            if (!useSkill)
            {
                useSkill = true;

                Instantiate(ks, go.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(cooldown);
                useSkill = false;
            }
        }
    }
}
