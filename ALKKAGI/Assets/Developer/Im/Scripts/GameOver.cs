using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{   
    public void lose()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "<color=blue>�й�!";
    }

    public void vic()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "<color=red>�¸�!";
    }
}
