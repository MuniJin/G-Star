using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBook : MonoBehaviour
{
    private void Start()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
    }
    void Update()
    {
        this.transform.Rotate(new Vector3(0f, -2f, 0f));
    }
}
