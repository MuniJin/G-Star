using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    // 5. ����Ȯ��

    //�浹 -> ���� ���� ��ǥ������ -> �浹�� ���� � -> ����Ȯ�� -> �¸���, �Ϲ�����

    public GameObject GM;

    private void Start()
    {
        GM = GameObject.Find("GameManager");
    }
    private void BlueTurn()
    {
        BlueMove();
        GM.GetComponent<GameManager>().IsMyTurn = true;
    }
    private void BlueMove()
    {

    }
}