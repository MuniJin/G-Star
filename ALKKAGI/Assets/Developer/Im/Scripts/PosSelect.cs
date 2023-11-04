using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PosSelect : MonoBehaviour
{
    [SerializeField] private GameObject PosObj;
    [SerializeField] private GameObject errorbox;
    [SerializeField] private GameObject[] SelectedBox;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text InsText;
    [SerializeField] private Material BlackWood;
    [SerializeField] private Material Wood;
    int seltype = 0;

    public void ShowSelected()
    {
        if (seltype == 1)
        {
            InsText.text = "�޻�����";
            SelectedBox[0].GetComponent<MeshRenderer>().material = BlackWood;
            SelectedBox[1].GetComponent<MeshRenderer>().material = Wood;
            SelectedBox[2].GetComponent<MeshRenderer>().material = Wood;
            SelectedBox[3].GetComponent<MeshRenderer>().material = Wood;
        }
        else if (seltype == 2)
        {
            InsText.text = "����������";
            SelectedBox[0].GetComponent<MeshRenderer>().material = Wood;
            SelectedBox[1].GetComponent<MeshRenderer>().material = BlackWood;
            SelectedBox[2].GetComponent<MeshRenderer>().material = Wood;
            SelectedBox[3].GetComponent<MeshRenderer>().material = Wood;
        }
        else if (seltype == 3)
        {
            InsText.text = "�Ȼ�����";
            SelectedBox[0].GetComponent<MeshRenderer>().material = Wood;
            SelectedBox[1].GetComponent<MeshRenderer>().material = Wood;
            SelectedBox[2].GetComponent<MeshRenderer>().material = BlackWood;
            SelectedBox[3].GetComponent<MeshRenderer>().material = Wood;
        }
        else if (seltype == 4)
        {
            InsText.text = "�ٱ�������";
            SelectedBox[0].GetComponent<MeshRenderer>().material = Wood;
            SelectedBox[1].GetComponent<MeshRenderer>().material = Wood;
            SelectedBox[2].GetComponent<MeshRenderer>().material = Wood;
            SelectedBox[3].GetComponent<MeshRenderer>().material = BlackWood;
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
