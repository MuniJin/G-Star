using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosSelect : MonoBehaviour
{
    public GameObject PosObj;
    int seltype = 0;
    
    private void InsertType()
    {
        PosObj.GetComponent<Position>().SetType = seltype;
        PosObj.GetComponent<Position>().SelPannel.SetActive(false);
        PosObj.GetComponent<Position>().GAS();
    }

    public void Select1() //¿Þ»ó
    {
        seltype = 1;
        InsertType();
    }
    public void Select2() //¿À¸¥»ó
    {
        seltype = 2;
        InsertType();
    }
    public void Select3() //¾È»ó
    {
        seltype = 3;
        InsertType();
    }
    public void Select4() //¹Ù±ù»ó
    {
        seltype = 4;
        InsertType();
    }
}
