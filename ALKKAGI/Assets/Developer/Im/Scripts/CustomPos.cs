using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomPos : MonoBehaviour
{
    private CustomPosManager CusManager;
    private Vector3 ReturnPos;
    private int piecesNum;
    public GameObject ParentObjR;
    public GameObject ErrorBox;
    public TMP_Text ErrorLog;

    private void Start()
    {
        CusManager = GameObject.Find("Custom_Setting").GetComponent<CustomPosManager>();
        ReturnPos = this.gameObject.transform.position;
    }
    private void PrintError(int ErrorNum)
    {
        ErrorBox.SetActive(true);
        if (ErrorNum == 0) 
            ErrorLog.text = "범위를 벗어났습니다";
        if (ErrorNum == 1) 
            ErrorLog.text = "이미 배치된 위치입니다";
        Invoke("CloseErrorBox",1f);
    }
    private void CloseErrorBox()
    {
        if (ErrorBox.activeSelf == true)
            ErrorBox.SetActive(false);
    }

    void OnMouseDrag()
    {
        if (ErrorBox.activeSelf == true)
            ErrorBox.SetActive(false);
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); //마우스 포지션 가져오기
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos); //오브젝트 포지션에 마우스 포지션을 대입
        objPos.y = 0.5f;
        transform.position = objPos;
    }


    private void OnMouseUp()
    {
        int CloseX = Mathf.RoundToInt(this.gameObject.transform.position.x / 2.0f) * 2;
        int CloseZ = Mathf.RoundToInt(this.gameObject.transform.position.z / 2.0f) * 2;

        if (this.gameObject.transform.position.z > -11f || this.gameObject.transform.position.z < -18f || this.gameObject.transform.position.x < 0 || this.gameObject.transform.position.x > 16)
        {
            PrintError(0);
            this.gameObject.transform.position = ReturnPos;
        }
        else
        {
            if (CusManager.LocalPos[CloseX / 2, -1 * (CloseZ + 12) / 2] != 1)
            {
                if (this.gameObject.name == "King_Red")
                {
                    if (CusManager.PiecesNum[0].text != "0")
                    {
                        CusManager.LocalPos[CloseX / 2, -1 * (CloseZ + 12) / 2] = 1;
                        GameObject newPiece = Instantiate(CusManager.RedPieces[0], new Vector3(CloseX, 0, CloseZ), Quaternion.identity);
                        newPiece.transform.SetParent(ParentObjR.transform);
                        int.TryParse(CusManager.PiecesNum[0].text, out piecesNum);
                        CusManager.PiecesNum[0].text = (piecesNum - 1).ToString();

                        if (CusManager.PiecesNum[0].text == "0")
                            Destroy(this.gameObject);
                        else
                            this.gameObject.transform.position = ReturnPos;
                    }
                }
                if (this.gameObject.name == "Horse_Red")
                {
                    if (CusManager.PiecesNum[1].text != "0")
                    {
                        CusManager.LocalPos[CloseX / 2, -1 * (CloseZ + 12) / 2] = 1;
                        GameObject newPiece = Instantiate(CusManager.RedPieces[1], new Vector3(CloseX, 0, CloseZ), Quaternion.identity);
                        newPiece.transform.SetParent(ParentObjR.transform);
                        int.TryParse(CusManager.PiecesNum[1].text, out piecesNum);
                        CusManager.PiecesNum[1].text = (piecesNum - 1).ToString();

                        if (CusManager.PiecesNum[1].text == "0")
                            Destroy(this.gameObject);
                        else
                            this.gameObject.transform.position = ReturnPos;
                    }
                }
                if (this.gameObject.name == "Elephant_Red")
                {
                    if (CusManager.PiecesNum[2].text != "0")
                    {
                        CusManager.LocalPos[CloseX / 2, -1 * (CloseZ + 12) / 2] = 1;
                        GameObject newPiece = Instantiate(CusManager.RedPieces[2], new Vector3(CloseX, 0, CloseZ), Quaternion.identity);
                        newPiece.transform.SetParent(ParentObjR.transform);
                        int.TryParse(CusManager.PiecesNum[2].text, out piecesNum);
                        CusManager.PiecesNum[2].text = (piecesNum - 1).ToString();

                        if (CusManager.PiecesNum[2].text == "0")
                            Destroy(this.gameObject);
                        else
                            this.gameObject.transform.position = ReturnPos;
                    }
                }
                if (this.gameObject.name == "Chariot_Red")
                {
                    if (CusManager.PiecesNum[3].text != "0")
                    {
                        CusManager.LocalPos[CloseX / 2, -1 * (CloseZ + 12) / 2] = 1;
                        GameObject newPiece = Instantiate(CusManager.RedPieces[3], new Vector3(CloseX, 0, CloseZ), Quaternion.identity);
                        newPiece.transform.SetParent(ParentObjR.transform);
                        int.TryParse(CusManager.PiecesNum[3].text, out piecesNum);
                        CusManager.PiecesNum[3].text = (piecesNum - 1).ToString();

                        if (CusManager.PiecesNum[3].text == "0")
                            Destroy(this.gameObject);
                        else
                            this.gameObject.transform.position = ReturnPos;
                    }
                }
                if (this.gameObject.name == "Cannon_Red")
                {
                    if (CusManager.PiecesNum[4].text != "0")
                    {
                        CusManager.LocalPos[CloseX / 2, -1 * (CloseZ + 12) / 2] = 1;
                        GameObject newPiece = Instantiate(CusManager.RedPieces[4], new Vector3(CloseX, 0, CloseZ), Quaternion.identity);
                        newPiece.transform.SetParent(ParentObjR.transform);
                        int.TryParse(CusManager.PiecesNum[4].text, out piecesNum);
                        CusManager.PiecesNum[4].text = (piecesNum - 1).ToString();

                        if (CusManager.PiecesNum[4].text == "0")
                            Destroy(this.gameObject);
                        else
                            this.gameObject.transform.position = ReturnPos;
                    }
                }
                if (this.gameObject.name == "Guard_Red")
                {
                    if (CusManager.PiecesNum[5].text != "0")
                    {
                        CusManager.LocalPos[CloseX / 2, -1 * (CloseZ + 12) / 2] = 1;
                        GameObject newPiece = Instantiate(CusManager.RedPieces[5], new Vector3(CloseX, 0, CloseZ), Quaternion.identity);
                        newPiece.transform.SetParent(ParentObjR.transform);
                        int.TryParse(CusManager.PiecesNum[5].text, out piecesNum);
                        CusManager.PiecesNum[5].text = (piecesNum - 1).ToString();

                        if (CusManager.PiecesNum[5].text == "0")
                            Destroy(this.gameObject);
                        else
                            this.gameObject.transform.position = ReturnPos;

                    }
                }
                if (this.gameObject.name == "Solider_Red")
                {
                    if (CusManager.PiecesNum[6].text != "0")
                    {
                        CusManager.LocalPos[CloseX / 2, -1 * (CloseZ + 12) / 2] = 1;
                        GameObject newPiece = Instantiate(CusManager.RedPieces[6], new Vector3(CloseX, 0, CloseZ), Quaternion.identity);
                        newPiece.transform.SetParent(ParentObjR.transform);
                        int.TryParse(CusManager.PiecesNum[6].text, out piecesNum);
                        CusManager.PiecesNum[6].text = (piecesNum - 1).ToString();

                        if (CusManager.PiecesNum[6].text == "0")
                            Destroy(this.gameObject);
                        else
                            this.gameObject.transform.position = ReturnPos;
                    }
                }
            }
            else
            {
                PrintError(1);
                this.gameObject.transform.position = ReturnPos;
            }
        }
    }
}