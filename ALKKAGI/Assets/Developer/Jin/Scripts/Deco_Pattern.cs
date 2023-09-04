using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Player
{
    protected int Hp;
    protected float Cooldown;
    protected string Name;

    public abstract string Get_All_Information();
    public abstract int Get_Hp();
    public abstract float Get_Cooldown();
    public abstract string Get_Name();
}

public class InitDeco : Base_Player
{
    public InitDeco()
    {
        this.Hp = 0;
        this.Cooldown = 0f;
        this.Name = "Init";
    }
    public override string Get_All_Information()
    {
        return $"Hp : {this.Hp}\nCoolDown : {this.Cooldown}\nName : {this.Name}";
    }
    public override int Get_Hp() { return this.Hp; }
    public override float Get_Cooldown() { return this.Cooldown; }
    public override string Get_Name() { return this.Name; }
}


public class Pawn : InitDeco
{
    public Pawn()
    {
        this.Hp = 100;
        this.Cooldown = 3.0f;
        this.Name = "Pawn";
    }
}



public class Deco_Pattern : MonoBehaviour
{
    private Base_Player b;

    public void Start()
    {
        b = new InitDeco();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && b.Get_Name() != "Pawn")
            Deco_Pawn();
        else if (Input.GetKeyDown(KeyCode.W) && b.Get_Name() != "Init")
            Deco_Init();
    }

    private void Deco_Pawn()
    {
        b = new Pawn();
        Debug.Log(b.Get_All_Information());
    }

    private void Deco_Init()
    {
        b = new InitDeco();
        Debug.Log(b.Get_All_Information());
    }
}
