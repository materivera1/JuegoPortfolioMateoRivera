using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController
{
    Weapon _weapon;
    Dictionary<KeyCode, string> _myControls;
    public WeaponController(Weapon weapon)
    {
        _weapon = weapon;
        _myControls = new Dictionary<KeyCode, string>
        {
            { KeyCode.Alpha1, BulletTypes.NormalBullet },
            { KeyCode.Alpha2, BulletTypes.BombBullet },
            { KeyCode.Alpha3, BulletTypes.LaserBullet }
        };
    }
	
	public void Update ()
    {
        foreach (var item in _myControls)
        {
            if (Input.GetKeyDown(item.Key))
            {
                _weapon.weaponType = item.Value;
            }
        }
	}
}
