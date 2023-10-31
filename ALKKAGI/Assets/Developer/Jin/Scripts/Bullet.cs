using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private bool hasCollided = false; // �浹 ���θ� ����

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

        if (hasCollided) // �̹� �浹�� ���
        {
            Destroy(gameObject); // �Ѿ� ������Ʈ�� �ı�

            return;
        }

        if (this.gameObject.tag != other.tag)
        {
            if (other.tag != "Bullet")
            {
                CheckTag(other);

                // ���� ���� ���
                Vector3 normal = other.transform.position - this.transform.position;
                normal.Normalize();

                // �Ի簢 ���
                float angleOfIncidence = Vector3.Angle(normal, GetComponent<Rigidbody>().velocity.normalized);

                // �ݻ簢 ���
                float angleOfReflection = 180 - angleOfIncidence;

                // �Ѿ��� �ӵ��� �ݻ� �������� ����
                GetComponent<Rigidbody>().velocity = Vector3.Reflect(GetComponent<Rigidbody>().velocity, normal);

                hasCollided = true;
            }
        }
    }
}