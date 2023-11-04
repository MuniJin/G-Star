using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBook : MonoBehaviour
{
    float r1, r2, r3;

    private void Start()
    {
        r1 = Random.Range(-2f, 3f);
        r2 = Random.Range(-2f, 3f);
        r3 = Random.Range(-2f, 3f);
    }

    void Update()
    {
        this.transform.Rotate(new Vector3(r1, r2, r3));
    }
}
