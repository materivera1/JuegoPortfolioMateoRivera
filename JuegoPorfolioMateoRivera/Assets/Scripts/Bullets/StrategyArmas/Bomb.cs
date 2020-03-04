using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : IBulletBehaviour
{
    float _lifeTime;
    float _tick;
    int _triggerCount;
    Bullets _bullet;
    LayerMask _layermask;
    Transform _trans;
    public Bomb(float lifeTime, Bullets bullet, LayerMask layermask, Transform t)
    {
        _lifeTime = lifeTime;
        _bullet = bullet;
        _layermask = layermask;
        _trans = t;
    }
    public void Update()
    {
        CheckTrigger();
        _tick += Time.deltaTime;
        if (_tick >= _lifeTime)
        {
            Explotion();
            _bullet.ReturnBullet();
        }
    }

    private void Explotion()
    {        
        var expRadius = Physics2D.OverlapCircleAll(_bullet.transform.position, 4, _layermask);        
        foreach (var item in expRadius)
        {
            item.gameObject.GetComponent<Meteorites>().LoseLife();
            EventsManager.TriggerEvent(MyEvents.ASTEROID_HIT);
        }
        EventsManager.TriggerEvent(MyEvents.BOOM, _trans.position, this._trans);
        //EventsManager.TriggerEvent(MyEvents.PARTICLE_RETURN);
    }
    public void Collision(Collision2D collision)
    {
        Explotion();
        _bullet.ReturnBullet();        
    }
    public void Reset()
    {
        _triggerCount = 0;
    }

    public void CheckTrigger()
    {
        if (_triggerCount < 1)
        {
            EventsManager.TriggerEvent(MyEvents.BOMB_SHOOT_SOUND);
            _triggerCount++;
        }
    }
}
