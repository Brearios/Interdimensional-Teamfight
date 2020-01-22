using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance;
    public CharacterProfile mageClass;
    public CharacterProfile warriorClass;
    public CharacterProfile priestClass;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    CharacterProfile GetCharacterProfileForUnit (ScriptableUnit unit)
    {
        switch(unit.name)
        {
            case "Mage":
                return mageClass;

            case "Priest":
                return priestClass;

            case "Warrior":
                return warriorClass;

            default:
                return null;
        }
    }
}
