using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    private GameObject GM;

    void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
    }

    public void Random() //����
    {
        GM.GetComponent<AlKKAGIManager>().CheatMode = 0;
    }
    public void AllWin() //�׻� �¸�
    {
        GM.GetComponent<AlKKAGIManager>().CheatMode = 1;
    }
    public void AllDefeat() //�׻� �й�
    {
        GM.GetComponent<AlKKAGIManager>().CheatMode = 2;
    }
}
