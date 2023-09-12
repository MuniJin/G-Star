using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Character : Default_Character
{
    private Decorator_Character _d;
    private string str;
    public float speed = 5f;

    public Transform bulParent;
    public Transform bulParent2;
    public Transform bulParent3;
    public GameObject bullet;

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Q))
            UseSkill();

        if (Input.GetMouseButtonDown(0))
        {
            Attack(bulParent.position, 40f);
        }
    }

    public override void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        this.transform.position += new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
    }

    public override void Jump()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack(Vector3 bulpos, float shootPower)
    {
        GameObject go = Instantiate(bullet, bulpos, Quaternion.identity);
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * shootPower, ForceMode.Impulse);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
