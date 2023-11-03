using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSet : MonoBehaviour
{
    [SerializeField] private GameObject PosManager;
    [SerializeField] private GameObject GM;
    private void OnMouseDown()
    {
        if (this.gameObject.name == "�޻�����")
        {
            PosManager.GetComponent<PosSelect>().Select1();
            PosManager.GetComponent<PosSelect>().ShowSelected();
            StartCoroutine(GM.GetComponent<AlKKAGIManager>().SoundPlay(3));
        }
        else if (this.gameObject.name == "����������")
        {
            PosManager.GetComponent<PosSelect>().Select2();
            PosManager.GetComponent<PosSelect>().ShowSelected();
            StartCoroutine(GM.GetComponent<AlKKAGIManager>().SoundPlay(3));
        }
        else if (this.gameObject.name == "�Ȼ�����")
        {
            PosManager.GetComponent<PosSelect>().Select3();
            PosManager.GetComponent<PosSelect>().ShowSelected();
            StartCoroutine(GM.GetComponent<AlKKAGIManager>().SoundPlay(3));
        }
        else if (this.gameObject.name == "�ٱ�������")
        {
            PosManager.GetComponent<PosSelect>().Select4();
            PosManager.GetComponent<PosSelect>().ShowSelected();
            StartCoroutine(GM.GetComponent<AlKKAGIManager>().SoundPlay(3));
        }
    }
}
