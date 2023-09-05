using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    public GameObject DragObj;
    public float MoveSpeed;
    private bool MoveTrue = false;

    private void Start()
    {
        MoveTrue = true;
        StartCoroutine(MoveStart());
    }

    private void PieceMoveMent()
    {
        MoveSpeed =  DragObj.GetComponent<AlggagiDrag>().ShootPower;
        MoveStart();
    }
    IEnumerator MoveStart()
    {
        Debug.Log("start");
        // 이동 방향을 설정합니다. 이거 어카지
        Vector3 moveDirection = Vector3.right;

        // 이동 거리를 설정합니다.
        float moveDistance = MoveSpeed * Time.deltaTime;

        // 현재 위치에서 이동 방향으로 이동 거리만큼 이동합니다.
        transform.position += moveDirection * moveDistance;
        while (MoveTrue)
        {
            yield return 0;
        }
    }
}
