using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController
{
    Ship _ship;

    public ShipController(Ship ship)
    {
        _ship = ship;
    }
	public void Update ()
    {
        CheckInputs();
	}

    public void CheckInputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _ship.ExecuteMovementCommands("Forward");
        }
        if (Input.GetKey(KeyCode.S))
        {
            _ship.ExecuteMovementCommands("Back");
        }
        if (Input.GetKey(KeyCode.A))
        {
            _ship.ExecuteRotationCommands("Left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            _ship.ExecuteRotationCommands("Right");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ship.PerformShoot();
        }
    }
}
