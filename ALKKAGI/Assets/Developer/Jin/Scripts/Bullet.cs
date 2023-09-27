using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            Destroy(this.gameObject);

            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<TestEnemyHp>()._hp -= 10;

                if (other.gameObject.GetComponent<TestEnemyHp>()._hp == 0)
                    Debug.Log("Player Win");
            }
        }
    }
}
