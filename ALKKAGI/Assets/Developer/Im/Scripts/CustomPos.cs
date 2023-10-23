using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPos : MonoBehaviour
{
    private CustomPosManager CusManager;
    private Vector3 ReturnPos;
    private int piecesNum;
    private void Start()
    {
        CusManager = GameObject.Find("Custom_Setting").GetComponent<CustomPosManager>();
        ReturnPos = this.gameObject.transform.position;
    }

    void OnMouseDrag()
    {

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); //마우스 포지션 가져오기
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos); //오브젝트 포지션에 마우스 포지션을 대입
        objPos.y = 0.5f;
        transform.position = objPos;

       
    }


    private void OnMouseUp()
    {
        if (this.gameObject.transform.position.z > -11f || this.gameObject.transform.position.z < -18f || this.gameObject.transform.position.x < 0 || this.gameObject.transform.position.x > 16)
        {
            Debug.Log(this.gameObject.transform.position);
            Debug.Log(" 범위 벗어남 ");
            this.gameObject.transform.position = ReturnPos;
        }
        else
        {
            if (this.gameObject.name == "King_Red")
            {
                if (CusManager.PiecesNum[0].text != "0")
                {
                    GameObject newPiece = Instantiate(CusManager.RedPieces[0], this.gameObject.transform.position, Quaternion.identity);
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
                    GameObject newPiece = Instantiate(CusManager.RedPieces[1], this.gameObject.transform.position, Quaternion.identity);
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
                    GameObject newPiece = Instantiate(CusManager.RedPieces[2], this.gameObject.transform.position, Quaternion.identity);
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
                    GameObject newPiece = Instantiate(CusManager.RedPieces[3], this.gameObject.transform.position, Quaternion.identity);
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
                    GameObject newPiece = Instantiate(CusManager.RedPieces[4], this.gameObject.transform.position, Quaternion.identity);
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
                    GameObject newPiece = Instantiate(CusManager.RedPieces[5], this.gameObject.transform.position, Quaternion.identity);
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
                    GameObject newPiece = Instantiate(CusManager.RedPieces[6], this.gameObject.transform.position, Quaternion.identity);
                    int.TryParse(CusManager.PiecesNum[6].text, out piecesNum);
                    CusManager.PiecesNum[6].text = (piecesNum - 1).ToString();

                    if (CusManager.PiecesNum[6].text == "0")
                        Destroy(this.gameObject);
                    else
                        this.gameObject.transform.position = ReturnPos;
                }
            }
        }
    }
}