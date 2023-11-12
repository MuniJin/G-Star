using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �� : ���� �������
public class King : Decorator_Character
{
    private float cooldown;
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

        cooldown = this.GetCoolDown();
    }

    float t = 0;
    bool endOfUseSkill = false;

    private void Update()
    {
        if (endOfUseSkill)
        {
            t += Time.deltaTime;
            PHPCTR.Instance.rotBar.fillAmount = 1 - (t / cooldown);

            if (t >= cooldown)
            {
                t = 0;
                endOfUseSkill = false;
                useSkill = false;
            }
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
             
                endOfUseSkill = true;
                PHPCTR.Instance.rotBar.fillAmount = 1f;
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
