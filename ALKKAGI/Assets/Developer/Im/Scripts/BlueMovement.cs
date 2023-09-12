using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    // 5. ����Ȯ��

    private Vector3 MoveDis;
    public float DisX;
    public float DisZ;
    public float ShootPower = 0f;
    public float Pita = 0f;
    public float MoveSpeed; //�̵��ӵ�
    private Vector3 direction;
    public GameObject GM;

    private void Start()
    {
        GM = GameObject.Find("GameManager"); 
    }

    public void BlueMove()
    {
        MoveSpeed = UnityEngine.Random.Range(2f, 4f);
        this.gameObject.GetComponent<Rigidbody>().AddForce(direction * MoveSpeed, ForceMode.Impulse);
    }

    private void PowerMath()
    {
        //RED�� ����
        //�ش� �⹰�� �ڽ��� �⹰�� �Ÿ� ����
        //�ش� �⹰�� ���ߴ� ���� ����
        //�Ÿ��� ���� �� �� ����
        
    }

    private void MoveMath()
    {
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //�巡�� �� �Ÿ���
        ShootPower = ((float)Math.Floor(Pita * 100) / 100) * 2; //�ӵ���
        direction = new Vector3(DisX, 0, DisZ);
    }
}