using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Startbuttonanim : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        myButton.onClick.AddListener(MoveButtonDelayed);
    }

    public void MoveButtonDelayed()
    {
        Invoke("MoveButton", 1f);
    }
    public void MoveButton()
    {
        // ��ư�� ���� ��ġ�� ������ ��, ���ο� ��ġ�� ����
        RectTransform rt = myButton.GetComponent<RectTransform>();
        Vector3 newPosition = new Vector3(-300f, -800f, 0f); // �����ϰ� ���� ���ο� ��ġ
        rt.anchoredPosition = newPosition;
    }
}
