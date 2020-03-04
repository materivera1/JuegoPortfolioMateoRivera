using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteorites : MonoBehaviour
{
    public float speed;
    public float lifeSpawn;
    public float lifeTime;
    private float _tick;
    private bool _alive;
    private int _posNumber;
    private int _life;
    private int _maxLife = 2;

    public float xPos;
    public float yPos;

    OnScreenObjects _screenPositionControl;

    private void Awake()
    {
        _posNumber = Random.Range(0, 4);
        _life = _maxLife;
    }
    
    private void Start()
    {
        var rand = Random.Range(-20f, 20f);
        transform.right = new Vector3(rand, rand, 0) - transform.position;
        _screenPositionControl = new OnScreenObjects(this.transform, xPos, yPos);
    }

    public void Initialize()
    {
        _tick = 0;

        if (_posNumber == 0)
        {
            transform.position = new Vector3(-15, Random.Range(-12, 12), 0);
        }
        else if (_posNumber == 1)
        {
            transform.position = new Vector3(Random.Range(-15 ,15), 14, 0);
        }
        else if (_posNumber == 2)
        {
            transform.position = new Vector3(15, Random.Range(-12, 12), 0);
        }
        else
        {
            transform.position = new Vector3(Random.Range(-15, 15), -14, 0);
        }
    }

    private void Update()
    {
        _tick += Time.deltaTime;
        transform.position += transform.right * speed * Time.deltaTime;
        _screenPositionControl.Update();
        
    }
    public void ReturnAsteroid()
    {
        EventsManager.TriggerEvent(MyEvents.ASTEROID_RETURN, transform.position, this);
    }
    public static void InitializeMeteorite(Meteorites meteoriteObj)
    {
        meteoriteObj.gameObject.SetActive(true);
        meteoriteObj.Initialize();
    }

    public static void DisposeMeteorite(Meteorites meteoriteObj)
    {
        meteoriteObj.gameObject.SetActive(false);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {        
        LoseLife();        
    }

    public void LoseLife()
    {
        var scale = transform.localScale;
        _life--;
        if (_life == 1)
        {
            var right = Random.Range(-2f, 2f);
            EventsManager.TriggerEvent(MyEvents.ASTEROID_CRASH, this.transform, this.transform);
            transform.localScale = new Vector3(scale.x / 2, scale.y / 2, scale.z / 2);
            transform.right = new Vector3(Random.Range(-right, right), Random.Range(-right, right), 0);
        }
        if (_life == 0)
        {
            EventsManager.TriggerEvent(MyEvents.ASTEROID_CRASH, this.transform.position, this.transform);
            transform.localScale = new Vector3(scale.x * 2, scale.y * 2, scale.z * 2);
            _life = _maxLife;
            ReturnAsteroid();
        }
    }
}
