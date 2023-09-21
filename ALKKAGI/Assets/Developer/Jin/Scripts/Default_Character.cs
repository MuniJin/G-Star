using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Default_Character : MonoBehaviour
{
    public int _hp { get; set; }
    public float _coolDown { get; set; }
    public float _damage { get; set; }

    protected abstract void Move();
    protected abstract void Jump();
    public abstract void Attack(Vector3 bulpos, float shootPower);

    public abstract IEnumerator Skill(GameObject go);
}

public class Decorator_Character : Default_Character
{
    protected override void Move() => throw new System.NotImplementedException();
    protected override void Jump() => throw new System.NotImplementedException();
    public override void Attack(Vector3 bulpos, float shootPower) => throw new System.NotImplementedException();

    public override IEnumerator Skill(GameObject go) => throw new System.NotImplementedException();
}

// 쫄 : 이동속도 버프
public class Pawn : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;
            go.GetComponent<Player_Character>().speed *= 2f;
         
            yield return new WaitForSeconds(cooldown);
            
            go.GetComponent<Player_Character>().speed /= 2f;
            useSkill = false;
        }
    }
}

// 차 : 돌진
public class Rook : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;
            Vector3 forwardDirection = go.transform.forward;
            go.GetComponent<Rigidbody>().AddForce(forwardDirection * 20f, ForceMode.Impulse);

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}

// 상 : 상대방 이동속도 감소
public class Elephant : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}

// 마 : 연사
public class Knight : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    public override IEnumerator Skill(GameObject go)
    {
        Vector3 b1, b2;

        if (!useSkill)
        {
            useSkill = true;

            for (int i = 0; i < 3; i++)
            {
                // 공격 아직 미구현이므로 주석처리해둠

                b1 = go.transform.position + Vector3.right;
                b2 = go.transform.position + (Vector3.right * 2f);

                go.GetComponent<Player_Character>().Attack(b1, 60f);
                go.GetComponent<Player_Character>().Attack(b2, 60f);

                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(cooldown - 0.6f);
            useSkill = false;
        }
    }
}

// 포 : 순간이동
public class Cannon : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    private LayerMask groundLayer;

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;

            while (true)
            {
                Cursor.lockState = CursorLockMode.None; // 마우스를 중앙에 고정
                Cursor.visible = true; // 마우스를 보이지 않게 설정
             
                if (Input.GetMouseButtonDown(1))
                {
                    // 마우스 위치를 스크린 좌표에서 월드 좌표로 변환합니다.
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    // 레이캐스트를 발사하여 충돌한 객체를 감지합니다.
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                    {
                        // 충돌한 지점의 벡터3 값을 얻습니다.
                        Vector3 hitPoint = hit.point;

                        // 이제 hitPoint 변수에는 충돌한 지점의 월드 좌표가 포함됩니다.
                        Debug.Log("Hit point: " + hitPoint);

                        // hitPoint 변수를 사용하여 원하는 작업을 수행할 수 있습니다.
                        go.transform.position = hitPoint + Vector3.up;

                        Cursor.lockState = CursorLockMode.Locked; // 마우스를 중앙에 고정
                        Cursor.visible = false; // 마우스를 보이지 않게 설정
                    }

                    break;
                }

                yield return null;
            }

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}

// 사 : 섬광탄
public class Guards : Decorator_Character
{
    private float cooldown = 2f;
    private bool useSkill = false;

    public GameObject bang;

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;
            
            GameObject b = Instantiate(bang, go.GetComponent<PlayerMovement1>().bulParent.position, Quaternion.identity);
            b.AddComponent<Rigidbody>();
            //b.AddComponent<Rigidbody>().AddForce(go.GetComponent<PlayerMovement1>().playerCam.transform.forward * 20f, ForceMode.Impulse);

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}

// 왕 : 병사 벽세우기
public class King : Decorator_Character
{
    private float cooldown = 5f;
    private bool useSkill = false;

    public override IEnumerator Skill(GameObject go)
    {
        if (!useSkill)
        {
            useSkill = true;

            Vector3 ksPos = go.transform.position + go.GetComponent<PlayerMovement>().orientation.forward * 5f;
            GameObject ks = Instantiate(go.GetComponent<PlayerMovement1>().kingSkill, ksPos, go.GetComponent<PlayerMovement>().orientation.rotation);

            yield return new WaitForSeconds(cooldown);
            useSkill = false;
        }
    }
}
