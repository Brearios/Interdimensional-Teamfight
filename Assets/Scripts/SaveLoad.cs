using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
class SaveWrapper {
    public PlayerProfile profile;
}

public class SaveLoad : MonoBehaviour
{
    public string SavePath {
        get {
            return Application.persistentDataPath + "/gamesave.json";
        }
    }

    public void SavePlayerProfile()
    {
        string json = JsonUtility.ToJson(PlayerProfile.Instance);
        Debug.Log("Saving as JSON" + json);
        File.WriteAllText(SavePath, json);
    }

    public void LoadPlayerProfile()
    {
        string json = File.ReadAllText(SavePath);
        Debug.Log("Loading JSON" + json);
        JsonUtility.FromJsonOverwrite(json, PlayerProfile.Instance);
    }



}
