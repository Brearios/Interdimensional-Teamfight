using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance;
    public CharacterProfile mageHero;
    public CharacterProfile warriorHero;
    public CharacterProfile priestHero;
    public int currentEditingInteger = 0;

    // public CharacterProfile rogueHero; - menu not implemented yet

    // public Dictionary<ScriptableUnit, CharacterProfile> characterProfiles;
    public List<CharacterProfile> characterProfiles = new List<CharacterProfile>();
    
    public CharacterProfile CurrentEditingCharacter;

    // Code to set the CurrentEditingCharacter (starts as mageHero on Awake)
    // Code to use the CurrentEditingCharacter on the menu screen, and re-load the menu screen
    // Code to display the CurrentEditingCharacter.Sprite

    public CharacterProfile GetCharacterProfileForUnit (ScriptableUnit unit)
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

        CurrentEditingCharacter = characterProfiles[currentEditingInteger];

        characterProfiles.Add(mageHero);
        characterProfiles.Add(warriorHero);
        characterProfiles.Add(priestHero);
    }

    void Start()
    {
        CurrentEditingCharacter = characterProfiles[currentEditingInteger];
    }

    
    void Update()
    {
        
    }






}
