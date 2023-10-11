using System;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    // Assignables
    public Transform playerCam; // �÷��̾� ī�޶��� Transform�� ��Ÿ���ϴ�.
    public Transform orientation; // �÷��̾��� ����(Transform)�� ��Ÿ���ϴ�.

    // Other
    private Rigidbody rb; // Rigidbody ������Ʈ�� ��Ÿ���ϴ�.

    // Rotation and look
    private float xRotation; // X �� ȸ�� ������ ��Ÿ���ϴ�.
    private float sensitivity = 50f; // ���콺 ������ ��Ÿ���ϴ�.
    private float sensMultiplier = 1f; // ������ ������ ��Ÿ���ϴ�.

    // Movement
    public float moveSpeed = 4500; // �̵� �ӵ��� ��Ÿ���ϴ�.
    public float maxSpeed = 10; // �ִ� �̵� �ӵ��� ��Ÿ���ϴ�.
    public bool grounded; // ���� �ִ��� ���θ� ��Ÿ���ϴ�.
    public LayerMask whatIsGround; // ������ �ν��� ���̾� ����ũ�� ��Ÿ���ϴ�.

    public float counterMovement = 0.125f; // �̵� �� �ݵ��� ��Ÿ���ϴ�.
    private float threshold = 0.01f; // �̵� ���� �Ӱ谪�� ��Ÿ���ϴ�.
    public float maxSlopeAngle = 35f; // �ִ� ��簢�� ��Ÿ���ϴ�.

    // Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1); // �ɾ��� �� �������� ��Ÿ���ϴ�.
    private Vector3 playerScale; // �÷��̾��� �ʱ� �������� ��Ÿ���ϴ�.
    public float slideForce = 1; // �����̵� ���� ��Ÿ���ϴ�.
    public float slideCounterMovement = 1f; // �����̵� �� �ݵ��� ��Ÿ���ϴ�.

    // Jumping
    private bool readyToJump = true; // ���� ������ �������� ���θ� ��Ÿ���ϴ�.
    private float jumpCooldown = 0.25f; // ���� ��ٿ� �ð��� ��Ÿ���ϴ�.
    public float jumpForce = 550f; // ���� ���� ��Ÿ���ϴ�.

    // Input
    float x, y; // ���� �� ���� �Է��� ��Ÿ���ϴ�.
    bool jumping, sprinting, crouching; // ����, �޸���, �ɱ� �Է��� ��Ÿ���ϴ�.

    // Sliding
    private Vector3 normalVector = Vector3.up; // ǥ�� ���͸� ��Ÿ���ϴ�.
    private Vector3 wallNormalVector; // ���� ǥ�� ���͸� ��Ÿ���ϴ�.

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ�� �����ɴϴ�.
    }

    void Start()
    {
        playerScale = transform.localScale; // �ʱ� �������� �����մϴ�.
        Cursor.lockState = CursorLockMode.Locked; // Ŀ���� ��� ���� �����մϴ�.
        Cursor.visible = false; // Ŀ���� ����ϴ�.
    }

    private void FixedUpdate()
    {
        Movement(); // �̵� ó���� FixedUpdate���� ȣ���մϴ�.
    }

    private void Update()
    {
        MyInput(); // ����� �Է��� ������Ʈ�մϴ�.
        Look(); // ī�޶� ȸ���� ������Ʈ�մϴ�.
    }

    // MyInput �Լ�: ����� �Է��� ó���մϴ�.
    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal"); // ���� �Է��� �޽��ϴ�.
        y = Input.GetAxisRaw("Vertical"); // ���� �Է��� �޽��ϴ�.
        jumping = Input.GetButton("Jump"); // ���� �Է��� �޽��ϴ�.
        crouching = Input.GetKey(KeyCode.LeftControl); // �ɱ� �Է��� �޽��ϴ�.

        // �ɱ� ����
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();

        // �ɱ� ����
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    // StartCrouch �Լ�: �ɱ⸦ �����մϴ�.
    private void StartCrouch()
    {
        transform.localScale = crouchScale; // �������� �ɱ� �����Ϸ� �����մϴ�.
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        // �̵� ���̸� �����̵��� �����մϴ�.
        if (rb.velocity.magnitude > 0.5f)
        {
            if (grounded)
            {
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    // StopCrouch �Լ�: �ɱ⸦ �����մϴ�.
    private void StopCrouch()
    {
        transform.localScale = playerScale; // �������� ���� �����Ϸ� �����մϴ�.
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    // Movement �Լ�: �÷��̾��� �̵��� ó���մϴ�.
    private void Movement()
    {
        // �߰� �߷� ����
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        // �÷��̾ �ٶ󺸴� ���⿡ ���� ���� �ӵ� ���
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        // �����̵� �� �Ҿ����� �̵� ����
        CounterMovement(x, y, mag);

        // ���� �Է��� �ְ� ���� ������ ���¶�� ����
        if (readyToJump && jumping)
            Jump();

        // �ִ� �ӵ� ����
        float maxSpeed = this.maxSpeed;

        // ������ �������� ���� ��, �÷��̾ ���� �굵�� �ϰ� �ӵ��� ���Դϴ�.
        if (crouching && grounded && readyToJump)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        // �ӵ��� �ִ� �ӵ����� ũ�� �Է��� ����Ͽ� �ִ� �ӵ��� �����մϴ�.
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        // �Ϻ� ���� ���
        float multiplier = 1f, multiplierV = 1f;

        // ���߿����� �̵� ó��
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        // �����̵� ���� ��� ���� �̵� ��Ȱ��ȭ
        if (grounded && crouching) multiplierV = 1.5f;

        // �̵����� �����Ͽ� �÷��̾ �̵���ŵ�ϴ�.
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    // Jump �Լ�: ������ ó���մϴ�.
    private void Jump()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;

            // ���� �� �߰�
            rb.AddForce(Vector2.up * jumpForce * 0.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            // ���� �� �ϰ� ���� ��, Y �ӵ��� �缳���մϴ�.
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            // ���� ��ٿ� ����
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    // ResetJump �Լ�: ���� ��ٿ��� �ʱ�ȭ�մϴ�.
    private void ResetJump()
    {
        readyToJump = true;
    }

    // Look �Լ�: ���콺�� ī�޶� ȸ���� ó���մϴ�.
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        // ���� ȸ���� ã���ϴ�.
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        // ȸ�� �� ���� �Ǵ� ��� ȸ���� ������ �մϴ�.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 90f);

        // ȸ�� ����
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    // CounterMovement �Լ�: �ݵ��� ó���մϴ�.
    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping) return;

        // �����̵� ����
        if (crouching)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        // �ݵ� ó��
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        // �밢�� �̵� ����
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    // FindVelRelativeToLook �Լ�: ī�޶� �������� �÷��̾��� ������� �ӵ��� ã���ϴ�.
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

    // IsFloor �Լ�: �������� Ȯ���մϴ�.
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;
    private float desiredX;

    // OnCollisionStay �Լ�: ���� ������ ó���մϴ�.
    private void OnCollisionStay(Collision other)
    {
        // ���� ���̾ üũ�մϴ�.
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        // ��� �浹�� ���� �ݺ��մϴ�.
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;

            // ������ ���
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        // Ground/Wall ��Ҹ� ȣ���մϴ�. CollisionExit���� normal�� Ȯ���� �� ���� �����Դϴ�.
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    // StopGrounded �Լ�: grounded�� false�� �����մϴ�.
    private void StopGrounded()
    {
        grounded = false;
    }
}
