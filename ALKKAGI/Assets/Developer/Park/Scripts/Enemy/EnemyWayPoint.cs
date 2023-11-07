using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPoint : MonoBehaviour
{
    // 변수 선언
    [SerializeField] float rotationSpeed = 5f; // 회전 속도
    [SerializeField] float velocity = 9f; // 속도
    [SerializeField] float acceleration = 100f; // 가속도

    // 디버그용 변수들
    [SerializeField] Material debugMaterialOrange; // 오렌지색 머티리얼
    [SerializeField] Material debugMaterialGreen; // 초록색 머티리얼
    [SerializeField] Mesh debugMesh; // 디버깅을 위한 Mesh
    [SerializeField] bool debugEnabled = false; // 디버그 활성화 여부

    // 프로퍼티
    private Vector3 TargetPosition { get => target.position + Vector3.up; } // 목표 위치

    // 변수 선언
    private List<Vector3> waypoints = new List<Vector3>(); // 경유지 지점 리스트

    [SerializeField] private Transform target; // 목표 위치를 나타내는 Transform
    private Rigidbody rb; // Rigidbody

    // 다른 변수들
    private float stuckTimer = -1; // 갇힘 타이머
    private Vector3 lastPosition; // 이전 위치
    private bool wiggleWaypointExixts; // 흔들림 지점 존재 여부
    private float orgDrag; // 초기 드래그 값
    private LineRenderer debugLineRenderer; // 라인 렌더러
    private Material debugLineMaterialGreen; // 디버그용 초록색 머티리얼
    private Material debugLineMaterialOrange; // 디버그용 오렌지색 머티리얼

    private int worldLayerMask; // 월드 레이어 마스크

    //Object gameObject;


    void Start()
    {
        target = ((GameObject)gameObject).transform;
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        worldLayerMask = LayerMask.GetMask("World"); // "World" 레이어 마스크 가져오기

        orgDrag = rb.drag; // 초기 드래그 값 저장
    }

    void Update()
    {
        if (debugEnabled) DrawDebug(); // 디버깅 기능 활성화 여부에 따라 디버그 메서드 호출

        if (target == null) return; // 목표 위치가 없으면 종료

        // 목표 지점과의 방향 계산
        var targetDirection = (TargetPosition - transform.position).normalized;

        // SphereCast로 경로에 장애물이 있는지 확인하고, 없다면 waypoints를 업데이트
        if (!Physics.SphereCast(new Ray(transform.position, targetDirection), 0.5f, Vector3.Distance(transform.position, TargetPosition), worldLayerMask) &&
            !Physics.CheckSphere(transform.position + targetDirection, 0.5f, worldLayerMask))
        {
            waypoints.Clear(); // waypoints 리스트 초기화
            waypoints.Add(TargetPosition); // 목표 지점을 waypoints에 추가
        }

        // waypoints 리스트가 비어있지 않고, 마지막 지점과 목표 지점의 거리가 1f 이상이면 waypoints에 목표 지점 추가
        if (waypoints.Count > 0 && Vector3.Distance(waypoints[waypoints.Count - 1], TargetPosition) > 1f)
        {
            waypoints.Add(TargetPosition);
        }

        // 현재 waypoints 리스트가 비어있다면 종료
        if (waypoints.Count == 0) return;

        // 적이 waypoints[0] 지점에 도착하면 해당 지점 삭제
        if (Vector3.Distance(transform.position, waypoints[0]) < 2f)
        {
            waypoints.RemoveAt(0);
            wiggleWaypointExixts = false;
        }
    }

    // 물리 업데이트를 위한 메서드
    private void FixedUpdate()
    {
        rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z)); // Rigidbody의 위치 이동

        if (waypoints.Count == 0) // waypoints가 비어있으면 드래그 값을 조정하고 종료
        {
            rb.drag = 1f;
            return;
        }
        else
        {
            rb.drag = orgDrag; // 드래그 값 복구
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(waypoints[0] - rb.position), rotationSpeed * Time.deltaTime)); // 적의 회전 조정

            // 전진 가속력을 추가하여 적을 이동시킴
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);

            // 적의 속도가 설정한 값(velocity)을 넘어가면 속도 값을 제한
            if (rb.velocity.magnitude > velocity)
            {
                rb.velocity = rb.velocity.normalized * velocity;
            }

            // 이전 위치와 현재 위치 사이의 거리 계산
            var distanceTravelled = Vector3.Distance(lastPosition, rb.position);
            lastPosition = rb.position;

            // 이동 거리가 매우 작다면 일정 시간 갇혀있는 것으로 판단하고 특정 범위 내에서 랜덤 위치로 이동
            if (distanceTravelled < Time.fixedDeltaTime)
            {
                if (stuckTimer < 0) stuckTimer = Time.time;

                if (Time.time > stuckTimer + 1)
                {
                    // 반경 4 내에서 랜덤 위치 선정
                    var randomInCircle = Random.insideUnitCircle.normalized * 4;
                    var wigglePosition = rb.position + new Vector3(randomInCircle.x, randomInCircle.y, 0);

                    // 해당 위치에 장애물이 없으면 waypoints에 추가 또는 업데이트
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
    // 디버깅을 위한 메서드
    private void DrawDebug()
    {
        // 라인 렌더러 초기화
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

        // waypoints에 따른 디버깅 정보 표시
        for (int i = 0; i < waypoints.Count; i++)
        {
            // 경유지점 표시
            Graphics.DrawMesh(debugMesh, waypoints[i], Quaternion.identity, i == 0 ? debugMaterialGreen : debugMaterialOrange, 0);

            // 목표 지점과 적 위치 사이에 장애물이 있는지 체크하고, 라인 렌더러로 표시
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
