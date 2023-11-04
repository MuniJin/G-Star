using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ¸¶ : ¿¬»ç
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
        Vector3 b1, b2;

        if (go.tag == "Player")
        {
            if (!useSkill)
            {
                useSkill = true;

                for (int i = 0; i < 3; i++)
                {
                    pScript.Attack(go.transform.GetChild(2).position);
                    pScript.Attack(go.transform.GetChild(3).position);

                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
        else if (go.tag == "Enemy")
        {
            if (!useSkill)
            {
                useSkill = true;

                for (int i = 0; i < 3; i++)
                {
                    b1 = go.transform.GetChild(0).transform.position + go.transform.right * -1f;
                    b2 = go.transform.GetChild(0).transform.position + go.transform.right * 1f;

                    eScript.Attack(b1);
                    eScript.Attack(b2);

                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        yield return new WaitForSeconds(cooldown - 0.6f);
        useSkill = false;
    }
}