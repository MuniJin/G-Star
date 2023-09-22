using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player_Character : Default_Character
{
    private Default_Character _d;

    private Rigidbody rb;

    private GameObject bullet;
    public GameObject playerObj;
    public GameObject kingSkill;

    public float speed = 1f;
    private float jumpForce = 5f;

    private void Start()
    {
        ShowCursor();

        rb = this.GetComponent<Rigidbody>();

        bullet = Resources.Load<GameObject>("TESTBUL 0");
        if (bullet.GetComponent<Bullet>() == false)
            bullet.AddComponent<Bullet>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
            Jump();

        if (Input.GetMouseButtonDown(0))
            Attack(bulPos.transform.position, 40f);

        if (Input.GetKeyDown(KeyCode.Q))
            UseSkill();

        if (Input.GetKeyDown(KeyCode.R))
            ShowCursor();
    }

    private void FixedUpdate()
    {
        Move();
        RotateCam();
    }

    private void ShowCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
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

    private float rotCamX = 5f;
    private float rotCamY = 3f;

    private float limitMinX = -40f;
    private float limitMaxX = 40f;
    private float eulerAngleX;
    private float eulerAngleY;

    public float sensitivity = 2f;

    private void RotateCam()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        eulerAngleX -= mouseY * rotCamX * sensitivity;
        eulerAngleY += mouseX * rotCamY * sensitivity;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        playerObj.transform.rotation = Quaternion.Euler(270f, 180f + eulerAngleY, 0f);
        Camera.main.transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0f);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }

    private Vector3 moveForce;

    protected override void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0f, v);
        dir = playerObj.transform.rotation * new Vector3(-dir.x, dir.z, 0f);
        moveForce = new Vector3(dir.x, moveForce.y, dir.z);
        this.transform.position += moveForce * 0.1f * speed;
    }

    protected override void Jump() => rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    public GameObject bulPos;

    public override void Attack(Vector3 bulpos, float shootPower)
    {
        GameObject go = Instantiate(bullet, bulpos, Quaternion.identity);
        
        go.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * shootPower, ForceMode.Impulse);
    }

    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }

    private void UseSkill()
    {
        if (_d != null)
            StartCoroutine(_d.Skill(this.gameObject));
        else
            Debug.Log("Not Decorator");
    }

    public void ChooseCharacter()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        string str = clickedBtn.name;

        if (_d == null)
        {
            Debug.Log("Select " + str);

            switch (str)
            {
                case "Pawn":
                    _d = this.gameObject.AddComponent<Pawn>();
                    bullet = Resources.Load<GameObject>("TESTBUL 1");
                    break;
                case "Rook":
                    _d = this.gameObject.AddComponent<Rook>();
                    bullet = Resources.Load<GameObject>("TESTBUL 2");
                    break;
                case "Knight":
                    _d = this.gameObject.AddComponent<Knight>();
                    //
                    break;
                case "Elephant":
                    _d = this.gameObject.AddComponent<Elephant>();
                    //
                    break;
                case "Cannon":
                    _d = this.gameObject.AddComponent<Cannon>();
                    //
                    break;
                case "Guards":
                    _d = this.gameObject.AddComponent<Guards>();
                    //
                    break;
                case "King":
                    _d = this.gameObject.AddComponent<King>();
                    //
                    break;
                default:
                    Debug.Log("it does not exist");
                    break;
            }
        }
        else
        {
            Debug.Log("Deselect " + str);
            bullet = Resources.Load<GameObject>("TESTBUL 0");
            Destroy(_d);
        }
    }
}
