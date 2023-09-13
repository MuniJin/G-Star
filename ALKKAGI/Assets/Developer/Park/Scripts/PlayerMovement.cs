using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // 플레이어 및 카메라의 Transform 컴포넌트를 할당하는 변수들
    public Transform playerCam;
    public Transform orientation;

    // 리지드바디 컴포넌트
    private Rigidbody rb;

    // 회전 및 시선 관련 변수
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    //Movement
    public float moveSpeed = 600;
    public float maxSpeed = 20;
    public bool grounded;
    public LayerMask whatIsGround;
    
    public float maxSlopeAngle = 35f;

    public float crouchSpeedMultiplier = 0.5f;
    public float airSpeedMultiplier = 0.5f;

    //Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.7f, 1);
    private Vector3 playerScale;
    public float slideForce = 100;
    public float slideCounterMovement = 0.1f;

    //Jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;
    
    //Input
    float x, y;
    bool jumping, crouching;
    
    //Sliding
    private Vector3 normalVector = Vector3.up;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    
    void Start() {
        playerScale =  transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    private void FixedUpdate() {
        Movement();
    }

    private void Update() {
        MyInput();
        Look();
    }

    private void MyInput() {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
        crouching = Input.GetKey(KeyCode.LeftControl);
      
        //Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    private void StartCrouch() {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.5f) {
            if (grounded) {
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch() {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement()
    {
        // 추가 중력을 적용하여 땅에 더 꾸준히 붙을 수 있도록 합니다.
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        // 점프 버튼을 누르고 준비 상태일 때 점프합니다.
        if (readyToJump && jumping) Jump();

        // 최대 속도를 설정합니다.
        float maxSpeed = this.maxSpeed;

        // 앉기 중인지, 공중에 있는지, 점프 준비 상태인지에 따라 최대 속도를 조정합니다.
        if (crouching)
        {
            maxSpeed *= crouchSpeedMultiplier; // 앉은 상태에서의 최대 속도를 줄입니다.
        }
        if (!grounded || !readyToJump)
        {
            maxSpeed *= airSpeedMultiplier; // 공중에 있거나 점프 준비가 되지 않은 경우 최대 속도를 줄입니다.
        }

        // 움직이는 플레이어에게 힘을 가합니다.
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime);

        // 플레이어의 속도를 최대 속도로 제한합니다.
        Vector2 horizontalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.y);
        }
    }

    private void Jump()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;

            // 점프 힘을 추가합니다.
            rb.AddForce(Vector2.up * jumpForce * 0.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            // 점프 중에 떨어질 때, y 속도를 재설정합니다.
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump() {
        readyToJump = true;
    }
    
    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        // 현재의 시선 회전을 찾습니다.
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        // 회전하고, 회전이 과도하지 않도록 조정합니다.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 90f);

        // 회전을 수행합니다.
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }


    // 플레이어가 바닥에 있는지 확인합니다.
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    // 바닥 감지를 처리합니다.
    private bool cancellingGrounded;
    private void OnCollisionStay(Collision other)
    {
        // 걷기 가능한 레이어만 확인하도록 합니다.
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        // 각 물체와의 충돌에 대해 반복합니다.
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            // 바닥(FLOOR)
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        // 충돌 종료 시 노멀을 확인할 수 없으므로 노멀 확인을 위해 지연 시간을 설정합니다.
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    // 바닥 감지 상태를 해제합니다.
    private void StopGrounded()
    {
        grounded = false;
    }
}
