using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabsc : MonoBehaviour
{
    public GameObject Tabui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Tabui.gameObject.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            Tabui.gameObject.SetActive(false);
        }
    }
}
