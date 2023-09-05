using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // 이동 속도를 설정할 변수

    private void Update()
    {
        // 이동 방향을 설정합니다. 여기서는 오른쪽으로 이동하는 예시입니다.
        Vector3 moveDirection = Vector3.right;

        // 이동 거리를 설정합니다.
        float moveDistance = moveSpeed * Time.deltaTime;

        // 현재 위치에서 이동 방향으로 이동 거리만큼 이동합니다.
        transform.position += moveDirection * moveDistance;
    }
}
