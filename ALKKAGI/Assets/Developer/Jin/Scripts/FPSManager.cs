using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : Singleton<FPSManager>
{
    // 플레이어, AI의 스폰 위치
    public GameObject mySpawnPoint;
    public GameObject enemySpawnPoint;
    
    // 기본 초기화 작업
    public void Init(GameObject myPiece, GameObject enemyPiece)
    {
        GameObject myP = Instantiate(myPiece, mySpawnPoint.transform.position, Quaternion.identity);
        GameObject enemyP = Instantiate(enemyPiece, enemySpawnPoint.transform.position, Quaternion.identity);

        myP.AddComponent<Player_Character>();
        // FPS 적 AI 추가

        myP.tag = "Player";
    }

    // 게임 결과 판정
    public void CheckGameResult(GameObject p, GameObject e)
    {
        Debug.Log(p.GetComponent<Player_Character>()._hp);
    }

}
