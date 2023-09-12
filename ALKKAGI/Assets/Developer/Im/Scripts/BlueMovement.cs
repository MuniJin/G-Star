using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : MonoBehaviour
{
    // 5. 승패확인

    private Vector3 MoveDis;
    public float DisX;
    public float DisZ;
    public float ShootPower = 0f;
    public float Pita = 0f;
    public float MoveSpeed; //이동속도
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
        //RED팀 지정
        //해당 기물과 자신의 기물의 거리 측정
        //해당 기물을 맞추는 방향 설정
        //거리에 따라 줄 힘 설정
        
    }

    private void MoveMath()
    {
        Pita = (float)Math.Sqrt(DisX * DisX + DisZ * DisZ); //드래그 한 거리값
        ShootPower = ((float)Math.Floor(Pita * 100) / 100) * 2; //속도값
        direction = new Vector3(DisX, 0, DisZ);
    }
}