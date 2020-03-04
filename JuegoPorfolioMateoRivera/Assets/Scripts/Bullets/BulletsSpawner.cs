using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsSpawner : MonoBehaviour
{
    public Bullets bulletPrefab;
    private Pool<Bullets> _bulletPool;

    private static BulletsSpawner _instance;
    public static BulletsSpawner Instance { get { return _instance; } }

    void Awake()
    {
        bulletPrefab = (Bullets)Resources.Load("bullet", typeof(Bullets));
        //es necesario combinarlo con una lookuptable o asi esta bien?

        _instance = this;
        _bulletPool = new Pool<Bullets>(8, BulletFactory, Bullets.InitializeBullet, Bullets.DisposeBullet, true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bulletPool.GetObjectFromPool();
        }
    }

    private Bullets BulletFactory()
    {
        return Instantiate<Bullets>(bulletPrefab);
    }

    public void ReturnBullet(Bullets bullet)
    {
        _bulletPool.DisablePoolObject(bullet);
    }
}
