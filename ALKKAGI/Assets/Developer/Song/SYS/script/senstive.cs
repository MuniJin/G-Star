using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class senstive : MonoBehaviour
{
    public Slider sensitivitySlider; // �����̴��� �����ϱ� ���� public 

    private void Start()
    {
        sensitivitySlider.value = Immortal.Instance.sensitivity;
    }

    private void Update()
    {
        Immortal.Instance.sensitivity = sensitivitySlider.value;
        if (sensitivitySlider.value < 0.01f)
            sensitivitySlider.value = 0.01f;
    }
}
