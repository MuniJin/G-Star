using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "death")
        {
            Invoke("Destroy",3f);
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
