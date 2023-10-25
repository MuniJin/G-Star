using UnityEngine;
using UnityEngine.UI;
public class PHPCTR : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;
    [SerializeField]
    private Text hpText;


    private float MaxHp = 100;
    private float CurHp = 100;
    float imsi;

    // Start is called before the first frame update
    void Start()
    {
        imsi = (float)CurHp / (float)MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CurHp > 0)
            {
                CurHp -= 10;
            }
            else
            {
                CurHp = 0;
                
            }

            imsi = (float)CurHp / (float)MaxHp;
            UpdateHpText(); // 추가된 부분
        }

        HandleHp();
    }

    private void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, imsi, Time.deltaTime * 10);
    }

    private void UpdateHpText()
    {
        hpText.text = "HP: " + CurHp.ToString(); // HP 값 텍스트로 업데이트
    }
}
