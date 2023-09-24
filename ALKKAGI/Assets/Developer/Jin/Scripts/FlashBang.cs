using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBang : MonoBehaviour
{
    // 사 스킬, 사용할지 안할지 미정
    private void Update()
    {
        if (this.transform.position.y < 5)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
