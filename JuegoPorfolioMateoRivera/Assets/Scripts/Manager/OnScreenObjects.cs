using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenObjects  
    {
    Transform _trans;
    float _yControl;
    float _xControl;

    public OnScreenObjects(Transform t, float x, float y)
    {
        _trans = t;
        _xControl = x;
        _yControl = y;
    }
	
	public void Update ()
    {
        var _t = _trans.position;
        if (_t.y > _yControl) _t.y = -_yControl;
        if (_t.y < -_yControl) _t.y = _yControl;
        if (_t.x > _xControl) _t.x = -_xControl;
        if (_t.x < -_xControl) _t.x = _xControl;
        _trans.position = _t;

    }
}
