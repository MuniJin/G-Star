using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject ps;

    public void Explosion(Vector3 boom)
    {
        Vector3 p = GameObject.FindWithTag("Player").transform.position;
        Vector3 dist = (boom - p).normalized;
        
        GameObject go = Instantiate(ps, boom - dist * 2, Quaternion.identity);
        go.transform.parent = null;
    }
}
