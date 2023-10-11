using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit!");

        if(other.tag == "Enemy")
        {
            Debug.Log(other.name);

            TestEnemyHp go = other.gameObject.transform.parent.gameObject.GetComponent<TestEnemyHp>();

            go.SetHp(10);
            Debug.Log(go.GetHp());
            if (go.GetHp() <= 0)
                FPSManager.Instance.Win();
        }

        Destroy(this.gameObject);
    }
}
