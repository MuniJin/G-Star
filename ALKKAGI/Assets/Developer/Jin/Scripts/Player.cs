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
                // 마우스 위치를 스크린 좌표로 가져옴
                Vector3 mousePosition = Input.mousePosition;
                Debug.Log(mousePosition);
                // 마우스 위치를 월드 좌표로 변환
                mousePosition.z = 10f; // 카메라에서 멀리 떨어진 위치로 설정
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                StartCoroutine(sm.Skill_Cannon(worldPosition));
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
}
