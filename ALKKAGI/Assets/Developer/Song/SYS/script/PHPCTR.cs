using UnityEngine;
using UnityEngine.UI;
public class PHPCTR : Singleton<PHPCTR>
{
    [SerializeField]
    private Slider hpbar;
    [SerializeField]
    private Text hpText;

    private float MaxHp = 100;
    private float CurHp = 100;
    float imsi;

    public Image skillImg;
    public Image rotBar;

    void Update()
    {
        HandleHp();

        UpdateHpText();
    }

    private void HandleHp()
    {
        imsi = CurHp / MaxHp;
        hpbar.value = Mathf.Lerp(hpbar.value, imsi, Time.deltaTime * 10);
    }

    private void UpdateHpText()
    {
        hpText.text = "HP: " + CurHp.ToString();
    }

    public void SetHpUI(float _hp)
    {
        MaxHp = _hp;
        CurHp = _hp;

        UpdateHpText();
    }

    public void PlayerHitted(float damage)
    {
        CurHp -= damage;
    }
}
