using System;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    // Assignables
    public Transform playerCam; // 플레이어 카메라의 Transform을 나타냅니다.
    public Transform orientation; // 플레이어의 방향(Transform)을 나타냅니다.

    // Other
    private Rigidbody rb; // Rigidbody 컴포넌트를 나타냅니다.

    // Rotation and look
    private float xRotation; // X 축 회전 각도를 나타냅니다.
    private float sensitivity = 50f; // 마우스 감도를 나타냅니다.
    private float sensMultiplier = 1f; // 감도의 배율을 나타냅니다.

    // Movement
    public float moveSpeed = 4500; // 이동 속도를 나타냅니다.
    public float maxSpeed = 10; // 최대 이동 속도를 나타냅니다.
    public bool grounded; // 땅에 있는지 여부를 나타냅니다.
    public LayerMask whatIsGround; // 땅으로 인식할 레이어 마스크를 나타냅니다.

    public float counterMovement = 0.125f; // 이동 중 반동을 나타냅니다.
    private float threshold = 0.01f; // 이동 반응 임계값을 나타냅니다.
    public float maxSlopeAngle = 35f; // 최대 경사각을 나타냅니다.

    // Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1); // 앉았을 때 스케일을 나타냅니다.
    private Vector3 playerScale; // 플레이어의 초기 스케일을 나타냅니다.
    public float slideForce = 1; // 슬라이딩 힘을 나타냅니다.
    public float slideCounterMovement = 1f; // 슬라이딩 중 반동을 나타냅니다.

    // Jumping
    private bool readyToJump = true; // 점프 가능한 상태인지 여부를 나타냅니다.
    private float jumpCooldown = 0.25f; // 점프 쿨다운 시간을 나타냅니다.
    public float jumpForce = 550f; // 점프 힘을 나타냅니다.

    // Input
    float x, y; // 수평 및 수직 입력을 나타냅니다.
    bool jumping, sprinting, crouching; // 점프, 달리기, 앉기 입력을 나타냅니다.

    // Sliding
    private Vector3 normalVector = Vector3.up; // 표준 벡터를 나타냅니다.
    private Vector3 wallNormalVector; // 벽의 표준 벡터를 나타냅니다.

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옵니다.
    }

    void Start()
    {
        playerScale = transform.localScale; // 초기 스케일을 저장합니다.
        Cursor.lockState = CursorLockMode.Locked; // 커서를 잠금 모드로 설정합니다.
        Cursor.visible = false; // 커서를 숨깁니다.
    }

    private void FixedUpdate()
    {
        Movement(); // 이동 처리를 FixedUpdate에서 호출합니다.
    }

    private void Update()
    {
        MyInput(); // 사용자 입력을 업데이트합니다.
        Look(); // 카메라 회전을 업데이트합니다.
    }

    // MyInput 함수: 사용자 입력을 처리합니다.
    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal"); // 수평 입력을 받습니다.
        y = Input.GetAxisRaw("Vertical"); // 수직 입력을 받습니다.
        jumping = Input.GetButton("Jump"); // 점프 입력을 받습니다.
        crouching = Input.GetKey(KeyCode.LeftControl); // 앉기 입력을 받습니다.

        // 앉기 시작
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();

        // 앉기 종료
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    // StartCrouch 함수: 앉기를 시작합니다.
    private void StartCrouch()
    {
        transform.localScale = crouchScale; // 스케일을 앉기 스케일로 변경합니다.
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        // 이동 중이면 슬라이딩을 적용합니다.
        if (rb.velocity.magnitude > 0.5f)
        {
            if (grounded)
            {
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    // StopCrouch 함수: 앉기를 종료합니다.
    private void StopCrouch()
    {
        transform.localScale = playerScale; // 스케일을 원래 스케일로 변경합니다.
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    // Movement 함수: 플레이어의 이동을 처리합니다.
    private void Movement()
    {
        // 추가 중력 적용
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        // 플레이어가 바라보는 방향에 따른 실제 속도 계산
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        // 슬라이딩 및 불안정한 이동 보정
        CounterMovement(x, y, mag);

        // 점프 입력이 있고 점프 가능한 상태라면 점프
        if (readyToJump && jumping)
            Jump();

        // 최대 속도 설정
        float maxSpeed = this.maxSpeed;

        // 경사면을 내려가고 있을 때, 플레이어가 땅에 닿도록 하고 속도를 높입니다.
        if (crouching && grounded && readyToJump)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        // 속도가 최대 속도보다 크면 입력을 취소하여 최대 속도를 유지합니다.
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        // 일부 곱셈 요소
        float multiplier = 1f, multiplierV = 1f;

        // 공중에서의 이동 처리
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        // 슬라이딩 중인 경우 수직 이동 비활성화
        if (grounded && crouching) multiplierV = 1.5f;

        // 이동력을 적용하여 플레이어를 이동시킵니다.
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    // Jump 함수: 점프를 처리합니다.
    private void Jump()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;

            // 점프 힘 추가
            rb.AddForce(Vector2.up * jumpForce * 0.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            // 점프 중 하강 중일 때, Y 속도를 재설정합니다.
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            // 점프 쿨다운 설정
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    // ResetJump 함수: 점프 쿨다운을 초기화합니다.
    private void ResetJump()
    {
        readyToJump = true;
    }

    // Look 함수: 마우스로 카메라 회전을 처리합니다.
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        // 현재 회전을 찾습니다.
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        // 회전 및 오버 또는 언더 회전이 없도록 합니다.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 90f);

        // 회전 수행
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    // CounterMovement 함수: 반동을 처리합니다.
    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping) return;

        // 슬라이딩 감속
        if (crouching)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        // 반동 처리
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        // 대각선 이동 제한
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    // FindVelRelativeToLook 함수: 카메라를 기준으로 플레이어의 상대적인 속도를 찾습니다.
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitude = rb.velocity.magnitude;
        float yMag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    // IsFloor 함수: 지면인지 확인합니다.
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;
    private float desiredX;

    // OnCollisionStay 함수: 지면 검출을 처리합니다.
    private void OnCollisionStay(Collision other)
    {
        // 지면 레이어만 체크합니다.
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        // 모든 충돌에 대해 반복합니다.
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;

            // 지면일 경우
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        // Ground/Wall 취소를 호출합니다. CollisionExit에서 normal을 확인할 수 없기 때문입니다.
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    // StopGrounded 함수: grounded를 false로 설정합니다.
    private void StopGrounded()
    {
        grounded = false;
    }
}
