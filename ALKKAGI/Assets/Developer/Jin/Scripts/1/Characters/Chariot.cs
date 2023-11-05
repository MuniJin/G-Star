using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chariot : Decorator_Character
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

        cooldown = this.GetCoolDown();
    }

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            Debug.Log("UseSkill");
            useSkill = true;
            Vector3 forwardDirection = Vector3.zero;
            if (go.tag == "Player")
                forwardDirection = Camera.main.transform.forward;
            else if (go.tag == "Enemy")
                forwardDirection = go.transform.forward;

            go.GetComponent<Rigidbody>().AddForce(forwardDirection * 30f, ForceMode.Impulse);

            yield return new WaitForSeconds(cooldown);
            Debug.Log("EndSkill");
            useSkill = false;
        }
    }
}
