using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    private GameObject GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("AlKKAGIManager");
    }
    public void Random()
    {
        GM.GetComponent<AlKKAGIManager>().CheatMode = 0;
    }
    public void AllWin()
    {
        GM.GetComponent<AlKKAGIManager>().CheatMode = 1;
    }
    public void AllDefeat()
    {
        GM.GetComponent<AlKKAGIManager>().CheatMode = 2;
    }
}
