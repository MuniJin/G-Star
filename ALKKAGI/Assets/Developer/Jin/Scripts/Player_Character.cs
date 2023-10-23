using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player_Character : Default_Character
{
    // �˱�� �Ŵ����� ���� �̱������� ������ ����
    public GameObject ALM;
    // Default_Character�� ��ӹ��� ������ ĳ����(��, ��, ��, ��...)�� Ư���� ���� �� �ְ� ����
    private Default_Character _d;
    // ���� ����� ���� ����
    private Rigidbody rb;
    // �Ѿ�, �÷��̾� ������Ʈ, �� ��ų(���� �ٸ�������� ������ �ҷ��ͼ� ����� ����)
    private GameObject bullet;
    //public GameObject playerObj;
    //public GameObject kingSkill;

    // �ӵ�, ���� ��
    public float speed = 5f;
    public float jumpForce = 5f;

    // �ѱ� ��ġ
    public GameObject bulPos;

    private void Start()
    {
        ALM = GameObject.Find("AlKKAGIManager");
        cam = Camera.main;
        bulPos = this.gameObject.transform.GetChild(0).gameObject;
        ChooseCharacter();

        // �÷��̾� �ʱ� ü�� ����
        this._hp = 100;

        // �÷��̾� ������Ʈ�� rigidbody �޾ƿ���
        rb = this.gameObject.GetComponent<Rigidbody>();
        
        // �÷��̾� �±� ����
        this.gameObject.tag = "Player";
        
        // �Ѿ� Resources�������� �ҷ��ͼ� ���(���� ���� ����, ���� ����, ��� : AssetBundle)
        bullet = Resources.Load<GameObject>("TESTBUL 0");
        if (bullet.GetComponent<Bullet>() == false)
            bullet.AddComponent<Bullet>();
    }

    private void Update()
    {
        Debug.Log(_d.name);
        // ����, velocity�� ������ ���� �����ϰ�
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
            Jump();
        // �ѱ� ��ġ���� �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0))
            Attack(bulPos.transform.position, 40f);
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

        eulerAngleX -= mouseY * sensitivity;
        eulerAngleY += mouseX * sensitivity;

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
        GameObject go = Instantiate(bullet, bulpos, new Quaternion(-90f, 0f, 0f, 0f));
        
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

    // ���Ƿ� ĳ���� ���� �����ϰ� ���ִ� �Լ�, ��ư�� ����
    public void ChooseCharacter()
    {
        //GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        //string str = clickedBtn.name;

        string str = this.gameObject.name.Split('_')[0];

        if (_d == null)
        {
            Debug.Log("Select " + str);

            switch (str)
            {
                case "Solider":
                    _d = this.gameObject.AddComponent<Pawn>();
                    bullet = Resources.Load<GameObject>("TESTBUL 1");
                    break;
                case "Chariot":
                    _d = this.gameObject.AddComponent<Rook>();
                    bullet = Resources.Load<GameObject>("TESTBUL 2");
                    break;
                case "Horse":
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
                case "Guard":
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
