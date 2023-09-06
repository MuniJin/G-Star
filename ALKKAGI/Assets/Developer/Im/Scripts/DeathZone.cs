using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject GM;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("show;");
        GameObject collisionObj = collision.gameObject;
        
        if (collisionObj.tag == "RedPiece")
        {
            Debug.Log("show;");
            if (collisionObj.name == "Solider_Red")
            {
                GM.GetComponent<GameManager>().Death(1);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Cannon_Red")
            {
                GM.GetComponent<GameManager>().Death(2);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Chariot_Red")
            {
                GM.GetComponent<GameManager>().Death(3);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Elephant_Red")
            {
                GM.GetComponent<GameManager>().Death(4);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Horse_Red")
            {
                GM.GetComponent<GameManager>().Death(5);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Guard_Red")
            {
                GM.GetComponent<GameManager>().Death(6);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "King_Red")
            {
                GM.GetComponent<GameManager>().GameOver(0);
                Destroy(collisionObj);
            }
        }

        if (collisionObj.tag == "BluePiece")
        {
            if (collisionObj.name == "Solider_Blue")
            {
                GM.GetComponent<GameManager>().Death(7);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Cannon_Blue")
            {
                GM.GetComponent<GameManager>().Death(8);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Chariot_Blue")
            {
                GM.GetComponent<GameManager>().Death(9);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Elephant_Blue")
            {
                GM.GetComponent<GameManager>().Death(10);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Horse_Blue")
            {
                GM.GetComponent<GameManager>().Death(11);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "Guard_Blue")
            {
                GM.GetComponent<GameManager>().Death(12);
                Destroy(collisionObj);
            }
            if (collisionObj.name == "King_Blue")
            {
                GM.GetComponent<GameManager>().GameOver(1);
                Destroy(collisionObj);
            }
        }
    }

}
