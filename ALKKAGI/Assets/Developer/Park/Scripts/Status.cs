using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [SerializeField] Text textHp;

    static float MaxHp = 100;
    private float currentHealth =100;

    private void Start()
    {
        StartStat();
    }

    void StartStat()
    {
        textHp.text = currentHealth.ToString();
    }

    // HP를 변경하는 메서드
    public void ChangeHP(float hpChange)
    {
        currentHealth += hpChange;

        // 최대 HP를 넘지 않도록 체크
        if (currentHealth > MaxHp)
            currentHealth = MaxHp;

        // 최소 HP는 0 미만이 되지 않도록 체크
        if (currentHealth < 0)
            currentHealth = 0;

        HpText();
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

    void HpText()
    {
        textHp.text = currentHealth.ToString();
    }
    private void Die()
    {
        // 플레이어가 죽었을 때 실행할 동작을 추가합니다.
        // 여기서는 간단하게 게임 오브젝트를 비활성화합니다.
        gameObject.SetActive(false);
    }
}

