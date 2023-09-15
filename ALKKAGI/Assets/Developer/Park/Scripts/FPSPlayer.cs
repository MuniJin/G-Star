using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSPlayer : IFPSMovement, IPlayerStats
{
    public int HP { get; set; }
    public float Speed { get; set; }
    public float AttackDelay { get; set; }
    public float GlobalVariable { get; set; }

    public FPSPlayer()
    {
        HP = 100;
        Speed = 5.0f;
        AttackDelay = 1.0f;
        GlobalVariable = 0.0f;
    }

    public void MoveForward()
    {
        Console.WriteLine("Moving forward");
    }

    public void MoveBackward()
    {
        Console.WriteLine("Moving backward");
    }

    public void StrafeLeft()
    {
        Console.WriteLine("Strafing left");
    }

    public void StrafeRight()
    {
        Console.WriteLine("Strafing right");
    }

    public void Jump()
    {
        Console.WriteLine("Jumping");
    }
}