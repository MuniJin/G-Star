using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{

    [SerializeField] Text textHp;
    //[SerializeField] Text textCooltime;

    static int MaxHp = 100;
    //static int MaxCoolTime = 10;

    int currentHP = 10;
    

    void StartStat()
    {
        //textHp.text = currentHP.ToString();
    }
    private void Start()
    {
        StartStat();
    }

    private void StartManager(int hp)
    {
        if(currentHP <= MaxHp)
        {
            currentHP += hp;
            if (currentHP > MaxHp)
                currentHP = MaxHp;
        }
        HpText();
    }

    void HpText()
    {
        textHp.text = currentHP.ToString();
    }
    
}
