using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    public GameObject ALM;

    void Start()
    {
        ALM = GameObject.Find("AlKKAGIManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Win();
        if (Input.GetKeyDown(KeyCode.P))
            Lose();
    }

    void Win()
    {
        SceneManager.LoadScene("Board");
        ALM.GetComponent<AlKKAGIManager>().BoardObj.SetActive(true);
        ALM.GetComponent<AlKKAGIManager>().IsWin = true;
        ALM.GetComponent<AlKKAGIManager>().FPSResult();
    }
    void Lose()
    {
        SceneManager.LoadScene("Board");
        ALM.GetComponent<AlKKAGIManager>().BoardObj.SetActive(true);
        ALM.GetComponent<AlKKAGIManager>().IsWin = false;
        ALM.GetComponent<AlKKAGIManager>().FPSResult();
    }
}
