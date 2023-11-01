using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class senstive : MonoBehaviour
{
    public Slider sensitivitySlider; // �����̴��� �����ϱ� ���� public ����
    public float minSensitivity = 1.0f; // �ּ� ����
    public float maxSensitivity = 10.0f; // �ִ� ����

    void Start()
    {
        sensitivitySlider.onValueChanged.AddListener(ChangeMouseSensitivity);
    }

    void ChangeMouseSensitivity(float value)
    {
        float sensitivity = Mathf.Lerp(minSensitivity, maxSensitivity, value);
        // ���⼭ sensitivity�� ����Ͽ� ���콺 ������ �����ϼ���.
    }
}
