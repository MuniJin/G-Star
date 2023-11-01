using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PosSelect : MonoBehaviour
{
    [SerializeField] private GameObject PosObj;
    [SerializeField] private GameObject errorbox;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text InsText;
    int seltype = 0;

    private void Update()
    {
        if (seltype == 1)
        {
            InsText.text = "�޻�����";
        }
        if (seltype == 2)
        {
            InsText.text = "����������";
        }
        if (seltype == 3)
        {
            InsText.text = "�Ȼ�����";
        }
        if (seltype == 4)
        {
            InsText.text = "�ٱ�������";
        }
    }

    public void InsertType()
    {
        if (seltype > 0 || seltype < 5)
        {       PosObj.GetComponent<Position>().SetType = seltype;
        PosObj.GetComponent<Position>().SelPannel.SetActive(false);
        PosObj.GetComponent<Position>().GAS();
        }
        else
        {
            errorbox.SetActive(true);
            errorText.text = "�������� �����Ͽ� �ֽʽÿ�.";
        }
    }

    public void Select1() //�޻�
    {
        seltype = 1;
    }
    public void Select2() //������
    {
        seltype = 2;
    }
    public void Select3() //�Ȼ�
    {
        seltype = 3;
    }
    public void Select4() //�ٱ���
    {
        seltype = 4;
    }
}
