using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSet : MonoBehaviour
{
    [SerializeField] private GameObject PosManager;
    private void OnMouseDown()
    {
        if (this.gameObject.name == "�޻�����")
        {
            PosManager.GetComponent<PosSelect>().Select1();
        }
        else if (this.gameObject.name == "����������")
        {
            PosManager.GetComponent<PosSelect>().Select2();
        }
        else if (this.gameObject.name == "�Ȼ�����")
        {
            PosManager.GetComponent<PosSelect>().Select3();
        }
        else if (this.gameObject.name == "�ٱ�������")
        {
            PosManager.GetComponent<PosSelect>().Select4();
        }
    }
}
