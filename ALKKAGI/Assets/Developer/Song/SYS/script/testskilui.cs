using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testskilui : MonoBehaviour
{
    public Image fill;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(CoolTime(6f));
        }
    }

    IEnumerator CoolTime(float cool)
    {
        print("쿨타임 코루틴 실행");

        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            fill.fillAmount = (1.0f/cool );
            yield return new WaitForFixedUpdate();
        }

        print("쿨타임 코루틴 완료");
    }
}
//public UnityEngine.UI.Image fill;
//private float maxCooldown = 6f;
//private float currentCooldown = 6f;

//public void SetMaxCooldown(in float value)
//{
//    maxCooldown = value;
//    UpdateFiilAmount();
//}

//public void SetCurrentCooldown(in float value)
//{
//    currentCooldown = value;
//    UpdateFiilAmount();
//}

//private void UpdateFiilAmount()
//{
//    fill.fillAmount = currentCooldown / maxCooldown;
//}

//// Test
//private void Update()
//{

//    SetCurrentCooldown(currentCooldown - Time.deltaTime);

//    // Loop
//    if (currentCooldown < 0f)
//        currentCooldown = maxCooldown;


//}