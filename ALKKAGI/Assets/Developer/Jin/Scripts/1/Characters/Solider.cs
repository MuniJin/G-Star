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
            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
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
        if (!useSkill)
        {
            useSkill = true;
            FPSManager.Instance.SkillSoundPlay(go.tag);

            if (go.tag == "Player")
            {
                pScript.speed *= 2f;

                yield return new WaitForSeconds(cooldown / 2);
                pScript.speed /= 2f;

                endOfUseSkill = true;
                PHPCTR.Instance.rotBar.fillAmount = 1f;
            }
            else if (go.tag == "Enemy")
            {

                FPSManager.Instance.SkillSoundPlay(go.tag);
                eScript.speed *= 2f;

                yield return new WaitForSeconds(cooldown / 2);
                eScript.speed /= 2f;

                yield return new WaitForSeconds(cooldown);
                useSkill = false;
            }
        }
    }
}