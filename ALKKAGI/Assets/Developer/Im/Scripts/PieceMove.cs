using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    public GameObject DragObj; //드래그되는 오브젝트(Trigger 오브젝트)
    private Rigidbody rb; // 해당 오브젝트의 리지드바디
    public Vector3 Arrow; //이동방향
    public float MoveSpeed; //이동속도
    private bool RotateZero = false; //기물의 기울기가 0,0,0이 맞는지 확인하는 변수


    private void Start()
    {
        RotateZero = true; // 처음엔 0,0,0이 기본값이기에 true
        rb = this.gameObject.GetComponent<Rigidbody>(); //오브젝트의 리지드바디를 자동으로 넣어주기
        MoveStart(); //test code
    }

    private void Update()
    {
        if (!RotateZero)
        {
            RotationReset();
        }
    }

    private void RotationReset() //기울기 초기화
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveStart() //기물 이동
    {
        MoveSpeed = DragObj.GetComponent<AlggagiDrag>().ShootPower;
        rb.AddForce(Arrow* MoveSpeed, ForceMode.Impulse);
    }

    
}
