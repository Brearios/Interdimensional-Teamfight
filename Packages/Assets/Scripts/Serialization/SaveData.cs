using UnityEngine;
using System.Collections;

[System.Serializable]
public class SaveData
{
    
    private static SaveData _current;
    public static SaveData current
    {
        get 
        {
            if (_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
    }
}
