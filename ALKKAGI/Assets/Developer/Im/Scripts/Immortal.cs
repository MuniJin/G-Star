using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immortal : Singleton<Immortal>
{
    public float sensitivity;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        sensitivity = 0.5f;
    }
}
