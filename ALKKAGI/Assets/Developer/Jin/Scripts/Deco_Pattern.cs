using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Unit
{
    public string Name = string.Empty;
    public int Hp = 100;
    public float Speed = 5f;
    public float Shoot_Delay = 0.5f;
    public float Skill_CoolDown = 3f;

    public virtual void Get_All_Information() { Debug.Log("Test"); }
}

public class Player_Unit : Base_Unit
{
    public Player_Unit()
    {
        this.Name = "Player";
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("asdf");
        }
    }
    public void Jump() { }
    public void Shoot() { }
}

public class AI_Unit : Base_Unit
{
    public AI_Unit()
    {
        this.Name = "AI Enemy";
    }

    public void Move() { }
    public void Jump() { }
    public void Shoot() { }
}

/////////////////////////////////////////////////////////////////
#region À¯´Ö µ¥ÄÚ
public abstract class Decorator_Unit : Base_Unit
{
    protected Base_Unit b_unit;
}

// ÂÌ
public class Pawn : Decorator_Unit
{
    public Pawn(Base_Unit b)
    {
        this.b_unit = b;
        this.Name = "Pawn";
        this.Hp += 10;
    }

    public override void Get_All_Information()
    {
        Debug.Log($"{this.b_unit} | {this.Name} | {this.Hp}");
    }
}

// ¸¶
public class Knight : Decorator_Unit
{
    public Knight(Base_Unit b)
    {
        this.b_unit = b;
        this.Name = "Knight";
    }
}

// »ó 
public class Elephant : Decorator_Unit
{
    public Elephant(Base_Unit b)
    {
        this.b_unit = b;
        this.Name = "Elephant";
    }
}

// Æ÷ 
public class Cannon : Decorator_Unit
{
    public Cannon(Base_Unit b)
    {
        this.b_unit = b;
        this.Name = "Cannon";
    }
}

// Â÷ 
public class Rook : Decorator_Unit
{
    public Rook(Base_Unit b)
    {
        this.b_unit = b;
        this.Name = "Rock";
    }
}

// »ç 
public class Guards : Decorator_Unit
{
    public Guards(Base_Unit b)
    {
        this.b_unit = b;
        this.Name = "Guards";
    }
}

// ¿Õ
public class King : Decorator_Unit
{
    public King(Base_Unit b)
    {
        this.b_unit = b;
        this.Name = "King";
    }
}
#endregion

public class Deco_Pattern : MonoBehaviour
{
    public GameObject go;
    public Base_Unit b;

    private void Start()
    {
        go.AddComponent<Player_Unit>();
        //Base_Unit b = new Player_Unit();

        //b = new Pawn(b);
        //b.Get_All_Information();
    }
}
