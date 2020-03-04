using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCommand
{
    private Vector3 _direction;
    private Transform _trans;
    private float _spdMove;
    private float _spdRotate;
    public ShipCommand(Vector3 dir, Transform t, float speed, float speedRotate)
    {
        _direction = dir;
        _trans = t;
        _spdMove = speed;
        _spdRotate = speedRotate;
    }
    public void Move()
    {
        _trans.position += _trans.up * _spdMove * Time.deltaTime * Input.GetAxis("Vertical");
    }

    public void Rotate()
    {
        _trans.Rotate(0,0, -_spdRotate * Input.GetAxis("Horizontal"));
    }

}
