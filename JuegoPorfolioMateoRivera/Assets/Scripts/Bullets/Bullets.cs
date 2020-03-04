using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;
    public float automaticLifeTime;
    public float laserLifeTime;
    public float bombLifeTime;
    private float _tick;
    private bool _alive;
    public LayerMask layermask;
    public IBulletBehaviour bul;
    Dictionary<string, IBulletBehaviour> _bulletsBehav;
    private void Start()
    {
        bul.Reset();
    }
    private void Update()
    {
        bul.Update();
    }
    public static void DisposeBullet(Bullets bulletObj)
    {
        bulletObj.gameObject.SetActive(false);
    }

    public static void InitializeBullet(Bullets bulletObj)            
    {
        bulletObj.gameObject.SetActive(true);
        bulletObj.SwitchBetweenBullets();             
    }
    public void SwitchBetweenBullets()
    {
        _bulletsBehav = new Dictionary<string, IBulletBehaviour>
        {
            {BulletTypes.NormalBullet,  new Automatic(speed, automaticLifeTime, this)},
            {BulletTypes.BombBullet,  new Bomb(bombLifeTime, this, layermask, this.transform)},
            {BulletTypes.LaserBullet,  new Laser(laserLifeTime, this)}
        };
        bul = _bulletsBehav[BulletTypes.NormalBullet];
    }
    
    public void CheckBehavior(string check)
    {
        if (!_bulletsBehav.ContainsKey(check)) return;
        bul = _bulletsBehav[check];
    }
    
    public void ReturnBullet()
    {
        EventsManager.TriggerEvent(MyEvents.BULLET_RETURN, this);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        bul.Collision(collision);
    }
}
