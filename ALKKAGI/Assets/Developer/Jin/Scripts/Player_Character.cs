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

    public float speed = 5f;

    private void Start()
    {
        ShowCursor();
        rb = this.GetComponent<Rigidbody>();
        bullet = Resources.Load<GameObject>("TESTBUL 1");
        bullet.AddComponent<Bullet>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetMouseButtonDown(0))
            Attack(new Vector3(this.transform.position.x + 1f, 0.75f, this.transform.position.z), 40f);

        if (Input.GetKeyDown(KeyCode.Q))
            UseSkill();
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

    [SerializeField]
    private float rotCamX = 5f;
    [SerializeField]
    private float rotCamY = 3f;

    private float limitMinX = -80f;
    private float limitMaxX = 50f;
    private float eulerAngleX;
    private float eulerAngleY;

    private void RotateCam()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        eulerAngleX -= mouseY * rotCamX;
        eulerAngleY += mouseX * rotCamY;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0f);
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
        dir = this.transform.rotation * new Vector3(dir.x, 0f, dir.z);
        moveForce = new Vector3(dir.x, moveForce.y, dir.z);
        this.transform.position += moveForce * 0.1f;
    }

    protected override void Jump()
    {
        rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
    }

    public override void Attack(Vector3 bulpos, float shootPower)
    {
        GameObject go = Instantiate(bullet, bulpos, Quaternion.identity);
        go.transform.position = this.transform.position + Vector3.right;
        go.GetComponent<Rigidbody>().AddForce(Vector3.forward * shootPower, ForceMode.Impulse);
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
