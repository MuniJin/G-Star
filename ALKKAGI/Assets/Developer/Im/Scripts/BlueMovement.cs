using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    // 5. 승패확인

    //충돌 -> 둘의 시작 좌표값저장 -> 충돌로 인한 운동 -> 승패확인 -> 승리시, 일반진행

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