using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{   
    public void lose()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "<color=blue>ÆÐ¹è!";
    }

    public void vic()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "<color=red>½Â¸®!";
    }
}
