using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance;
    public CharacterProfile mageHero;
    public CharacterProfile warriorHero;
    public CharacterProfile priestHero;
    // public CharacterProfile rogueHero; - menu not implemented yet

    public Dictionary<ScriptableUnit, CharacterProfile> characterProfiles;

    CharacterProfile GetCharacterProfileForUnit(ScriptableUnit unit)
    {
        switch (unit.name)
        {
            case "Mage":
                return mageHero;

            case "Priest":
                return priestHero;

            case "Warrior":
                return warriorHero;

            default:
                return null;
        }
    }

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
        duplicateProfileCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void duplicateProfileCheck()
    {
        if (mageHero != mageHero.created)
        {
            DontDestroyOnLoad(mageHero);
            mageHero.created = true;
        }
        else
        {
            Destroy(mageHero);
        }
        if (priestHero != priestHero.created)
        {
            DontDestroyOnLoad(priestHero);
            priestHero.created = true;
        }
        else
        {
            Destroy(priestHero);
        }
        if (warriorHero != warriorHero.created)
        {
            DontDestroyOnLoad(warriorHero);
            warriorHero.created = true;
        }
        else
        {
            Destroy(warriorHero);
        }
    }
   
}
