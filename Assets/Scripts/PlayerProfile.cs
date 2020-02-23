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
    public CharacterProfile rogueHero;
    public int currentEditingInteger;
    public int nextBattleScene;

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
            case "MageUnit":
                return mageHero;

            case "PriestUnit":
                return priestHero;

            case "WarriorUnit":
                return warriorHero;

            case "RogueUnit":
                return rogueHero;

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

        nextBattleScene = 2;

        characterProfiles.Add(mageHero);
        characterProfiles.Add(warriorHero);
        characterProfiles.Add(priestHero);
        characterProfiles.Add(rogueHero);

        currentEditingInteger = 0;
        // Sets the current editing character to the first one at the start of the script
        CurrentEditingCharacter = characterProfiles[currentEditingInteger];
    }

    void Start()
    {
        // Sets the current editing character to the index determined by the buttons when the scene reloads
        
    }

    
    void Update()
    {
        CurrentEditingCharacter = characterProfiles[currentEditingInteger];
    }






}
