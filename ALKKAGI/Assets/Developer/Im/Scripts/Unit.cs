using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Unit : MonoBehaviour
{
    public GameObject DirSys;
    public GameObject PowerSys;
    public GameObject MyGameObj;
    public GameObject CrashObj;
    public GameObject GameManager;

    public AudioClip ShootSound;
    public AudioClip CrashSound;

    private AudioSource myAudioSource;

    private bool IsWin = false;
    private bool IsDir = false;
    private bool IsPower = false;
    private bool IsMyTurn = false;
    private bool IsPieceSelected = false;

    public Vector3 MyPieceLocal = new Vector3(0, 0, 0);
    public Vector3 BluePieceLocal = new Vector3(0, 0, 0);

    private void Start()
    {
        IsMyTurn = true;
        MyGameObj = GetComponent<GameObject>();
        myAudioSource = GetComponent<AudioSource>();
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
        }
    }
    private void shoot()
    {
        if (IsPieceSelected) //�⹰�� ���õǾ��ٸ�
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsDir == false && IsPower == false) //�����̽��ٸ� �����ٸ�
            {
                //�������� �ý����� ���� Ȱ��ȭ �ȴ�.
                DirSys.SetActive(true);
                IsDir = true;
                if (Input.GetKeyDown(KeyCode.Space) && IsDir == true && IsPower == false) //�����̽��ٸ� �ٽ��ѹ� �����ٸ�
                {
                    //������ Ȯ���ȴ�

                    //����ý����� ��Ȱ��ȭ�ȴ�
                    DirSys.SetActive(false);
                    //�Ŀ��ý����� Ȱ��ȭ�ȴ�.
                    PowerSys.SetActive(true);
                    IsPower = true;
                    if (Input.GetKeyDown(KeyCode.Space) && IsDir == true && IsPower == true) //�����̽��ٸ� �ٽ��ѹ� �����ٸ�
                    {
                        //�Ŀ��� Ȯ���ȴ�.

                        //ƨ��� �Ҹ� ���
                        myAudioSource.PlayOneShot(ShootSound);
                        //�߻�(�̵�)

                        //�� �� ����
                        IsMyTurn = false;
                    }
                }
            }
        }
    }

    private void FPSResult()
    {
        if (IsWin)
        {
            //CrashObj�� ƨ���� ������

        }
        else
        {
            //���� �ݴ�������� ƨ���� ������

        }
    }

    //�浹üũ
    private void OnCollisionEnter(Collision collision)
    {
        //�浹�� ���
        myAudioSource.PlayOneShot(CrashSound);

        //�浹�� ��ġ�� ����
        MyPieceLocal = MyGameObj.transform.position;
        CrashObj = collision.gameObject;
        BluePieceLocal = CrashObj.transform.position;


        //�浹�� �⹰���� �±� �Ѱ��ֱ�

        //GameManager.GetComponent<GameManager>().CrashCheck(); // ũ���� üũ�� MyPieceLocal BluePieceLocal�� �������� �Լ� �����
        if (collision.gameObject.tag == "Solider")
        {

        }
        if (collision.gameObject.tag == "Cannon")
        {

        }
        if (collision.gameObject.tag == "Horse")
        {

        }
        if (collision.gameObject.tag == "Chariot")
        {

        }
        if (collision.gameObject.tag == "Elephant")
        {

        }
        if (collision.gameObject.tag == "Guard")
        {

        }
        if (collision.gameObject.tag == "King")
        {

        }
    }
}
