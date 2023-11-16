using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cannon : Decorator_Character
{
    private float cooldown;
    private float damage;
    private bool useSkill = false;

    private LayerMask groundLayer;

    private GameObject basePlayer;

    private Player_Character pScript;
    private Enemy_Character eScript;

    private GameObject AtferCastingSkillParticle;

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");

        basePlayer = base.gameObject;
        if (basePlayer.CompareTag("Player"))
        {
            pScript = basePlayer.GetComponent<Player_Character>();
            damage = this.GetDamage();

            Destroy(eScript);
        }
        else if (basePlayer.CompareTag("Enemy"))
        {
            eScript = basePlayer.GetComponent<Enemy_Character>();

            Destroy(pScript);
        }
        AtferCastingSkillParticle = Resources.Load<GameObject>("Particles\\Teleportation");
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
        if (basePlayer.tag == "Player")
        {
            if (!useSkill)
            {
                useSkill = true;
                while (true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("show");

                        Cursor.lockState = CursorLockMode.Locked; // ���콺�� �߾ӿ� ����
                        Cursor.visible = false; // ���콺�� ������ �ʰ� ����

                        useSkill = false;
                        break;
                    }
                    
                    Cursor.lockState = CursorLockMode.None; // ���콺�� �߾ӿ� ����
                    Cursor.visible = true; // ���콺�� ������ �ʰ� ����

                    if (Input.GetMouseButtonDown(1))
                    {
                        // ���콺 ��ġ�� ��ũ�� ��ǥ���� ���� ��ǥ�� ��ȯ�մϴ�.
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        // ����ĳ��Ʈ�� �߻��Ͽ� �浹�� ��ü�� �����մϴ�.
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                        {
                            // �浹�� ������ ����3 ���� ����ϴ�.
                            Vector3 hitPoint = hit.point;

                            // hitPoint ������ ����Ͽ� ���ϴ� �۾��� ������ �� �ֽ��ϴ�.
                            go.transform.position = hitPoint + Vector3.up;

                            GameObject p = Instantiate(AtferCastingSkillParticle, go.transform.position + (Vector3.down * 0.75f), AtferCastingSkillParticle.transform.rotation);

                            Cursor.lockState = CursorLockMode.Locked; // ���콺�� �߾ӿ� ����
                            Cursor.visible = false; // ���콺�� ������ �ʰ� ����
                        }
                        endOfUseSkill = true;
                        PHPCTR.Instance.rotBar.fillAmount = 1f;
                        break;
                    }

                    yield return null;
                }
            }
        }
        if (basePlayer.tag == "Enemy")
        {
            if (!useSkill)
            {
                useSkill = true;
                float rayLength = 100f;

                Vector3 randomDirection = Random.onUnitSphere;

                Ray ray = new Ray(transform.position, randomDirection);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, rayLength, groundLayer))
                {
                    Vector3 hitPoint = hit.point;

                    go.transform.position = hitPoint + Vector3.up;

                    GameObject p = Instantiate(AtferCastingSkillParticle, go.transform.position + (Vector3.down * 0.75f), AtferCastingSkillParticle.transform.rotation);
                }
            }

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}
