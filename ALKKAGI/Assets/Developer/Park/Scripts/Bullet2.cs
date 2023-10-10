using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;

    private void Start()
    {
        // 일정 시간 후에 총알을 파괴
        Destroy(gameObject, bulletLifetime);
    }

    private void Update()
    {
        // 총알을 이동시킴
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Obstacles" 태그를 가진 오브젝트와 충돌한 경우
        if (other.CompareTag("Obstacles"))
        {
            // 총알을 파괴
            Destroy(gameObject);
        }
    }
}