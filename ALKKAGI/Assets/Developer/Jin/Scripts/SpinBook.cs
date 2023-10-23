using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBook : MonoBehaviour
{
    void Update()
    {
        this.transform.Rotate(new Vector3(0f, -2f, 0f));
    }
}
