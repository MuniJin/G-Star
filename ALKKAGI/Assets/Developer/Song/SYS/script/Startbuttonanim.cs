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
        // 버튼의 현재 위치를 가져온 후, 새로운 위치로 변경
        RectTransform rt = myButton.GetComponent<RectTransform>();
        Vector3 newPosition = new Vector3(-300f, -800f, 0f); // 변경하고 싶은 새로운 위치
        rt.anchoredPosition = newPosition;
    }
}
