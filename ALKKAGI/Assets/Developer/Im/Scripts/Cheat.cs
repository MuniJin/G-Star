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

    public void Random() //·£´ý
    {
        GM.GetComponent<AlKKAGIManager>().CheatMode = 0;
    }
    public void AllWin() //Ç×»ó ½Â¸®
    {
        GM.GetComponent<AlKKAGIManager>().CheatMode = 1;
    }
    public void AllDefeat() //Ç×»ó ÆÐ¹è
    {
        GM.GetComponent<AlKKAGIManager>().CheatMode = 2;
    }
}
