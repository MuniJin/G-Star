using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    public GameObject GM;
    private void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
    }
    IEnumerable RaySet()
    {
        RaycastHit hitInfo;

        Ray ray = new Ray();
        ray.origin = this.gameObject.transform.localPosition;
        ray.direction = this.transform.forward; 

        if (Physics.Raycast(ray, out hitInfo, 30))
        {
            // �浹�� ��ü�� ��ġ
            Vector3 hitPoint = hitInfo.point;

            // �浹�� ��ü�� ��� ����
            Vector3 hitNormal = hitInfo.normal;

            // �浹�� ��ü�� ���� ������Ʈ
            GameObject hitObject = hitInfo.collider.gameObject;
        }
        yield return 0;
    }
    public void BlueMove()
    {
        StartCoroutine("RaySet") ;
    }
}