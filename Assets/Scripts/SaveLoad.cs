using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]

public class SaveLoad : MonoBehaviour
{
    public string SavePath
    {
        get
        {
            return Application.persistentDataPath + "/gamesave.json";
        }
    }

    public string MathPath
    {
        get
        {
            return Application.persistentDataPath + "/xpandstats.json";
        }
    }

    public void SaveData()
    {
        AudioManager.Instance.Play("Click");
        string jsonPlayerProf = JsonUtility.ToJson(PlayerProfile.Instance, true);
        Debug.Log("Saving as JSON" + jsonPlayerProf);
        File.WriteAllText(SavePath, jsonPlayerProf);

        string xpAndStats = JsonUtility.ToJson(PlayerProfile.Instance, true);
        Debug.Log("Saving as JSON" + xpAndStats);
        File.WriteAllText(MathPath, xpAndStats);
    }

    public void LoadData()
    {
        AudioManager.Instance.Play("Click");
        string jsonPlayerProf = File.ReadAllText(SavePath);
        Debug.Log("Loading JSON" + jsonPlayerProf);
        JsonUtility.FromJsonOverwrite(jsonPlayerProf, PlayerProfile.Instance);

        string xpAndStats = File.ReadAllText(MathPath);
        Debug.Log("Loading JSON" + xpAndStats);
        JsonUtility.FromJsonOverwrite(xpAndStats, PlayerProfile.Instance);
    }
}
