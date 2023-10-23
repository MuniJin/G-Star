using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyHp : Default_Character
{
    Default_Character _d;

    private void Start()
    {
        ChooseCharacter();
        this._hp = 100;
    }
    // 임의로 캐릭터 선택 가능하게 해주는 함수, 버튼과 연결
    public void ChooseCharacter()
    {
        string str = this.gameObject.name.Split('_')[0];

        if (_d == null)
        {
            switch (str)
            {
                case "Solider":
                    _d = this.gameObject.AddComponent<Pawn>();
                    break;
                case "Chariot":
                    _d = this.gameObject.AddComponent<Rook>();
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
                    break;
                case "Guard":
                    _d = this.gameObject.AddComponent<Guards>();
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
    }
    public override void Attack(Vector3 bulpos, float shootPower)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Skill(GameObject go)
    {
        throw new System.NotImplementedException();
    }

    protected override void Jump()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

}
