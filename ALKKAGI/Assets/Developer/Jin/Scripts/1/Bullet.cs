using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Transform bulPos;

    private Quaternion originRot;

    public string parentPlayer;

    public int guardBuffDamage;

    void Start()
    {
        originRot = this.transform.rotation;
    }


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
            go.GetComponent<Player_Character>().Hitted(damage + guardBuffDamage);
        }
        else if (other.tag == "Enemy")
        {
            GameObject go = other.transform.parent.gameObject;
            go.GetComponent<Enemy_Character>().Hitted(damage + guardBuffDamage);
        }
        else
            Debug.Log("지형지물 맞음");
    }

    private void ReturnBulPos()
    {
        if (this.GetComponent<Explode>() != null)
            this.GetComponent<Explode>().Explosion(this.transform.position);

        this.transform.parent = bulPos.transform;
        this.transform.position = bulPos.transform.position;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.rotation = originRot;

        this.gameObject.SetActive(false);
    }
}