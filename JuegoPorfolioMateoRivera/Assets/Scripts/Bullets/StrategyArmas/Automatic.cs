using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatic : IBulletBehaviour
{

    float _speed;
    float _lifeTime;
    float _tick;
    int _triggerCount;
    Bullets _bullet;

    public Automatic(float speed, float lifeTime, Bullets bullet)
    {
        _speed = speed;
        _lifeTime = lifeTime;
        _bullet = bullet;
    }
    
    
    public void Update()
    {
        CheckTrigger();
        _tick += Time.deltaTime;
        if (_tick >= _lifeTime)
        {
            _bullet.ReturnBullet();
        }
        else
        {
            _bullet.transform.position += _bullet.transform.up * _speed * Time.deltaTime;
        }
    }
    public void Collision(Collision2D collision)
    {
        _bullet.ReturnBullet();
        EventsManager.TriggerEvent(MyEvents.ASTEROID_HIT);
    }
    public void Reset()
    {
        _triggerCount = 0;
    }

    public void CheckTrigger()
    {
        if (_triggerCount < 1)
        {
            EventsManager.TriggerEvent(MyEvents.AUTOMATIC_SHOOT_SOUND);
            _triggerCount++;
        }
    }

}
