using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private SphereCollider sc;

    private void Start()
    {
        sc = this.GetComponent<SphereCollider>();
    }

    public void Explosion()
    {
        sc.radius *= 3f;
    }
}
