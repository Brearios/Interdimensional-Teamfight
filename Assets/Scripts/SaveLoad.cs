using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]

public class SaveLoad : MonoBehaviour
{
    public string SavePath {
        get {
            return Application.persistentDataPath + "/gamesave.json";
        }
    }

    public void SavePlayerProfile()
    {
        AudioManager.instance.Play("Click");
        string json = JsonUtility.ToJson(PlayerProfile.Instance, true);
        Debug.Log("Saving as JSON" + json);
        File.WriteAllText(SavePath, json);
    }

    public void LoadPlayerProfile()
    {
        AudioManager.instance.Play("Click");
        string json = File.ReadAllText(SavePath);
        Debug.Log("Loading JSON" + json);
        JsonUtility.FromJsonOverwrite(json, PlayerProfile.Instance);
    }



}
