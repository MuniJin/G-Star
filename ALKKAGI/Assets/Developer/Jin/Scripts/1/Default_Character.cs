using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� -> ���ڷ����� ������ ����ϱ� ���� ������ �߻� Ŭ����
public abstract class Default_Character : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    // ü��
    protected int _hp { get; set; }

    public void Attacked(int damage) { _hp -= damage; }

    public int GetHp() { return _hp; }

    // ��ų ��Ÿ��
    protected float _coolDown { get; set; }

    public float GetCoolDown() { return _coolDown; }

    // ���� ������
    protected int _damage { get; set; }

    public int GetDamage() { return _damage; }

    public void SetStatus(int hp, float coolDown, int damage)
    {
        _hp = hp;
        _coolDown = coolDown;
        _damage = damage;
    }

    public void GetStatus() { Debug.Log($"Name : {this.name} | Hp : {_hp} | CoolDown : {_coolDown} | Damage : {_damage}"); }

    // ������, ����, ����, ��ų �߻� �Լ�
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