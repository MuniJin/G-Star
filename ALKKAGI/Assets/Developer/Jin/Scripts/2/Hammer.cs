using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private float throwSpeed = 120f;
    private bool isThrown;

    private Quaternion originalRotation;
    private Vector3 attackingRotation;
    private Vector3 originPosition;

    public GameObject player;
    private BoxCollider bc;

    void Start()
    {
        originalRotation = transform.rotation;
        originPosition = transform.localPosition;
        bc = this.GetComponent<BoxCollider>();
        isThrown = false;
    }

    public void ThrowHammer(Vector3 direction)
    { 
        isThrown = true;

        this.transform.LookAt(direction * -1);

        this.transform.parent = null;
        
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.AddForce((direction  - this.gameObject.transform.position).normalized * throwSpeed, ForceMode.Impulse);

        bc.isTrigger = true;
    }

    public void ReturnHammer()
    {
        isThrown = false;

        bc.isTrigger = false;

        this.transform.rotation = originalRotation;
        this.transform.parent = player.transform.GetChild(2);
        this.transform.rotation = player.transform.rotation * originalRotation;
        this.transform.position = player.transform.position;
        this.transform.localPosition = originPosition;

        Rigidbody rb =  GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        Destroy(rb);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
            ReturnHammer();
    }
}
