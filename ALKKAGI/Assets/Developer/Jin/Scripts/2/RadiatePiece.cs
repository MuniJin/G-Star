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
            c.gameObject.GetComponent<Rigidbody>().AddForce(c.forward * 20f, ForceMode.Impulse);
    }

    private void DS() => Destroy(this.gameObject);
}
