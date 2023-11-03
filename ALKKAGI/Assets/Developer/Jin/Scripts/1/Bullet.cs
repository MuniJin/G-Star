using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    //private bool hasCollided = false; // 충돌 여부를 추적

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "Hammer")
        {
            if (other.tag != "Bullet")  
                CheckTag(other);

            return;
        }

        CheckTag(other);

        if (this.name.Split('(')[0] == "Dynamite" || this.name.Split('(')[0] == "Solider")
            this.GetComponent<Explode>().Explosion();
        else
            Destroy(this.gameObject);
    }

    private void CheckTag(Collider other)
    {
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
}