using UnityEngine;
using UnityEngine.UI; // UI 관련 기능을 사용하기 위해 추가

public class HealthUI : MonoBehaviour
{
    private Health playerHealth; // Health 스크립트에 접근하기 위한 변수
    public Text hpText; // UI Text 컴포넌트를 연결할 변수

    private void Start()
    {
        // Player 프리팹을 찾아옵니다. 이 때 "Player"는 프리팹의 이름이어야 합니다.
        GameObject playerPrefab = GameObject.FindWithTag("Player");
        // Player 프리팹에서 Health 스크립트를 찾아옵니다.
        playerHealth = playerPrefab.GetComponent<Health>();

        if (playerHealth == null)
        {
            Debug.LogError("Player 프리팹에서 Health 스크립트를 찾을 수 없습니다.");
        }
        // Text UI 컴포넌트를 할당
        hpText = GetComponent<Text>();
    }

    private void Update()
    {
        // Health 스크립트에서 현재 HP 값을 가져와 UI Text에 표시
        if (playerHealth != null && hpText != null)
        {
            hpText.text = "HP: " + playerHealth.GetCurrentHealth().ToString(); // HP 값을 문자열로 변환하여 표시
        }
    }
}
