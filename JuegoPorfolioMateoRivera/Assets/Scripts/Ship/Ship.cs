using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public int life;
    public int score;
    public int hitScore;
    public float rotateSpeed;
    public float moveSpeed;
    public bool interactable;
    Rigidbody2D _rb;
    private ShipController _sc;
    public Dictionary<string, ShipCommand> _moveBehavs; //command de movimientos
    public Dictionary<KeyCode, IBulletBehaviour> _shootStyle; //switch entre armas

    public float xPos;
    public float yPos;

    //public Text lifeText;
    //public Text scoreText;

    OnScreenObjects _screenPositionControl;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _screenPositionControl = new OnScreenObjects(this.transform, xPos, yPos);
        _sc = new ShipController(this);
        _moveBehavs = new Dictionary<string, ShipCommand>();
        _moveBehavs.Add("Forward", new ShipCommand(this.transform.up, this.transform, moveSpeed, rotateSpeed));
        _moveBehavs.Add("Back", new ShipCommand(-this.transform.up, this.transform, moveSpeed, rotateSpeed));
        _moveBehavs.Add("Left", new ShipCommand(-this.transform.right, this.transform, moveSpeed, rotateSpeed));
        _moveBehavs.Add("Right", new ShipCommand(this.transform.right, this.transform, moveSpeed, rotateSpeed));
        EventsManager.SubscribeToEvent(MyEvents.ASTEROID_HIT, OnAsteroidHit);
        EventsManager.SubscribeToEvent(MyEvents.NON_INTERACTABLE, OnNotInteracting);
        EventsManager.SubscribeToEvent(MyEvents.INTERACTABLE, OnInteracting);
    }

    private void OnInteracting(object[] parameterContainer)
    {
        interactable = true;
        _rb.isKinematic = false;
        this.gameObject.SetActive(true);
    }

    private void OnNotInteracting(object[] parameterContainer)
    {
        interactable = false;
        _rb.isKinematic = true;
        this.gameObject.SetActive(false);
    }

    void Update ()
    {
        if (interactable)
        {
            _sc.Update();
        }
        _screenPositionControl.Update();
	}

    public void PerformShoot()
    {
        if (interactable)
        {
            EventsManager.TriggerEvent(MyEvents.SHOOT, transform);        
        }
    }

    public void ExecuteMovementCommands(string command)
    {
        _moveBehavs[command].Move();
    }
    public void ExecuteRotationCommands(string command)
    {
        _moveBehavs[command].Rotate();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        life--;
        EventsManager.TriggerEvent(MyEvents.LIFE_CHANGED, life, this.transform.position, this.transform);
    }
    private void OnAsteroidHit(object[] parameterContainer)
    {
        score += hitScore;
        EventsManager.TriggerEvent(MyEvents.SCORE_UPDATE, score);
    }
}
