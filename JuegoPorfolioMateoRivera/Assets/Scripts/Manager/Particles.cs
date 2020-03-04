using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    float _disableBombParticle;
    float _disableAsteroidParticle;
    bool _triggeredAsteroidParticle;
    GameObject _shipHit;
    GameObject _bomb;
    GameObject _asteroidHit;
    Pool<GameObject> _asteroidParticlePool;
    Pool<GameObject> _bombParticlePool;


	void Start ()
    {
        EventsManager.SubscribeToEvent(MyEvents.BOOM, OnBoom);
        EventsManager.SubscribeToEvent(MyEvents.LIFE_CHANGED, OnLifeChanged);
        EventsManager.SubscribeToEvent(MyEvents.ASTEROID_CRASH, OnAsteroidHit);
        EventsManager.SubscribeToEvent(MyEvents.RETURN_PARTICLE_ASTEROID, OnReturnParticleAsteroid);
        EventsManager.SubscribeToEvent(MyEvents.RETURN_PARTICLE_BOMB, OnReturnParticleBomb);
        _bomb = (GameObject)Resources.Load("Boom", typeof(GameObject));
        _shipHit = Instantiate((GameObject)Resources.Load("ShipHit", typeof(GameObject)));
        _shipHit.SetActive(false);
        _asteroidHit = (GameObject)Resources.Load("AsteroidHit", typeof(GameObject));
        _asteroidParticlePool = new Pool<GameObject>(8, AsteroidHitParticle, InitializeParticle, DisposeParticle, false);
        _bombParticlePool = new Pool<GameObject>(8, BombParticlesFactory, InitializeParticle, DisposeParticle, false);
    }

    private void OnReturnParticleBomb(object[] parameterContainer)
    {
        var part = (GameObject)parameterContainer[0];
        StartCoroutine(ReturnBombParticleCoroutine(part));
    }

    private void OnReturnParticleAsteroid(object[] parameterContainer)
    {
        var part = (GameObject)parameterContainer[0];
        StartCoroutine(ReturnAstParticleCoroutine(part));
    }
    private void OnAsteroidHit(object[] parameterContainer)
    {
        var trans = (Transform)parameterContainer[1];
        var particle = _asteroidParticlePool.GetObjectFromPool(); ;
        particle.transform.position = trans.position;
        EventsManager.TriggerEvent(MyEvents.RETURN_PARTICLE_ASTEROID, particle);
    }


    private void OnLifeChanged(object[] parameterContainer)
    {        
        var trans = (Transform)parameterContainer[2];
        var particle = _shipHit;
        particle.transform.position = trans.position;
        particle.SetActive(true);
        StartCoroutine(DisposeShipHitParticleCoroutine(particle));       
    }

    private void OnBoom(object[] parameterContainer)
    {
        var trans = (Transform)parameterContainer[1];
        var particle = _bombParticlePool.GetObjectFromPool();
        particle.transform.position = trans.position;
        EventsManager.TriggerEvent(MyEvents.RETURN_PARTICLE_BOMB, particle);
    }

    public GameObject BombParticlesFactory()
    {
        return Instantiate<GameObject>(_bomb);
    }
    public GameObject AsteroidHitParticle()
    {
        return Instantiate<GameObject>(_asteroidHit);
    }
    public void ReturnBombParticle(GameObject bomb)
    {
        _bombParticlePool.DisablePoolObject(bomb);
    }
    private void ReturnAsteroidParticle(GameObject asteroid)
    {
        _asteroidParticlePool.DisablePoolObject(asteroid);
    }
    void InitializeParticle(GameObject obj)
    {
        obj.gameObject.SetActive(true);
    }
    void DisposeParticle(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }
    IEnumerator ReturnAstParticleCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        ReturnAsteroidParticle(obj);
        yield return null;
    }
    IEnumerator ReturnBombParticleCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        ReturnBombParticle(obj);
        yield return null;
    }
    IEnumerator DisposeShipHitParticleCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f);
        obj.gameObject.SetActive(false);
        yield return null;
    }
}
