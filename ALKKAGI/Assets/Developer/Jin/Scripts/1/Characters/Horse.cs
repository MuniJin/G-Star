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
                    b1 = pScript.bulPos.transform.position + go.transform.right * -0.5f;
                    b2 = pScript.bulPos.transform.position + go.transform.right * 0.5f;

                    pScript.Attack(b1, 60f);
                    pScript.Attack(b2, 60f);

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
                    b1 = go.transform.GetChild(0).transform.position + go.transform.right * -0.5f;
                    b2 = go.transform.GetChild(0).transform.position + go.transform.right * 0.5f;

                    go.GetComponent<EnemyAI2>().Attack(b1, 60f);
                    go.GetComponent<EnemyAI2>().Attack(b2, 60f);
                    //eScript.Attack(b1, 60f);
                    //eScript.Attack(b2, 60f);

                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        yield return new WaitForSeconds(cooldown - 0.6f);
        useSkill = false;
    }
}