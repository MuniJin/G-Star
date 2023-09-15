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
            // 충돌한 물체의 위치
            Vector3 hitPoint = hitInfo.point;

            // 충돌한 물체의 노멀 벡터
            Vector3 hitNormal = hitInfo.normal;

            // 충돌한 물체의 게임 오브젝트
            GameObject hitObject = hitInfo.collider.gameObject;
        }
        yield return 0;
    }
    public void BlueMove()
    {
        StartCoroutine("RaySet") ;
    }
}