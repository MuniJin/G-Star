using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Alggagi : MonoBehaviour
{
    // 1. 유닛선택
    // 2. 방향선택
    // 3. 힘조절
    // 4. 충돌확인
    // 5. 승패확인
    // 6. 충돌확인(외벽) 3초뒤 삭제

    //충돌 -> -> 둘의 시작 좌표값저장 -> 충돌로 인한 운동 -> 승패확인 -> 승리시, 일반진행

    public Vector3 DefaultPos;

    private bool IsMyTurn = false;
    private bool IsPieceSelected = false;

    private void Start()
    {
        IsMyTurn = true;
    }

    void OnMouseDrag()
    {
        if (IsMyTurn)
        {
            float distance = Camera.main.WorldToScreenPoint(transform.position).z;
    
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
            objPos.y = -0.5f;
            transform.position = objPos;
    
            IsPieceSelected = true;
        }
    }
    
    private void PieceSelect()
    {
    
    }
}
