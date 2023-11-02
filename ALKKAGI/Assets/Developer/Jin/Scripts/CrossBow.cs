using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    public float rotationSpeed = 100f;  // ���� �ӵ�

    void Update()
    {
        if (transform.parent != null)
        {
            // ������ �����ϴ� �ڵ�
            transform.RotateAround(transform.parent.position, Vector3.right, rotationSpeed);
        }
        else
        {
            Debug.LogWarning("Target not assigned. Please assign a target for orbiting.");
        }
    }
}
