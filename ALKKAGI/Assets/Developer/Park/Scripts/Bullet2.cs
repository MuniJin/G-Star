using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;

    private void Start()
    {
        // ���� �ð� �Ŀ� �Ѿ��� �ı�
        Destroy(gameObject, bulletLifetime);
    }

    private void Update()
    {
        // �Ѿ��� �̵���Ŵ
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Obstacles" �±׸� ���� ������Ʈ�� �浹�� ���
        if (other.CompareTag("Obstacles"))
        {
            // �Ѿ��� �ı�
            Destroy(gameObject);
        }
    }
}