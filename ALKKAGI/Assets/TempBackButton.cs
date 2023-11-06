using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBackButton : MonoBehaviour
{
    [SerializeField] private GameObject GM;
    public void BackButton()
    {
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        GM.GetComponent<AlKKAGIManager>().audioManager.GetComponent<AudioManager>().SFXSource.PlayOneShot(GM.GetComponent<AlKKAGIManager>().audioManager.GetComponent<AudioManager>().ButtonSound);

        clickedButton.transform.parent.gameObject.SetActive(false);
    }

}
