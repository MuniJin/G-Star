using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // �̵� �ӵ��� ������ ����

    private void Update()
    {
        // �̵� ������ �����մϴ�. ���⼭�� ���������� �̵��ϴ� �����Դϴ�.
        Vector3 moveDirection = Vector3.right;

        // �̵� �Ÿ��� �����մϴ�.
        float moveDistance = moveSpeed * Time.deltaTime;

        // ���� ��ġ���� �̵� �������� �̵� �Ÿ���ŭ �̵��մϴ�.
        transform.position += moveDirection * moveDistance;
    }
}
