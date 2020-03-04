using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : IBulletBehaviour
{
    private float _lifeTime;
    private float _tick = 0;
    Bullets _bullet;
    int _triggerCount;
    public Laser(float lifeTime, Bullets bullet)
    {
        _lifeTime = lifeTime;
        _bullet = bullet;
    }
    public void Update()
    {
        CheckTrigger();
        _tick += Time.deltaTime;
        ScaleBullet();
        if (_tick >= _lifeTime)
        {
            _bullet.transform.localScale = new Vector3(1, 1, 1);
            _bullet.transform.Translate(new Vector3(0, 0, 0));
            _bullet.ReturnBullet();
            _tick = 0;
        }
    }
    public void Collision(Collision2D collision)
    {
        _bullet.transform.localScale = new Vector3(1, 1, 1);
        _bullet.transform.Translate(new Vector3(0, 0, 0));
        _bullet.ReturnBullet();
        EventsManager.TriggerEvent(MyEvents.ASTEROID_HIT);
    }

    void ScaleBullet()
    {
        _bullet.transform.Translate(new Vector3(0, 13, 0));
        _bullet.transform.localScale = new Vector3(1, 20, 1); 
    }
    public void Reset()
    {
        _triggerCount = 0;
    }

    public void CheckTrigger()
    {
        if (_triggerCount < 1)
        {
            EventsManager.TriggerEvent(MyEvents.LASER_SHOOT_SOUND);
            _triggerCount++;
        }
    }
}
