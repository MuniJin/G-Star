using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : Decorator_Character
{
    private float cooldown;
    private bool useSkill = false;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private float radius = 60f;
    private LayerMask detectionLayer;

    private float duration;

    private GameObject UseSkillParticle;

    private void Start()
    {
        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            detectionLayer = LayerMask.GetMask("Enemy");
            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();
            detectionLayer = LayerMask.GetMask("Player");
            Destroy(pScript);
        }

        UseSkillParticle = Resources.Load<GameObject>("Particles\\Gravity");
        cooldown = this.GetCoolDown();
        duration = 3f;
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

            GameObject g = Instantiate(UseSkillParticle, go.transform.position + (Vector3.down * 3f), UseSkillParticle.transform.rotation);

            if (go.tag == "Player")
            {
                Collider[] colliders = Physics.OverlapSphere(go.transform.position, radius, detectionLayer);

                foreach (Collider collider in colliders)
                {
                    collider.gameObject.transform.parent.GetComponent<Enemy_Character>().speed /= 3f;
                    collider.gameObject.transform.parent.GetComponent<Enemy_Character>().jumpForce = 1f;
                    StartCoroutine(RestoreSpeed(collider.gameObject.transform.parent.gameObject));
                }

                endOfUseSkill = true;
                PHPCTR.Instance.rotBar.fillAmount = 1f;
            }
            else if (go.tag == "Enemy")
            {
                Collider[] colliders = Physics.OverlapSphere(go.transform.position, radius, detectionLayer);

                foreach (Collider collider in colliders)
                {
                    collider.gameObject.transform.parent.GetComponent<Player_Character>().speed /= 3f;
                    collider.gameObject.transform.parent.GetComponent<Player_Character>().jumpForce = 1f;
                    StartCoroutine(RestoreSpeed(collider.gameObject.transform.parent.gameObject));
                }

            }

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }

    private IEnumerator RestoreSpeed(GameObject go)
    {
        yield return new WaitForSeconds(duration);

        if (go.GetComponent<Player_Character>() != null)
        {
            go.GetComponent<Player_Character>().speed = 15;
            go.GetComponent<Player_Character>().jumpForce = 10;
        }
        else if (go.GetComponent<Enemy_Character>() != null)
        {
            go.GetComponent<Enemy_Character>().speed = 15;
            go.GetComponent<Enemy_Character>().jumpForce = 10;
        }
    }
}
