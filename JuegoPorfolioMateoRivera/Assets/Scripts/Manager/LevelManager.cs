using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int maxScore;
	void Start ()
    {
        EventsManager.Init();
        EventsManager.SubscribeToEvent(MyEvents.LIFE_CHANGED, OnLifeChanged);
        EventsManager.SubscribeToEvent(MyEvents.SCORE_UPDATE, OnScoreUpdated);
        var player = Instantiate((GameObject)Resources.Load("Player", typeof(GameObject)));
        player.transform.position = Vector3.zero;       
	}


    private void OnLifeChanged(object[] parameterContainer)
    {
        if ((int)parameterContainer[0] < 1) EventsManager.TriggerEvent(MyEvents.LOSE);
    }

    private void OnScoreUpdated(object[] parameterContainer)
    {
        if ((int)parameterContainer[0] >= maxScore) EventsManager.TriggerEvent(MyEvents.WIN);
    }
    public void RestartGame()
    {
        EventsManager.TriggerEvent(EventType.CLEAR_ALL);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
