
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject GM;
    GameObject collisionObj;

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionObj != collision.gameObject)
        {
            collisionObj = collision.gameObject;
            if (collisionObj.tag == "RedPiece")
            {
                if (collisionObj.name == "Solider_Red(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(1);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Cannon_Red(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(2);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Chariot_Red(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(3);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Elephant_Red(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(4);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Horse_Red(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(5);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Guard_Red(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(6);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "King_Red(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().GameOver(0);
                    Invoke("die", 2f);

                }
            }

            if (collisionObj.tag == "BluePiece")
            {
                if (collisionObj.name == "Solider_Blue(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(7);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Cannon_Blue(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(8);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Chariot_Blue(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(9);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Elephant_Blue(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(10);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Horse_Blue(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(11);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "Guard_Blue(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().Death(12);
                    Invoke("die", 2f);

                }
                if (collisionObj.name == "King_Blue(Clone)")
                {
                    GM.GetComponent<AlKKAGIManager>().GameOver(1);
                    Invoke("die", 2f);
                }
            }
        }
    }
    private void die()
    {
        GM.GetComponent<AlKKAGIManager>().CrashObjB = null;
        GM.GetComponent<AlKKAGIManager>().CrashObjR = null;
        Destroy(collisionObj);
    }
}



