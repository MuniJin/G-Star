using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class senstive : MonoBehaviour
{
    public Slider sensitivitySlider; // 슬라이더를 연결하기 위한 public 변수
    public float minSensitivity = 1.0f; // 최소 감도
    public float maxSensitivity = 10.0f; // 최대 감도

    void Start()
    {
        sensitivitySlider.onValueChanged.AddListener(ChangeMouseSensitivity);
    }

    void ChangeMouseSensitivity(float value)
    {
        float sensitivity = Mathf.Lerp(minSensitivity, maxSensitivity, value);
        // 여기서 sensitivity를 사용하여 마우스 감도를 조절하세요.
    }
}
