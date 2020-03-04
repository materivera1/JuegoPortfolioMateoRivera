using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Main : MonoBehaviour {

    private static Main _instance;
    public static Main Instance
    {
        get
        {
            return _instance;
        }
    }
    
    void Awake()
    {
        _instance = this;
        LocalizationManager loc = new LocalizationManager();
    }

    public void DownloadTextsDatabase()
    {
        LoadFromDisk();
    }

    public void LoadFromDisk()
    {
        string data = File.ReadAllText("Save/localization.json");
        List<object> parsedData = (List<object>)MiniJSON.Json.Deserialize(data);
        LocalizationManager.Instance.SetTexts(parsedData);
    }
}
