using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullets bulPref;
    Pool<Bullets> _poolBullet;
    WeaponController _wc;
    public string weaponType;   
    void Awake ()
    {
        weaponType = BulletTypes.NormalBullet;
        _poolBullet = new Pool<Bullets>(8, BulletFactory , Bullets.InitializeBullet, Bullets.DisposeBullet, true);
        _wc = new WeaponController(this);
        EventsManager.SubscribeToEvent(MyEvents.SHOOT, ShootShipEvent);
        EventsManager.SubscribeToEvent(MyEvents.BULLET_RETURN, BulletReturnEvent);
    }
	
	void Update ()
    {
        _wc.Update();
	}

    private Bullets BulletFactory()
    {
        return Instantiate<Bullets>(bulPref);
    }

    public void ReturnBullet(Bullets bullet)
    {
        _poolBullet.DisablePoolObject(bullet);
    }
    void ShootShipEvent(object[] wrapperParam)
    {
        var trans = (Transform)transform;
        var bullet = GetBulletFromPool();
        bullet.transform.position = trans.position;
        bullet.transform.up = trans.up;
    }
    void BulletReturnEvent(object[] wrapperParam)
    {
        ReturnBullet((Bullets)wrapperParam[0]);
    }
    public Bullets GetBulletFromPool()
    {
        var bullet = _poolBullet.GetObjectFromPool();
        bullet.CheckBehavior(weaponType);
        return bullet;
    }

}
