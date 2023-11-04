using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private SphereCollider sc;
    private GameObject ps;

    private void Start()
    {
        sc = this.GetComponent<SphereCollider>();
        ps = Resources.Load<GameObject>("Bullets\\TinyFire");
    }

    public void Explosion()
    {
        Vector3 p = GameObject.FindWithTag("Player").transform.position;
        Vector3 dist = (p - this.transform.position).normalized;
        
        this.GetComponent<Rigidbody>().isKinematic = true;
        GameObject go = Instantiate(ps, this.transform.position + dist * 2, Quaternion.identity);
        this.transform.GetChild(0).gameObject.SetActive(false);
        go.transform.parent = this.gameObject.transform;
        Invoke("Des", 0.3f);
    }

    private void Des()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
    }

}
