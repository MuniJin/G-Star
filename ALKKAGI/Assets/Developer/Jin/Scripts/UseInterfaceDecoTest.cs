using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Character
{
    protected string Name;
    protected int Hp;
    protected float Shoot_Delay;
    protected float Speed;
    protected float Skill_CoolDown;

    public abstract string Get_Info();
}

public class Init_Character : Base_Character
{
    public Init_Character()
    {
        this.Name = "Init";
        this.Hp = 100;
        this.Shoot_Delay = 0.3f;
        this.Speed = 5f;
        this.Skill_CoolDown = 10f;
    }

    public override string Get_Info()
    {
        return $"Name : {this.Name} | Hp : {this.Hp} | Shoot_Delay : {this.Shoot_Delay} | Speed : {this.Speed} | Skill_CoolDown : {this.Skill_CoolDown}";
    }

    public void Move() { }
    public void Jump() { }
    public void Shoot() { }
}


public class UseInterfaceDecoTest : MonoBehaviour
{
    private void Awake()
    {
        Init_Character b = new Init_Character();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { }
    }
}
