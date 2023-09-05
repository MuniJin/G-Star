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
        // �̵� ������ �����մϴ�. �̰� ��ī��
        Vector3 moveDirection = Vector3.right;

        // �̵� �Ÿ��� �����մϴ�.
        float moveDistance = MoveSpeed * Time.deltaTime;

        // ���� ��ġ���� �̵� �������� �̵� �Ÿ���ŭ �̵��մϴ�.
        transform.position += moveDirection * moveDistance;
        while (MoveTrue)
        {
            yield return 0;
        }
    }
}
