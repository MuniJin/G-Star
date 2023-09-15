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

    // HP�� �����ϴ� �޼���
    public void ChangeHP(float hpChange)
    {
        currentHealth += hpChange;

        // �ִ� HP�� ���� �ʵ��� üũ
        if (currentHealth > MaxHp)
            currentHealth = MaxHp;

        // �ּ� HP�� 0 �̸��� ���� �ʵ��� üũ
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
        // �÷��̾ �׾��� �� ������ ������ �߰��մϴ�.
        // ���⼭�� �����ϰ� ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        gameObject.SetActive(false);
    }
}

