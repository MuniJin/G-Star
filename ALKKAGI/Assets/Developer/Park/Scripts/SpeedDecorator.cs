using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedDecorator : IFPSMovement
{
    private readonly IFPSMovement _fpsMovement;
    private readonly IPlayerStats _playerStats;

    public SpeedDecorator(IFPSMovement fpsMovement, IPlayerStats playerStats)
    {
        _fpsMovement = fpsMovement;
        _playerStats = playerStats;
    }

    public void MoveForward()
    {
        Console.WriteLine($"Moving forward at speed {_playerStats.Speed}");
        _fpsMovement.MoveForward();
    }

    public void MoveBackward()
    {
        Console.WriteLine($"Moving backward at speed {_playerStats.Speed}");
        _fpsMovement.MoveBackward();
    }

    public void StrafeLeft()
    {
        Console.WriteLine($"Strafing left at speed {_playerStats.Speed}");
        _fpsMovement.StrafeLeft();
    }

    public void StrafeRight()
    {
        Console.WriteLine($"Strafing right at speed {_playerStats.Speed}");
        _fpsMovement.StrafeRight();
    }

    public void Jump()
    {
        Console.WriteLine("Jumping");
        _fpsMovement.Jump();
    }
}
