using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteoriteSpawner : MonoBehaviour
{
    public Meteorites meteoritePrefab;
    private Pool<Meteorites> _meteoritesPool;
    private static MeteoriteSpawner _instance;
    public static MeteoriteSpawner Instance { get { return _instance; } }
    public int metStock;

    void Awake()
    {
        meteoritePrefab = (Meteorites)Resources.Load("Meteorite", typeof(Meteorites));
        _instance = this;
        _meteoritesPool = new Pool<Meteorites>(metStock, MeteoritesFactory, Meteorites.InitializeMeteorite, Meteorites.DisposeMeteorite, false);
        EventsManager.SubscribeToEvent(MyEvents.ASTEROID_RETURN, OnReturnAsteroid);
        CheckMaxStock();

    }

    public Meteorites MeteoritesFactory()
    {
        return Instantiate<Meteorites>(meteoritePrefab);
    }

    public void ReturnMeteorite(Meteorites meteorite)
    {
        _meteoritesPool.DisablePoolObject(meteorite);
    }
    public void OnReturnAsteroid(object[] wrapperParam)
    {
        ReturnMeteorite((Meteorites)wrapperParam[1]);
    }
    void CheckMaxStock()
    {        
          for (int i = metStock - 1; i >= 0; i--)
          {
                _meteoritesPool.GetObjectFromPool();
          }        
    }
}
