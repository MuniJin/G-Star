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

    public void BlueMove()
    {
        GM.GetComponent<GameManager>().CrashObjR.GetComponent<PieceMove>().SaveSpeed = this.gameObject.GetComponent<Rigidbody>().velocity;

    }

    private void OnCollisionEnter(Collision collision)
    {
      //if (collision.gameObject.tag == "BluePiece" && this.gameObject.tag == "RedPiece" && GM.GetComponent<GameManager>().CrashObjB != collision.gameObject)
      //{
      //    GameObject collidedObject = collision.gameObject;
      //
      //    GM.GetComponent<GameManager>().CrashObjR = this.gameObject;
      //    GM.GetComponent<GameManager>().CrashObjB = collidedObject;
      //    GM.GetComponent<GameManager>().RedPieceLocal = this.gameObject.transform.position;
      //    GM.GetComponent<GameManager>().BluePieceLocal = collidedObject.transform.position;
      //
      //    // Save the velocity before setting it to zero
      //    SaveSpeed = rb.velocity;
      //
      //    // Set the velocity of both objects to zero
      //    rb.velocity = Vector3.zero;
      //    GM.GetComponent<GameManager>().CrashObjB.GetComponent<Rigidbody>().velocity = Vector3.zero;
      //
      //    RotationReset();
      //
      //    GM.GetComponent<GameManager>().FPSResult();
      //}
    }
}