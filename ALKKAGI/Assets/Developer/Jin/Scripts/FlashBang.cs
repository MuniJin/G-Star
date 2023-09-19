using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBang : MonoBehaviour
{
    private void Update()
    {
        if (this.transform.position.y < 5)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
