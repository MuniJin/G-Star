using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Skills sm;
    
    private Vector3 position;
    public float speed;

    private void Start()
    {
        speed = 5f;
        sm = Skills.Instance;
    }

    public void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Q))
            Skill();
        if (Input.GetKeyDown(KeyCode.E))
            this.transform.Rotate(Vector3.up, 10f);
    }


    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        this.transform.position += new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
    }

    public void Skill()
    {
        string getName = this.gameObject.name;
        Debug.Log(getName);
        switch(getName)
        {
            case "Pawn":
                StartCoroutine(sm.Skill_Pawn());
                break;
            case "Rook":
                StartCoroutine(sm.Skill_Rook());
                break;
            case "Cannon":
                // ���콺 ��ġ�� ��ũ�� ��ǥ�� ������
                Vector3 mousePosition = Input.mousePosition;
                Debug.Log(mousePosition);
                // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
                mousePosition.z = 10f; // ī�޶󿡼� �ָ� ������ ��ġ�� ����
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                StartCoroutine(sm.Skill_Cannon(worldPosition));
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
}
