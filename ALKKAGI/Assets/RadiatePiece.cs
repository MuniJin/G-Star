using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatePiece : MonoBehaviour
{
    private List<GameObject> ps;

    private void Start()
    {
        Invoke("KS", 0.5f);
        Invoke("DS", 5f);
    }

    private void KS()
    {
        foreach (Transform c in this.transform)
        {
            //Bullet b = c.gameObject.AddComponent<Bullet>();
            //b.damage = 10;
            c.gameObject.GetComponent<Rigidbody>().AddForce(c.forward * 10f, ForceMode.Impulse);
        }
    }

    private void DS()
    {
        Destroy(this.gameObject);
    }
}
