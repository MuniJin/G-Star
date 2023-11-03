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
                    Cursor.lockState = CursorLockMode.None; // 마우스를 중앙에 고정
                    Cursor.visible = true; // 마우스를 보이지 않게 설정

                    if (Input.GetMouseButtonDown(1))
                    {
                        // 마우스 위치를 스크린 좌표에서 월드 좌표로 변환합니다.
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        // 레이캐스트를 발사하여 충돌한 객체를 감지합니다.
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                        {
                            // 충돌한 지점의 벡터3 값을 얻습니다.
                            Vector3 hitPoint = hit.point;

                            // hitPoint 변수를 사용하여 원하는 작업을 수행할 수 있습니다.
                            go.transform.position = hitPoint + Vector3.up;

                            GameObject p = Instantiate(particle, go.transform.position + (Vector3.down * 0.75f), particle.transform.rotation);

                            Cursor.lockState = CursorLockMode.Locked; // 마우스를 중앙에 고정
                            Cursor.visible = false; // 마우스를 보이지 않게 설정
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
