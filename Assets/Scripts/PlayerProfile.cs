using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance;
    public CharacterProfile mageHero;
    public CharacterProfile priestHero;
    public CharacterProfile warriorHero;
    public CharacterProfile rogueHero;
    public CharacterProfile plantHero;
    public CharacterProfile steamHero;
    public CharacterProfile spaceSoldier;
    public int currentEditingInteger;
    public ScriptableAbility currentDetailAbility;
    public int nextBattleScene;
    public int earnedCrystals;
    public int currentCrystals;
    public int totalGold;
    public int currentGold;
    public HeroData startingHero;
    public List<HeroData> UnlockedHeroes;
    public List<HeroData> heroes;



    // public Dictionary<ScriptableUnit, CharacterProfile> characterProfiles;
    public List<CharacterProfile> characterProfiles = new List<CharacterProfile>();
    
    public CharacterProfile CurrentEditingCharacter;

    // Code to set the CurrentEditingCharacter (starts as mageHero on Awake)
    // Code to use the CurrentEditingCharacter on the menu screen, and re-load the menu screen
    // Code to display the CurrentEditingCharacter.Sprite

    public void Awake()
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

        nextBattleScene = 0;

        UnlockedHeroes.Add(startingHero);

        characterProfiles.Add(mageHero);
        characterProfiles.Add(priestHero);
        characterProfiles.Add(warriorHero);
        characterProfiles.Add(rogueHero);
        characterProfiles.Add(steamHero);
        characterProfiles.Add(plantHero);
        characterProfiles.Add(spaceSoldier);

        currentEditingInteger = 0;
        // Sets the current editing character to the first one at the start of the script
        CurrentEditingCharacter = characterProfiles[currentEditingInteger];
        currentDetailAbility = CurrentEditingCharacter.ability2;

        //foreach (CharacterProfile characterStartupProfile in characterProfiles)
        //{
        //    EnsureInitialUnlockedAbilities(characterStartupProfile, "autoAtk", autoAtk, autoAtkUnlock);
        //    EnsureInitialUnlockedAbilities(characterStartupProfile, "ability1", ability1, ability1Unlock);
        //}
        foreach (CharacterProfile characterStartupProfile in characterProfiles)
        {
            EnsureInitialUnlockedAbilities(characterStartupProfile);
        }
    }

    void Start()
    {
        // Sets the current editing character to the index determined by the buttons when the scene reloads
        //if (UnlockedHeroes.Contains(startingHero))
        //{
        //    return;
        //}
        //UnlockedHeroes.Add(startingHero);
        //Debug.Log("Starting Hero Added.");
    }


    void Update()
    {
        CurrentEditingCharacter = characterProfiles[currentEditingInteger];
    }

    // Add default unlocked abilities to the list
    //void EnsureInitialUnlockedAbilities(CharacterProfile listProfile, string abilityName, ScriptableAbility ability, AbilityUnlock unlockedAbility)
    //{
    //    AbilityUnlock unlockAbility = new AbilityUnlock(abilityName, ability);
    //    listProfile.AbilityUnlocks.Add(unlockAbility);
    //}

    public CharacterProfile GetCharacterProfileForUnit(ScriptableUnit unit)
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

            case "PlantUnit":
                return plantHero;

            case "SteamTankUnit":
                return steamHero;

            case "SpaceSoldier":
                return spaceSoldier;

            default:
                return null;
        }
    }


    void EnsureInitialUnlockedAbilities(CharacterProfile unlockProfile)
    {
        unlockProfile.autoAtkUnlock = true;
        unlockProfile.ability1Unlock = true;
        //if (unlockProfile.ability2Unlock = null)
        //{
        //    unlockProfile.ability2Unlock = false;
        //}
    }

    void LockHighLevelAbilities(CharacterProfile lockProfile)
    {
        lockProfile.ability2Unlock = false;
        lockProfile.ability3Unlock = false;
        lockProfile.potionUnlock = false;
    }
}
