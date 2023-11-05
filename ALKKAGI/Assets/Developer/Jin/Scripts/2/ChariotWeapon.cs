using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotWeapon : MonoBehaviour
{
    public Hammer hammer;

    private bool isZoom;
    private Camera cam;
    private float eTimer;

    private void Start()
    {
        cam = Camera.main;
        isZoom = false;

        eTimer = 0f;
    }

    private float angle = 0f;

    private void Update()
    {
        if (this.tag == "Enemy")
        {
            eTimer += Time.deltaTime;

            if (eTimer >= 3f)
            {
                GameObject p = GameObject.FindWithTag("Player");
                
                float dist = Vector3.Distance(this.transform.position, p.transform.position);

                angle = dist / 20 - 1f;
                float additionalDistance = dist * Mathf.Tan(Mathf.Deg2Rad * angle);

                float r1, r2, r3;
                r1 = Random.Range(-2f, 5f);
                r2 = Random.Range(-2f, 5f);
                r3 = Random.Range(-2f, 5f);

                hammer.ThrowHammer(p.transform.position + new Vector3(r1, additionalDistance + r2, r3));
                eTimer = 0f;
            }
        }

        if (this.tag == "Player")
        {
            if (Input.GetMouseButtonDown(1))
                HammerAttack();

            if (!isZoom)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    
                }
            }
            if (isZoom)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    HammerAttack();

                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    Vector3 throwDir = Vector3.zero;

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        throwDir = hit.point;

                    hammer.ThrowHammer(throwDir);
                }
            }
        }
    }

    private void HammerAttack()
    {
        if (!isZoom)
        {
            isZoom = true;
            cam.fieldOfView /= 3;
        }
        else
        {
            isZoom = false;
            cam.fieldOfView *= 3;
        }

        FPSManager.Instance.ScopeOnOff();
    }
}
