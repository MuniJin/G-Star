using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : Singleton<FPSManager>
{
    // �÷��̾�, AI�� ���� ��ġ
    public GameObject mySpawnPoint;
    public GameObject enemySpawnPoint;
    
    // �⺻ �ʱ�ȭ �۾�
    public void Init(GameObject myPiece, GameObject enemyPiece)
    {
        GameObject myP = Instantiate(myPiece, mySpawnPoint.transform.position, Quaternion.identity);
        GameObject enemyP = Instantiate(enemyPiece, enemySpawnPoint.transform.position, Quaternion.identity);

        myP.AddComponent<Player_Character>();
        // FPS �� AI �߰�

        myP.tag = "Player";
    }

    // ���� ��� ����
    public void CheckGameResult(GameObject p, GameObject e)
    {
        Debug.Log(p.GetComponent<Player_Character>()._hp);
    }

}
