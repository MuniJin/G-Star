using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public Transform SkillPos;
    public string parentPlayer; // 플레이어랑 충돌 주의
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
            return;


        CheckTag(other);

        ReturnBulPos();
    }
    private void CheckTag(Collider other)
    {
        if (other.tag == "LimitArea")
            return;
        if (other.tag == parentPlayer)
            return;

        if (other.tag == "Player")
        {
            GameObject go = other.transform.parent.gameObject;
            go.GetComponent<Player_Character>().Hitted(damage);
        }
        else if (other.tag == "Enemy")
        {
            GameObject go = other.transform.parent.gameObject;
            go.GetComponent<Enemy_Character>().Hitted(damage);
        }
        else
            Debug.Log("지형지물 맞음");
    }

    private void ReturnBulPos()
    {
        if (this.GetComponent<Explode>() != null)
            this.GetComponent<Explode>().Explosion(this.transform.position);

        this.transform.parent = SkillPos.transform;
        this.transform.position = SkillPos.transform.position;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
       
        this.gameObject.SetActive(false);
    }
}