using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player_Character : Default_Character
{
    // �˱�� �Ŵ����� ���� �̱������� ������ ����
    private AlKKAGIManager ALM;
    private FPSManager fm;
    // Default_Character�� ��ӹ��� ������ ĳ����(��, ��, ��, ��...)�� Ư���� ���� �� �ְ� ����
    private Default_Character _d;
    // ���� ����� ���� ����
    private Rigidbody rb;
    // �Ѿ�, �÷��̾� ������Ʈ, �� ��ų(���� �ٸ�������� ������ �ҷ��ͼ� ����� ����)
    public GameObject bullet;

    // �ӵ�, ���� ��
    public float speed = 5f;
    public float jumpForce = 5f;

    // �ѱ� ��ġ
    public GameObject bulPos;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Map1")
            ALM = AlKKAGIManager.Instance;
        fm = FPSManager.Instance;

        cam = Camera.main;
        bulPos = this.gameObject.transform.GetChild(0).gameObject;

        //// �÷��̾� �ʱ� ü�� ����
        this._hp = 100;

        // �÷��̾� ������Ʈ�� rigidbody �޾ƿ���
        rb = this.gameObject.GetComponent<Rigidbody>();

        // �÷��̾� �±� ����
        this.gameObject.tag = "Player";

        fm.ChooseCharacter(ref _d, ref bullet, this.gameObject);

        if (bullet.GetComponent<Bullet>() == false)
            bullet.AddComponent<Bullet>();
    }

    private void Update()
    {
        // ����, velocity�� ������ ���� �����ϰ�
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
            Jump();
        // �ѱ� ��ġ���� �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0))
            _d.Attack(bulPos.transform.position, 40f);
        // ��ų ���
        if (Input.GetKeyDown(KeyCode.Q))
            UseSkill();

        // AI �ϼ� ������ ���� ���г�������� ���Ƿ� ����� �� ���ǹ�
        if (Input.GetKeyDown(KeyCode.O))
            Win();
        if (Input.GetKeyDown(KeyCode.P))
            Lose();
    }

    // ���� ���г���� ������ �Լ�
    public void Win()
    {
        ALM.GetComponent<AlKKAGIManager>().BoardObj.SetActive(true);
        ALM.GetComponent<AlKKAGIManager>().IsWin = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("Board");
        ALM.GetComponent<AlKKAGIManager>().FPSResult();

    }

    // ���� ���г���� ������ �Լ�
    public void Lose()
    {
        ALM.GetComponent<AlKKAGIManager>().BoardObj.SetActive(true);
        Cursor.visible = false;
        ALM.GetComponent<AlKKAGIManager>().IsWin = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("Board");
        ALM.GetComponent<AlKKAGIManager>().FPSResult();
    }

    private void FixedUpdate()
    {
        // �÷��̾� ������
        Move();
        // ���콺 �����ӿ� ���� ī�޶� ȸ���� ����
        CamMove();
    }

    float h, v;

    protected override void Move()
    {
        CamMove();
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.forward * v + transform.right * h;

        this.transform.position += dir * speed * Time.deltaTime;
    }

    private float mouseX, mouseY;
    private float eulerAngleX, eulerAngleY;

    private float rotCamX = 5f;
    private float rotCamY = 3f;

    private float limitMinX = -40f;
    private float limitMaxX = 40f;

    private Camera cam;

    public float sensitivity = 2f;

    private void CamMove()
    {
        cam.transform.position = this.gameObject.transform.position;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        eulerAngleX -= mouseY * rotCamX * sensitivity;
        eulerAngleY += mouseX * rotCamY * sensitivity;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        this.transform.rotation = Quaternion.Euler(0f, eulerAngleY, 0f);
        cam.transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0f);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }

    // ����
    protected override void Jump() => rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    // ����
    public override void Attack(Vector3 bulpos, float shootPower)
    {
        GameObject go = Instantiate(bullet, bulpos, bullet.transform.rotation);

        go.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * shootPower, ForceMode.Impulse);
    }

    // ��ų(Default_Character�� ��ӹ޾Ƽ� �����ϳ� �ʿ��� ����ó���� �ص�)
    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }

    // ��ų ���
    private void UseSkill()
    {
        if (_d != null)
            StartCoroutine(_d.Skill(this.gameObject));
        else
            Debug.Log("Not Decorator");
    }

    
}
