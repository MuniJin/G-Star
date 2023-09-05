using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlggagiDrag : MonoBehaviour
{
    // 1. ���ּ���
    // 2. ���⼱��
    // 3. ������
    // 4. �浹Ȯ��
    // 5. ����Ȯ��
    // 6. �浹Ȯ��(�ܺ�) 3�ʵ� ����

    //�浹 -> ���� ���� ��ǥ������ -> �浹�� ���� � -> ����Ȯ�� -> �¸���, �Ϲ�����
    public bool IsMyTurn = false;
    private bool IsPieceSelected = false;
    private Vector3 MoveDis;
    private float DisX;
    private float DisZ;
    public float ShootPower = 0f;
    public float Pita = 0f;

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
    private void OnMouseUp()
    {
        if (IsPieceSelected)
        {
            MoveDis = this.gameObject.transform.localPosition;
            this.gameObject.transform.localPosition = new Vector3(0,0,0);
            DisX = MoveDis.x;
            DisZ = MoveDis.z;
            MoveMath();
            IsPieceSelected = false;
        }
    }
    private void MoveMath()
    {
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�巡�� �� �Ÿ���
        ShootPower = ((float)Math.Floor(Pita * 100) / 100)*2; //�ӵ���
    }
}
