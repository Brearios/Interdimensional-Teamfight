using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public string json;
    public void SavePlayerProfile()
    {
        string json = JsonUtility.ToJson(PlayerProfile.Instance);
        Debug.Log("Saving as JSON" + json);
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.txt");
    }

    public void LoadPlayerProfile()
    {
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.txt", FileMode.Open);
        PlayerProfile.Instance = JsonUtility.FromJson<PlayerProfile>(json);
    }



}
