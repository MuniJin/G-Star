using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            TestEnemyHp go = other.gameObject.transform.parent.gameObject.GetComponent<TestEnemyHp>();

            go.SetHp(10);

            if (go.GetHp() <= 0)
                FPSManager.Instance.Win();
        }
        else if(other.tag == "Player")
        {
            Player_Character go = other.gameObject.GetComponent<Player_Character>();

            go.SetHp(10);

            if (go.GetHp() <= 0)
                FPSManager.Instance.Lose();
        }
        
        Destroy(this.gameObject);
    }
}
