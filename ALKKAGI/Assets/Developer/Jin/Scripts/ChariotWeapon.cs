using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotWeapon : MonoBehaviour
{
    public Hammer hammer;
    public Shield shield;

    private bool isZoom;
    private Camera cam;
    private float eTimer;

    private void Start()
    {
        cam = Camera.main;
        isZoom = false;

        eTimer = 0f;
    }
    private void Update()
    {
        if (this.tag == "Enemy")
        {
            eTimer += Time.deltaTime;

            if (eTimer >= 3f)
            {
                GameObject p = GameObject.FindWithTag("Player");
                Debug.Log(Vector3.Distance(this.transform.position, p.transform.position));

                hammer.ThrowHammer(p.transform.position);
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
                    shield.ShieldSlam();
                }
            }
            if (isZoom)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isZoom = false;
                    cam.fieldOfView *= 3;

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
    }
}
