using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float throwSpeed = 1f;
    private bool isThrown = false;
    private bool isNoHit = false;

    private Quaternion originalRotation;
    private Vector3 originPosition;

    public GameObject player;
    private MeshCollider mc;

    void Start()
    {
        originalRotation = transform.rotation;
        originPosition = transform.localPosition;
        mc = this.GetComponent<MeshCollider>();
    }

    public void ThrowHammer(Vector3 direction)
    {
        if (isNoHit)
            StopCoroutine(NoHit());
        isNoHit = true;
        isThrown = true;

        this.transform.parent = null; // 부모에서 분리

        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.AddForce(direction * throwSpeed, ForceMode.Impulse);

        mc.isTrigger = true;
        StartCoroutine(NoHit());
    }

    public void ReturnHammer()
    {
        isThrown = false;
        mc.isTrigger = false;

        this.transform.rotation = originalRotation;
        this.transform.parent = player.transform.GetChild(1); // 다시 플레이어의 자식으로 설정
        this.transform.position = player.transform.position + originPosition;
        this.transform.rotation = player.transform.rotation * originalRotation;

        Rigidbody rb =  GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        Destroy(rb);
    }

    private IEnumerator NoHit()
    {
        yield return new WaitForSeconds(2f);
        ReturnHammer();
        isNoHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            ReturnHammer();
        }
    }
}
