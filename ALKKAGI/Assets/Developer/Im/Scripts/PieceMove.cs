using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    public GameObject DragObj; //�巡�׵Ǵ� ������Ʈ(Trigger ������Ʈ)
    private Rigidbody rb; // �ش� ������Ʈ�� ������ٵ�
    public Vector3 Arrow; //�̵�����
    public float MoveSpeed; //�̵��ӵ�
    private bool RotateZero = false; //�⹰�� ���Ⱑ 0,0,0�� �´��� Ȯ���ϴ� ����


    private void Start()
    {
        RotateZero = true; // ó���� 0,0,0�� �⺻���̱⿡ true
        rb = this.gameObject.GetComponent<Rigidbody>(); //������Ʈ�� ������ٵ� �ڵ����� �־��ֱ�
        MoveStart(); //test code
    }

    private void Update()
    {
        if (!RotateZero)
        {
            RotationReset();
        }
    }

    private void RotationReset() //���� �ʱ�ȭ
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveStart() //�⹰ �̵�
    {
        MoveSpeed = DragObj.GetComponent<AlggagiDrag>().ShootPower;
        rb.AddForce(Arrow* MoveSpeed, ForceMode.Impulse);
    }

    
}
