using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void Start()
    {
        Invoke("DelBul", 3f);
    }

    private void DelBul()
    {
        Destroy(this.gameObject);
    }
}
