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

    private GameObject particle;

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

        cooldown = this.GetCoolDown();
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

                            GameObject p = Instantiate(particle, go.transform.position + (Vector3.down * 0.75f), particle.transform.rotation);

                            Cursor.lockState = CursorLockMode.Locked; // ���콺�� �߾ӿ� ����
                            Cursor.visible = false; // ���콺�� ������ �ʰ� ����
                        }

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

                    GameObject p = Instantiate(particle, go.transform.position + (Vector3.down * 0.75f), particle.transform.rotation);
                }
            }
        }

        yield return new WaitForSeconds(cooldown);
        useSkill = false;
    }

    public override void MakeParticle(GameObject p) => particle = p;
}
