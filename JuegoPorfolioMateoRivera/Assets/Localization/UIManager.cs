using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIManager : MonoBehaviour 
{
    public Text[] localizableTexts;
    public Text lifeText;
    public Text scoreText;
    public int score;
    public int life = 3;
    public int hitScore = 10;
    public GameObject loseMenu;
    public GameObject winMenu;
    public GameObject startMenu;
    public GameObject englishButton;
    public GameObject SpanishButton;
    public UserLanguage language;
    LocalizationManager _myLoc;


    private void Start()
    {
        _myLoc = new LocalizationManager();
        EventsManager.SubscribeToEvent(MyEvents.SCORE_UPDATE, OnScoreUpdate);
        EventsManager.SubscribeToEvent(MyEvents.LIFE_CHANGED, OnLifeChanged);
        EventsManager.SubscribeToEvent(MyEvents.WIN, OnLevelWon);
        EventsManager.SubscribeToEvent(MyEvents.LOSE, OnLevelLost);
        language = UserLanguage.English;
        ShowStartWindow();
    }

    private void OnLifeChanged(object[] parameterContainer)
    {
        life--;
    }

    private void OnScoreUpdate(object[] parameterContainer)
    {
        score += hitScore;
    }

    private void ShowStartWindow()
    {
        var start = !startMenu.activeSelf;
        startMenu.SetActive(start);
        if(start) EventsManager.TriggerEvent(MyEvents.NON_INTERACTABLE);
        else EventsManager.TriggerEvent(MyEvents.INTERACTABLE);
    }
    public void Play()
    {
        ShowStartWindow();
        englishButton.SetActive(false);
        SpanishButton.SetActive(false);
    }
    private void OnLevelWon(object[] parameterContainer)
    {
        ShowWinWindow();
    }

    private void ShowWinWindow()
    {
        var winWindow = !winMenu.activeSelf;
        winMenu.SetActive(winWindow);
        if (winWindow) EventsManager.TriggerEvent(MyEvents.NON_INTERACTABLE);
        else EventsManager.TriggerEvent(MyEvents.INTERACTABLE);
    }

    private void OnLevelLost(object[] parameterContainer)
    {
        ShowLoseWindow();        
    }

    private void ShowLoseWindow()
    {
        var loseWindow = !loseMenu.activeSelf;
        loseMenu.SetActive(loseWindow);
        if(loseWindow) EventsManager.TriggerEvent(MyEvents.NON_INTERACTABLE);
        else EventsManager.TriggerEvent(MyEvents.INTERACTABLE);
    }
    
    void Update () 
    {
        UpdateTexts();
        lifeText.text = "" + life;
        scoreText.text = "" + score;
    }
    
    private void UpdateTexts()
    {
        for (int i = 0; i < localizableTexts.Length; i++)
        {
            string key = gameObject.name + "." + localizableTexts[i].gameObject.name;
            localizableTexts[i].text = LocalizationManager.Instance.TryGetText(language, key);
        }
    }
    public void Spanish()
    {
        language = UserLanguage.Spanish;
    }
    public void English()
    {
        language = UserLanguage.English;
    }
}


public enum UserLanguage
{
    English,
    Spanish,
}
