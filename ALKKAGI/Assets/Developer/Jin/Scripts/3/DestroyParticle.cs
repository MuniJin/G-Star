using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    public float destroyTime;

    private void Start()
    {
        Invoke("Des", destroyTime);
    }

    private void Des()
    {
        Destroy(this.gameObject);
    }
}
