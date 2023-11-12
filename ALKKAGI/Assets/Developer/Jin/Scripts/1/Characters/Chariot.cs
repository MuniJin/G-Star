using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chariot : Decorator_Character
{
    private float cooldown;
    private bool useSkill = false;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private GameObject UseSkillParticle;

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

        UseSkillParticle = Resources.Load<GameObject>("Particles\\Booster");
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

            Vector3 forwardDirection = Vector3.zero;
            if (go.tag == "Player")
                forwardDirection = Camera.main.transform.forward;
            else if (go.tag == "Enemy")
                forwardDirection = go.transform.forward;


            GameObject g = Instantiate(UseSkillParticle, go.transform.position, Quaternion.identity);
            g.transform.SetParent(go.transform);
            g.transform.SetAsLastSibling();
            g.transform.localPosition = new Vector3(0f, -1.5f, -1f);
            if (go.tag == "Player")
                g.transform.rotation = new Quaternion(0f, 180f, 0f, g.transform.rotation.w);

            go.GetComponent<Rigidbody>().AddForce(forwardDirection * 30f, ForceMode.Impulse);
         
            yield return new WaitForSeconds(3f);
            
            endOfUseSkill = true;
            PHPCTR.Instance.rotBar.fillAmount = 1f;
        }
    }
}
