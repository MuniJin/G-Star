using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private bool hasCollided = false; // 충돌 여부를 추적

    private void CheckTag(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameObject go = other.transform.parent.gameObject;
            go.GetComponent<Enemy_Character>().Hitted(damage);
        }
        else if (other.tag == "Player")
        {
            GameObject go = other.transform.parent.gameObject;
            go.GetComponent<Player_Character>().Hitted(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "Hammer")
        {
            if (other.tag != "Bullet")
                CheckTag(other);

            return;
        }

        if (hasCollided) // 이미 충돌한 경우
        {
            Destroy(gameObject); // 총알 오브젝트를 파괴

            return;
        }

        if (this.gameObject.tag != other.tag)
        {
            if (other.tag != "Bullet")
            {
                CheckTag(other);

                // 법선 벡터 계산
                Vector3 normal = other.transform.position - this.transform.position;
                normal.Normalize();

                // 입사각 계산
                float angleOfIncidence = Vector3.Angle(normal, GetComponent<Rigidbody>().velocity.normalized);

                // 반사각 계산
                float angleOfReflection = 180 - angleOfIncidence;

                // 총알의 속도를 반사 방향으로 수정
                GetComponent<Rigidbody>().velocity = Vector3.Reflect(GetComponent<Rigidbody>().velocity, normal);

                hasCollided = true;
            }
        }
    }
}