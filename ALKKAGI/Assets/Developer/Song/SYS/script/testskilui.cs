using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testskilui : MonoBehaviour
{
    public Image img_Skill;

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
            img_Skill.fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }

        print("쿨타임 코루틴 완료");
    }
}
