using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPoint : MonoBehaviour
{
    // ���� ����
    [SerializeField] float rotationSpeed = 5f; // ȸ�� �ӵ�
    [SerializeField] float velocity = 9f; // �ӵ�
    [SerializeField] float acceleration = 100f; // ���ӵ�

    // ����׿� ������
    [SerializeField] Material debugMaterialOrange; // �������� ��Ƽ����
    [SerializeField] Material debugMaterialGreen; // �ʷϻ� ��Ƽ����
    [SerializeField] Mesh debugMesh; // ������� ���� Mesh
    [SerializeField] bool debugEnabled = false; // ����� Ȱ��ȭ ����

    // ������Ƽ
    private Vector3 TargetPosition { get => target.position + Vector3.up; } // ��ǥ ��ġ

    // ���� ����
    private List<Vector3> waypoints = new List<Vector3>(); // ������ ���� ����Ʈ

    [SerializeField] private Transform target; // ��ǥ ��ġ�� ��Ÿ���� Transform
    private Rigidbody rb; // Rigidbody

    // �ٸ� ������
    private float stuckTimer = -1; // ���� Ÿ�̸�
    private Vector3 lastPosition; // ���� ��ġ
    private bool wiggleWaypointExixts; // ��鸲 ���� ���� ����
    private float orgDrag; // �ʱ� �巡�� ��
    private LineRenderer debugLineRenderer; // ���� ������
    private Material debugLineMaterialGreen; // ����׿� �ʷϻ� ��Ƽ����
    private Material debugLineMaterialOrange; // ����׿� �������� ��Ƽ����

    private int worldLayerMask; // ���� ���̾� ����ũ

    //Object gameObject;


    void Start()
    {
        target = ((GameObject)gameObject).transform;
        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ ��������
        worldLayerMask = LayerMask.GetMask("World"); // "World" ���̾� ����ũ ��������

        orgDrag = rb.drag; // �ʱ� �巡�� �� ����
    }

    void Update()
    {
        if (debugEnabled) DrawDebug(); // ����� ��� Ȱ��ȭ ���ο� ���� ����� �޼��� ȣ��

        if (target == null) return; // ��ǥ ��ġ�� ������ ����

        // ��ǥ �������� ���� ���
        var targetDirection = (TargetPosition - transform.position).normalized;

        // SphereCast�� ��ο� ��ֹ��� �ִ��� Ȯ���ϰ�, ���ٸ� waypoints�� ������Ʈ
        if (!Physics.SphereCast(new Ray(transform.position, targetDirection), 0.5f, Vector3.Distance(transform.position, TargetPosition), worldLayerMask) &&
            !Physics.CheckSphere(transform.position + targetDirection, 0.5f, worldLayerMask))
        {
            waypoints.Clear(); // waypoints ����Ʈ �ʱ�ȭ
            waypoints.Add(TargetPosition); // ��ǥ ������ waypoints�� �߰�
        }

        // waypoints ����Ʈ�� ������� �ʰ�, ������ ������ ��ǥ ������ �Ÿ��� 1f �̻��̸� waypoints�� ��ǥ ���� �߰�
        if (waypoints.Count > 0 && Vector3.Distance(waypoints[waypoints.Count - 1], TargetPosition) > 1f)
        {
            waypoints.Add(TargetPosition);
        }

        // ���� waypoints ����Ʈ�� ����ִٸ� ����
        if (waypoints.Count == 0) return;

        // ���� waypoints[0] ������ �����ϸ� �ش� ���� ����
        if (Vector3.Distance(transform.position, waypoints[0]) < 2f)
        {
            waypoints.RemoveAt(0);
            wiggleWaypointExixts = false;
        }
    }

    // ���� ������Ʈ�� ���� �޼���
    private void FixedUpdate()
    {
        rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z)); // Rigidbody�� ��ġ �̵�

        if (waypoints.Count == 0) // waypoints�� ��������� �巡�� ���� �����ϰ� ����
        {
            rb.drag = 1f;
            return;
        }
        else
        {
            rb.drag = orgDrag; // �巡�� �� ����
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(waypoints[0] - rb.position), rotationSpeed * Time.deltaTime)); // ���� ȸ�� ����

            // ���� ���ӷ��� �߰��Ͽ� ���� �̵���Ŵ
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);

            // ���� �ӵ��� ������ ��(velocity)�� �Ѿ�� �ӵ� ���� ����
            if (rb.velocity.magnitude > velocity)
            {
                rb.velocity = rb.velocity.normalized * velocity;
            }

            // ���� ��ġ�� ���� ��ġ ������ �Ÿ� ���
            var distanceTravelled = Vector3.Distance(lastPosition, rb.position);
            lastPosition = rb.position;

            // �̵� �Ÿ��� �ſ� �۴ٸ� ���� �ð� �����ִ� ������ �Ǵ��ϰ� Ư�� ���� ������ ���� ��ġ�� �̵�
            if (distanceTravelled < Time.fixedDeltaTime)
            {
                if (stuckTimer < 0) stuckTimer = Time.time;

                if (Time.time > stuckTimer + 1)
                {
                    // �ݰ� 4 ������ ���� ��ġ ����
                    var randomInCircle = Random.insideUnitCircle.normalized * 4;
                    var wigglePosition = rb.position + new Vector3(randomInCircle.x, randomInCircle.y, 0);

                    // �ش� ��ġ�� ��ֹ��� ������ waypoints�� �߰� �Ǵ� ������Ʈ
                    if (!Physics.CheckSphere(wigglePosition, 0.5f, worldLayerMask))
                    {
                        if (!wiggleWaypointExixts)
                        {
                            waypoints.Insert(0, wigglePosition);
                            wiggleWaypointExixts = true;
                        }
                        else
                        {
                            waypoints[0] = wigglePosition;
                        }
                        stuckTimer = -1;
                    }
                }
            }
        }
    }

    void TargetEnter(Object gameObject)
    {
        target = ((GameObject)gameObject).transform;
    }
    // ������� ���� �޼���
    private void DrawDebug()
    {
        // ���� ������ �ʱ�ȭ
        if (debugLineRenderer == null)
        {
            debugLineMaterialOrange = new Material(debugMaterialOrange);
            debugLineMaterialOrange.color = new Color(debugLineMaterialOrange.color.r, debugLineMaterialOrange.color.g, debugLineMaterialOrange.color.b, 0.1f);
            debugLineMaterialGreen = new Material(debugLineMaterialGreen);
            debugLineMaterialGreen.color = new Color(debugLineMaterialGreen.color.r, debugLineMaterialOrange.color.g, debugLineMaterialOrange.color.b, 0.1f);
            debugLineRenderer = gameObject.AddComponent<LineRenderer>();
            debugLineRenderer.material = debugMaterialOrange;
            debugLineRenderer.startWidth = debugLineRenderer.endWidth = 0.5f;
        }

        // waypoints�� ���� ����� ���� ǥ��
        for (int i = 0; i < waypoints.Count; i++)
        {
            // �������� ǥ��
            Graphics.DrawMesh(debugMesh, waypoints[i], Quaternion.identity, i == 0 ? debugMaterialGreen : debugMaterialOrange, 0);

            // ��ǥ ������ �� ��ġ ���̿� ��ֹ��� �ִ��� üũ�ϰ�, ���� �������� ǥ��
            if (Physics.SphereCast(new Ray(transform.position, (TargetPosition - transform.position).normalized), 0.5f, out RaycastHit hit, Vector3.Distance(transform.position, target.position)))
            {
                debugLineRenderer.sharedMaterial = debugLineMaterialOrange;
                debugLineRenderer.SetPositions(new Vector3[] { transform.position, hit.point });
            }
            else
            {
                debugLineRenderer.sharedMaterial = debugLineMaterialGreen;
                debugLineRenderer.SetPositions(new Vector3[] { transform.position, TargetPosition });
            }
        }
    }
}
