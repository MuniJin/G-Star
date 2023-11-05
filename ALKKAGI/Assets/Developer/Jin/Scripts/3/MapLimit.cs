using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MapLimit : MonoBehaviour
{
    public Image LimitText;

    private void Start()
    {
        if (LimitText.gameObject.activeInHierarchy == true)
            LimitText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            LimitText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            LimitText.gameObject.SetActive(false);
    }

}
