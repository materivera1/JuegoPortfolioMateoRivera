using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource AS;
    private AudioClip _automaticShootSound;
    private AudioClip _bombShootSound;
    private AudioClip _laserShootSound;
    private AudioClip _explotionSound;
    private AudioClip _pointsSound;
    private AudioClip _damageSound;
    private AudioClip _winSound;
    private AudioClip _loseSound;

    void Start ()
    {
        EventsManager.SubscribeToEvent(MyEvents.SCORE_UPDATE, OnScoreUpdate);
        EventsManager.SubscribeToEvent(MyEvents.LIFE_CHANGED, OnLifeChanged);
        EventsManager.SubscribeToEvent(MyEvents.WIN, OnLevelWon);
        EventsManager.SubscribeToEvent(MyEvents.LOSE, OnLevelLost);
        EventsManager.SubscribeToEvent(MyEvents.LASER_SHOOT_SOUND, OnLaserShoot);
        EventsManager.SubscribeToEvent(MyEvents.BOMB_SHOOT_SOUND, OnBombShoot);
        EventsManager.SubscribeToEvent(MyEvents.AUTOMATIC_SHOOT_SOUND, OnAutomaticShoot);
        EventsManager.SubscribeToEvent(MyEvents.BOOM, OnExplotion);

    }

    private void OnExplotion(object[] parameterContainer)
    {
        _explotionSound = (AudioClip)Resources.Load(MyResources.EXPLOTION_SOUND, typeof(AudioClip));
        AS.PlayOneShot(_explotionSound);
    }

    private void OnAutomaticShoot(object[] parameterContainer)
    {
        _automaticShootSound = (AudioClip)Resources.Load(MyResources.AUTOMATIC_SHOOT_SOUND, typeof(AudioClip));
        AS.PlayOneShot(_automaticShootSound);
    }

    private void OnBombShoot(object[] parameterContainer)
    {
        _bombShootSound = (AudioClip)Resources.Load(MyResources.BOMB_SHOOT_SOUND, typeof(AudioClip));
        AS.PlayOneShot(_bombShootSound);
    }

    private void OnLaserShoot(object[] parameterContainer)
    {
        _laserShootSound = (AudioClip)Resources.Load(MyResources.LASER_SHOOT_SOUND, typeof(AudioClip));
        AS.PlayOneShot(_laserShootSound);
    }

    private void OnLevelWon(object[] parameterContainer)
    {
        _winSound = (AudioClip)Resources.Load(MyResources.WIN_SOUND, typeof(AudioClip));
        AS.PlayOneShot(_winSound);
    }

    private void OnLevelLost(object[] parameterContainer)
    {
        _loseSound = (AudioClip)Resources.Load(MyResources.LOSE_SOUND, typeof(AudioClip));
        AS.PlayOneShot(_loseSound);
    }

    private void OnLifeChanged(object[] parameterContainer)
    {
        _damageSound = (AudioClip)Resources.Load(MyResources.DAMAGE_SOUND, typeof(AudioClip));
        AS.PlayOneShot(_damageSound);
    }

    private void OnScoreUpdate(object[] parameterContainer)
    {
        _pointsSound = (AudioClip)Resources.Load(MyResources.POINTS_SOUND, typeof(AudioClip));
        AS.PlayOneShot(_pointsSound);
    }
}
