using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private void Start()
    {
        Invoke("Des", 0.5f);
    }

    private void Des()
    {
        Destroy(this.gameObject);
    }
}
