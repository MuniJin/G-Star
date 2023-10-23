using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomPosManager : MonoBehaviour
{
    public TMP_Text[] PiecesNum;
    public GameObject[] RedPieces;
    public GameObject GM;
    public GameObject PosMan;

    private void Update()
    {
        if (PiecesNum[0].text == "0" && PiecesNum[1].text == "0" && PiecesNum[2].text == "0" && PiecesNum[3].text == "0" && PiecesNum[4].text == "0" && PiecesNum[5].text == "0" && PiecesNum[6].text == "0")
        {
            SettingEnd();
        }
    }

    private void SettingEnd()
    {
        PosMan.GetComponent<Position>().BST = Random.Range(1, 5);
        PosMan.GetComponent<Position>().BlueSettingSelect();
        PosMan.GetComponent<Position>().AutoPosBlue();
        PosMan.GetComponent<Position>().BlueSetting();
        GM.GetComponent<AlKKAGIManager>().PieceSet();
        this.gameObject.SetActive(false);
    }
}
