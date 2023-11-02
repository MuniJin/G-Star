using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    public float rotationSpeed = 100f;  // 공전 속도

    void Update()
    {
        if (transform.parent != null)
        {
            // 주위를 공전하는 코드
            transform.RotateAround(transform.parent.position, Vector3.right, rotationSpeed);
        }
        else
        {
            Debug.LogWarning("Target not assigned. Please assign a target for orbiting.");
        }
    }
}
