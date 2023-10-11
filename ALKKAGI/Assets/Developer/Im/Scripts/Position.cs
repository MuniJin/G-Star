using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    //player is red
    //기물을 배치해주는 스크립트

    private int[,] LocalPos = new int[10, 9]; //빈칸 0, 1~14 기물
    private int Auto = 0;

    public int SetType = 0; //상차림 타입 변수
    public int BST = 0; //BlueSetType 랜덤변수
    public GameObject[] pieces; //빨 -> 파 / 졸 -> 포 -> 차 -> 상 -> 마 -> 사 -> 궁
    public GameObject SelPannel;
    public GameObject DeathRange;
    public GameObject ParentObjR;
    public GameObject ParentObjB;
    public GameObject GM;
    public GameObject ClassicButton;
    public GameObject[] SetButtons;

    private void Start()
    {
        GameStart();
    }
    public void Classic()
    {
        ClassicButton.SetActive(true);
        SetButtons[0].SetActive(false);
        SetButtons[1].SetActive(false);
    }

    public  void Custom()
    {

        SetButtons[0].SetActive(false);
        SetButtons[1].SetActive(false);
    }
    private void GameStart()
    {
        BST = Random.Range(1, 5);
        if (Auto == 0) //오토세팅
        {
            AutoPos();
            SelPannel.SetActive(true);
            DeathRange.SetActive(true);
        }
        else //플레이어가 직접 세팅
        {
            AutoPosBlue();
            BlueSettingSelect();
            PosSetting();
            DeathRange.SetActive(true);
        }
    }

    public void GAS()
    {
        SettingSelect();
        BlueSettingSelect();
        PosSetting();
    }

    private void PosSetting()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (LocalPos[i, j] > 0 && LocalPos[i, j] <= 14) 
                {
                    GameObject newPiece = Instantiate(pieces[LocalPos[i, j]], new Vector3(j * 2, 0f, i * -2), Quaternion.identity);

                    // 아래 라인을 사용하여 ParentObj를 부모로 설정합니다.
                    if (LocalPos[i, j] > 0 && LocalPos[i, j] <= 7)
                        newPiece.transform.SetParent(ParentObjR.transform);
                    if (LocalPos[i, j] > 7 && LocalPos[i, j] <= 14)
                        newPiece.transform.SetParent(ParentObjB.transform);
                }
            }
        }
        GM.GetComponent<AlKKAGIManager>().PieceSet();
    }
    private void BlueSettingSelect()
    {
        //Blue Elephant[11] && Horse[12]
        if (BST == 1)  //상마상마
        {
            LocalPos[0, 1] = 11;
            LocalPos[0, 2] = 12;
            LocalPos[0, 6] = 11;
            LocalPos[0, 7] = 12;
        }
        if (BST == 2) //마상마상
        {
            LocalPos[0, 1] = 12;
            LocalPos[0, 2] = 11;
            LocalPos[0, 6] = 12;
            LocalPos[0, 7] = 11;
        }
        if (BST == 3) //상마마상
        {
            LocalPos[0, 1] = 12;
            LocalPos[0, 2] = 11;
            LocalPos[0, 6] = 11;
            LocalPos[0, 7] = 12;

        }
        if (BST == 4) //마상상마
        {
            LocalPos[0, 1] = 11;
            LocalPos[0, 2] = 12;
            LocalPos[0, 6] = 12;
            LocalPos[0, 7] = 11;

        }
    }

    private void SettingSelect()//상차림 버튼 활성화 1, 2, 3, 4 {상마상마 / 마상마상 / 마상상마 / 상마마상}
    {
        //red Elephant[4] && Horse[5]
        if (SetType == 1)
        {
            LocalPos[9, 1] = 4;
            LocalPos[9, 2] = 5;
            LocalPos[9, 6] = 4;
            LocalPos[9, 7] = 5;
        }
        if (SetType == 2)
        {
            LocalPos[9, 1] = 5;
            LocalPos[9, 2] = 4;
            LocalPos[9, 6] = 5;
            LocalPos[9, 7] = 4;
        }
        if (SetType == 3)
        {
            LocalPos[9, 1] = 5;
            LocalPos[9, 2] = 4;
            LocalPos[9, 6] = 4;
            LocalPos[9, 7] = 5;

        }
        if (SetType == 4)
        {
            LocalPos[9, 1] = 4;
            LocalPos[9, 2] = 5;
            LocalPos[9, 6] = 5;
            LocalPos[9, 7] = 4;

        }
    }

    private void AutoPos()
    {
        AutoPosBlue();
        
        //red Solider
        LocalPos[6, 0] = 1;
        LocalPos[6, 2] = 1;
        LocalPos[6, 4] = 1;
        LocalPos[6, 6] = 1;
        LocalPos[6, 8] = 1;
        //red Cannon 
        LocalPos[7, 1] = 2;
        LocalPos[7, 7] = 2;
        //red King
        LocalPos[8, 4] = 7;
        //red Guard
        LocalPos[9, 3] = 6;
        LocalPos[9, 5] = 6;
        //red Chariot
        LocalPos[9, 0] = 3;
        LocalPos[9, 8] = 3;
    }
    
    private void AutoPosBlue()
    {
        //blue Chariot
        LocalPos[0, 0] = 10;
        LocalPos[0, 8] = 10;
        //blue Guard
        LocalPos[0, 3] = 13;
        LocalPos[0, 5] = 13;
        //blue King
        LocalPos[1, 4] = 14;
        //blue Cannon
        LocalPos[2, 1] = 9;
        LocalPos[2, 7] = 9;
        //blue Solider
        LocalPos[3, 0] = 8;
        LocalPos[3, 2] = 8;
        LocalPos[3, 4] = 8;
        LocalPos[3, 6] = 8;
        LocalPos[3, 8] = 8;
        LocalPos[3, 8] = 8;

    }
}
