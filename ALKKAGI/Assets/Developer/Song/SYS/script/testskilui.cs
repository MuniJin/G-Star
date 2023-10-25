using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Testskilui : MonoBehaviour
{
    public Image image;
    public float duration = 6.0f;


    private void Awake()
    {
        image.enabled = false;
    }
    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(ChangeFillAmountOverTime());
        }
    }

    private IEnumerator ChangeFillAmountOverTime()
    {
        image.enabled = true;

        float currentTime = 0.0f;
        float startFillAmount = 1.0f;
        float endFillAmount = 0.0f;

        while (currentTime < duration)
        {
            float fillAmount = Mathf.Lerp(startFillAmount, endFillAmount, currentTime / duration);
            fillAmount = Mathf.Clamp01(fillAmount);

            image.fillAmount = fillAmount;
            currentTime += Time.deltaTime;

            yield return null;
        }

        image.fillAmount = endFillAmount;
    }
}
