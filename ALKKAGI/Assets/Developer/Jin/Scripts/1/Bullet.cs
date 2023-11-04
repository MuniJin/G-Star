using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Transform bulPos;

    private Quaternion originRot;

    void Start()
    {
        originRot = this.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.name.Split('(')[0] == "Arrow")
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Bullets\\ArrowStuck"), this.transform.position, this.transform.rotation);
        }
        
        CheckTag(other);

        ReturnBulPos();
    }

    private void CheckTag(Collider other)
    {
        if (other.tag == "LimitArea")
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
        else if (other.tag == "Bullet")
            Debug.Log("Bullet Hit");
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