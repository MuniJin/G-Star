using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 디자인 패턴 -> 데코레이터 패턴을 사용하기 위해 만들어둔 추상 클래스
public abstract class Default_Character : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    // 체력
    protected int _hp { get; set; }

    public void Attacked(int damage) { _hp -= damage; }

    public int GetHp() { return _hp; }

    // 스킬 쿨타임
    protected float _coolDown { get; set; }

    public float GetCoolDown() { return _coolDown; }

    // 공격 데미지
    protected int _damage { get; set; }

    public int GetDamage() { return _damage; }

    public void SetStatus(int hp, float coolDown, int damage)
    {
        _hp = hp;
        _coolDown = coolDown;
        _damage = damage;
    }

    public void GetStatus() { Debug.Log($"Name : {this.name} | Hp : {_hp} | CoolDown : {_coolDown} | Damage : {_damage}"); }

    // 움직임, 점프, 공격, 스킬 추상 함수
    protected abstract void Move();
    protected abstract void Jump();
    public abstract void Attack(Vector3 bulpos);
    public abstract IEnumerator Skill(GameObject go);
}

public class Decorator_Character : Default_Character
{
    protected override void Move() => throw new System.NotImplementedException();
    protected override void Jump() => throw new System.NotImplementedException();
    public override void Attack(Vector3 bulpos) => throw new System.NotImplementedException();

    public override IEnumerator Skill(GameObject go) => throw new System.NotImplementedException();

}