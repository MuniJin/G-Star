using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Bullet")
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

        Destroy(this.gameObject);
    }
}
