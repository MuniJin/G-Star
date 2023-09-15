using System;
using System.Collections;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement1 : Default_Character {

    private Decorator_Character _d;

    private string str;

    public Transform bulParent;
    public Transform bulParent2;
    public Transform bulParent3;
    public GameObject bullet;

    public GameObject kingSkill;

    // �÷��̾� �� ī�޶��� Transform ������Ʈ�� �Ҵ��ϴ� ������
    public Transform playerCam;
    public Transform orientation;

    // ������ٵ� ������Ʈ
    private Rigidbody rb;

    // ȸ�� �� �ü� ���� ����
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
        whatIsGround = LayerMask.GetMask("Ground");
    }

    private void ShowCursor()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    private void FixedUpdate() {
        Movement();
    }

    private void Update() {
        MyInput();
        Look();

        if (Input.GetKeyDown(KeyCode.C))
            ShowCursor();

        if (Input.GetKeyDown(KeyCode.Q))
            UseSkill();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetMouseButtonDown(0))
            Attack(bulParent.position, 40f);
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
        // �߰� �߷��� �����Ͽ� ���� �� ������ ���� �� �ֵ��� �մϴ�.
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        // ���� ��ư�� ������ �غ� ������ �� �����մϴ�.
        if (readyToJump && jumping) Jump();

        // �ִ� �ӵ��� �����մϴ�.
        float maxSpeed = this.maxSpeed;

        // �ɱ� ������, ���߿� �ִ���, ���� �غ� ���������� ���� �ִ� �ӵ��� �����մϴ�.
        if (crouching)
        {
            maxSpeed *= crouchSpeedMultiplier; // ���� ���¿����� �ִ� �ӵ��� ���Դϴ�.
        }
        if (!grounded || !readyToJump)
        {
            maxSpeed *= airSpeedMultiplier; // ���߿� �ְų� ���� �غ� ���� ���� ��� �ִ� �ӵ��� ���Դϴ�.
        }

        // �����̴� �÷��̾�� ���� ���մϴ�.
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime);

        // �÷��̾��� �ӵ��� �ִ� �ӵ��� �����մϴ�.
        Vector2 horizontalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.y);
        }
    }

    private void Jump1()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;

            // ���� ���� �߰��մϴ�.
            rb.AddForce(Vector2.up * jumpForce * 0.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            // ���� �߿� ������ ��, y �ӵ��� �缳���մϴ�.
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

        // ������ �ü� ȸ���� ã���ϴ�.
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        // ȸ���ϰ�, ȸ���� �������� �ʵ��� �����մϴ�.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 90f);

        // ȸ���� �����մϴ�.
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }


    // �÷��̾ �ٴڿ� �ִ��� Ȯ���մϴ�.
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    // �ٴ� ������ ó���մϴ�.
    private bool cancellingGrounded;
    private void OnCollisionStay(Collision other)
    {
        // �ȱ� ������ ���̾ Ȯ���ϵ��� �մϴ�.
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        // �� ��ü���� �浹�� ���� �ݺ��մϴ�.
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            // �ٴ�(FLOOR)
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        // �浹 ���� �� ����� Ȯ���� �� �����Ƿ� ��� Ȯ���� ���� ���� �ð��� �����մϴ�.
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    // �ٴ� ���� ���¸� �����մϴ�.
    private void StopGrounded()
    {
        grounded = false;
    }

    public override void Move()
    {
        Movement();
    }

    public override void Jump()
    {
        Jump1();
    }

    public override void Attack(Vector3 bulpos, float shootPower)
    {
        GameObject go = Instantiate(bullet, bulpos, Quaternion.identity);
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.AddForce(orientation.transform.forward * shootPower, ForceMode.Impulse);
    }

    private void UseSkill()
    {
        if (_d != null)
            StartCoroutine(_d.Skill(this.gameObject));
        else
            Debug.Log("Not Decorator");
    }

    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }

    public void ChooseCharacter()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        str = clickedBtn.name;

        if (_d == null)
        {
            Debug.Log("Select " + str);
            switch (str)
            {
                case "Pawn":
                    _d = this.gameObject.AddComponent<Pawn>();
                    break;
                case "Rook":
                    _d = this.gameObject.AddComponent<Rook>();
                    break;
                case "Knight":
                    _d = this.gameObject.AddComponent<Knight>();
                    break;
                case "Elephant":
                    _d = this.gameObject.AddComponent<Elephant>();
                    break;
                case "Cannon":
                    _d = this.gameObject.AddComponent<Cannon>();
                    break;
                case "Guards":
                    _d = this.gameObject.AddComponent<Guards>();
                    break;
                case "King":
                    _d = this.gameObject.AddComponent<King>();
                    break;
                default:
                    Debug.Log("it does not exist");
                    break;
            }
        }
        else
        {
            Debug.Log("Deselect " + str);
            Destroy(_d);
        }
    }

}
