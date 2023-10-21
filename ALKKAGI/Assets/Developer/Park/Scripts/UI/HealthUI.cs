using UnityEngine;
using UnityEngine.UI; // UI ���� ����� ����ϱ� ���� �߰�

public class HealthUI : MonoBehaviour
{
    private Health playerHealth; // Health ��ũ��Ʈ�� �����ϱ� ���� ����
    public Text hpText; // UI Text ������Ʈ�� ������ ����

    private void Start()
    {
        // Player �������� ã�ƿɴϴ�. �� �� "Player"�� �������� �̸��̾�� �մϴ�.
        GameObject playerPrefab = GameObject.FindWithTag("Player");
        // Player �����տ��� Health ��ũ��Ʈ�� ã�ƿɴϴ�.
        playerHealth = playerPrefab.GetComponent<Health>();

        if (playerHealth == null)
        {
            Debug.LogError("Player �����տ��� Health ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
        // Text UI ������Ʈ�� �Ҵ�
        hpText = GetComponent<Text>();
    }

    private void Update()
    {
        // Health ��ũ��Ʈ���� ���� HP ���� ������ UI Text�� ǥ��
        if (playerHealth != null && hpText != null)
        {
            hpText.text = "HP: " + playerHealth.GetCurrentHealth().ToString(); // HP ���� ���ڿ��� ��ȯ�Ͽ� ǥ��
        }
    }
}
