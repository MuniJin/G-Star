using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 총알 생성 3초후 삭제
    private void Start()
    {
        Invoke("DelBul", 3f);
    }

    private void DelBul()
    {
        Destroy(this.gameObject);
    }
}
