using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHammer : MonoBehaviour
{
    public Hammer hammer;
    public GameObject shield;

    private bool isZoom;

    private void Start()
    {
        cam = Camera.main;
        ShowCursor();
        isZoom = false;
    }

    private void Update()
    {
        Move();
        CamMove();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        if (Input.GetMouseButtonDown(1))
            HammerAttack();
        if(!isZoom)
        {
            if(Input.GetMouseButtonDown(0))
            {
                
            }
        }
        if(isZoom)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isZoom = false;
                cam.fieldOfView *= 3;
                Vector3 throwDir = Vector3.zero;

                

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    throwDir = hit.point;

                hammer.ThrowHammer(throwDir);
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


    float h, v;

    protected void Move()
    {
        CamMove();
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.forward * v + transform.right * h;

        this.transform.position += dir * 8f * Time.deltaTime;
    }

    private float mouseX, mouseY;
    private float eulerAngleX, eulerAngleY;

    private float rotCamX = 5f;
    private float rotCamY = 3f;

    private float limitMinX = -90f;
    private float limitMaxX = 90f;

    private Camera cam;

    public float sensitivity = 0.5f;

    private void CamMove()
    {
        cam.transform.position = this.gameObject.transform.position;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        eulerAngleX -= mouseY * rotCamX * sensitivity;
        eulerAngleY += mouseX * rotCamY * sensitivity;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        this.transform.rotation = Quaternion.Euler(0f, eulerAngleY, 0f);
        cam.transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0f);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }

    private void ShowCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
