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
    public int[,] LocalPos = new int[9, 5]; // 0,0 -> 0 0 -12      // 8,3 -> 16 0 -18

    private void Update()
    {
        if (PiecesNum[0].text == "0" && PiecesNum[1].text == "0" && PiecesNum[2].text == "0" && PiecesNum[3].text == "0" &&
            PiecesNum[4].text == "0" && PiecesNum[5].text == "0" && PiecesNum[6].text == "0")
        {
            SettingEnd();
        }
    }

    public void SettingEnd()
    {
        PosMan.GetComponent<Position>().BST = Random.Range(1, 5);
        //PosMan.GetComponent<Position>().CustomPosBlue(1);
        PosMan.GetComponent<Position>().BlueSettingSelect();
        PosMan.GetComponent<Position>().AutoPosBlue();
        PosMan.GetComponent<Position>().BlueSetting();
        GM.GetComponent<AlKKAGIManager>().SFX.PlayOneShot(GM.GetComponent<AlKKAGIManager>().audioManager.GetComponent<AudioManager>().StartSound);
        GM.GetComponent<AlKKAGIManager>().PieceSet();
        this.gameObject.SetActive(false);
    }
}

