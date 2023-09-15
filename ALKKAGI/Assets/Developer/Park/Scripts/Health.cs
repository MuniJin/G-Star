using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // 플레이어가 죽었을 때 실행할 동작을 추가합니다.
        // 여기서는 간단하게 게임 오브젝트를 비활성화합니다.
        gameObject.SetActive(false);
    }
}

