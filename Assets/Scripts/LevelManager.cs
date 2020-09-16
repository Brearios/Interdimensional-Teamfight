using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    // public int currentLevel;
    public ScriptableLevel currentLevel;
    public int currentLevelInt;
    public List<ScriptableLevel> levelList;
    public bool startingCharactersSpawned;
    public int battleParticipants;
    public float battleParticipantsScaling;
    public enum BattleSize { skirmish, brawl, raid, battle };
    public BattleSize battleSize;
    public float npcHealthScalingNewGamePlus;
    public float npcAttackPowerScalingNewGamePlus;
    public float npcAbilityPowerScalingNewGamePlus;

    // List of prefabs to instantiate - list for heroes and enemies?

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Setting level to Index in levelList, accounting for Title (0) and Character Menu (1)
        // Using  nextBattleScene instead of replacing it throughout code
        startingCharactersSpawned = false;
        currentLevelInt = PlayerProfile.Instance.nextBattleScene;
        currentLevel = levelList[currentLevelInt];
        battleParticipants = 0;

        // Count total Participants for Scaling
        foreach (LevelPrefabData unitSpawnData in currentLevel.nPCSpawnData)
        {
            for (int i = 0; i < unitSpawnData.numberOfInstances; i++)
            {
                battleParticipants++;
            }
        }
        foreach (HeroData unlockedHeroSpawnData in PlayerProfile.Instance.UnlockedHeroes)
        {
            battleParticipants++;
        }

        SetScalingThreshold();

        // Code to select level in list that matches CurrentLevelInt and set to currentLevel;
        foreach (LevelPrefabData unitSpawnData in currentLevel.nPCSpawnData)
        {
            for (int i = 0; i < unitSpawnData.numberOfInstances; i++)
            {
                SpawnAndScaleFighter(unitSpawnData.characterPrefab, GenerateRandomPosition(unitSpawnData.spawn), (int)unitSpawnData.size, PlayerProfile.Instance.newGamePlusActive, PlayerProfile.Instance.newGamePlusIterator);
            }
        }
        foreach (HeroData unlockedHeroSpawnData in PlayerProfile.Instance.UnlockedHeroes)
        {
            SpawnAndScaleFighter(unlockedHeroSpawnData.characterPrefab, GenerateRandomPosition(unlockedHeroSpawnData.spawn), 1);
            Debug.Log($"Spawned hero { unlockedHeroSpawnData.characterPrefab.name}");
        }
        startingCharactersSpawned = true;
    }

    private void SetScalingThreshold()
    {
        if (battleParticipants <= MagicNumbers.Instance.skirmishScalingParticipantThreshold)
        {
            battleSize = BattleSize.skirmish;
        }

        else if (battleParticipants <= MagicNumbers.Instance.brawlScalingParticipantThreshold)
        {
            battleSize = BattleSize.brawl;
        }

        else if (battleParticipants <= MagicNumbers.Instance.raidScalingParticipantThreshold)
        {
            battleSize = BattleSize.raid;
        }

        else
        {
            battleSize = BattleSize.battle;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Overloaded Method to work with both LevelPrefabData and HeroList
    public Vector3 GenerateRandomPosition(LevelPrefabData.SpawnRegion spawn)
    {
        float minXPosition;
        float maxXPosition;
        float minYPosition;
        float maxYPosition;
        switch (spawn)
        {
            case LevelPrefabData.SpawnRegion.Anywhere: //Anywhere
                minXPosition = -8;
                maxXPosition = 8;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionAnywhere = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionAnywhere;
            case LevelPrefabData.SpawnRegion.AlliedMelee: //AlliedMelee
                minXPosition = -4;
                maxXPosition = -0.75f;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionAlliedMelee = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionAlliedMelee;
            case LevelPrefabData.SpawnRegion.AlliedRanged: //AlliedRanged
                minXPosition = -8;
                maxXPosition = -4;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionAlliedRanged = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionAlliedRanged;
            case LevelPrefabData.SpawnRegion.AlliedSide: //AlliedSide
                minXPosition = -8;
                maxXPosition = 0.75f;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionAlliedSide = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionAlliedSide;
            case LevelPrefabData.SpawnRegion.EnemyMelee: //EnemyMelee
                minXPosition = 0.75f;
                maxXPosition = 8;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionEnemyMelee = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionEnemyMelee;
            case LevelPrefabData.SpawnRegion.EnemyRanged: //EnemyRanged
                minXPosition = 4;
                maxXPosition = 8;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionEnemyRanged = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionEnemyRanged;
            case LevelPrefabData.SpawnRegion.EnemySide: //EnemySide
                minXPosition = 0.75f;
                maxXPosition = 8;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionEnemySide = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionEnemySide;
        }
        Vector3 randomBoundariesUndefined = new Vector3(0, 0, 0);
        return randomBoundariesUndefined;
    }

    public Vector3 GenerateRandomPosition(HeroData.SpawnRegion spawn)
    {
        float minXPosition;
        float maxXPosition;
        float minYPosition;
        float maxYPosition;
        switch (spawn)
        {
            case HeroData.SpawnRegion.Anywhere: //Anywhere
                minXPosition = -8;
                maxXPosition = 8;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionAnywhere = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionAnywhere;
            case HeroData.SpawnRegion.AlliedMelee: //AlliedMelee
                minXPosition = -4;
                maxXPosition = -0.75f;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionAlliedMelee = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionAlliedMelee;
            case HeroData.SpawnRegion.AlliedRanged: //AlliedRanged
                minXPosition = -8;
                maxXPosition = -4;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionAlliedRanged = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionAlliedRanged;
            case HeroData.SpawnRegion.AlliedSide: //AlliedSide
                minXPosition = -8;
                maxXPosition = 0.75f;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionAlliedSide = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionAlliedSide;
            case HeroData.SpawnRegion.EnemyMelee: //EnemyMelee
                minXPosition = 0.75f;
                maxXPosition = 8;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionEnemyMelee = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionEnemyMelee;
            case HeroData.SpawnRegion.EnemyRanged: //EnemyRanged
                minXPosition = 4;
                maxXPosition = 8;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionEnemyRanged = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionEnemyRanged;
            case HeroData.SpawnRegion.EnemySide: //EnemySide
                minXPosition = 0.75f;
                maxXPosition = 8;
                minYPosition = -4.7f;
                maxYPosition = 2.9f;
                Vector3 randomPositionEnemySide = new Vector3(UnityEngine.Random.Range(minXPosition, maxXPosition), UnityEngine.Random.Range(minYPosition, maxYPosition), 0.0f);
                return randomPositionEnemySide;
        }
        Vector3 randomBoundariesUndefined = new Vector3(0, 0, 0);
        return randomBoundariesUndefined;
    }

    // NPC Version
    void SpawnAndScaleFighter(GameObject characterPrefab, Vector3 randomSpawnPosition, int spawnSize, bool newGamePlusActive, int newGamePlusIterator)
    {
        if (!newGamePlusActive)
        {
            GameObject Fighter = Instantiate(characterPrefab, randomSpawnPosition, characterPrefab.transform.rotation);
            FighterScaler(battleSize, spawnSize);
            Fighter.transform.localScale = new Vector3(battleParticipantsScaling, battleParticipantsScaling, battleParticipantsScaling);
        }
        else if (newGamePlusActive)
        {
            GameObject Fighter = Instantiate(characterPrefab, randomSpawnPosition, characterPrefab.transform.rotation);
            NewGamePlusStatScaler(Fighter);
            FighterScaler(battleSize, spawnSize);
            Fighter.transform.localScale = new Vector3(battleParticipantsScaling, battleParticipantsScaling, battleParticipantsScaling);
        }
    }

    // Hero Version
    void SpawnAndScaleFighter(GameObject characterPrefab, Vector3 randomSpawnPosition, int spawnSize)
    {
        GameObject Fighter = Instantiate(characterPrefab, randomSpawnPosition, characterPrefab.transform.rotation);
        FighterScaler(battleSize, spawnSize);
        Fighter.transform.localScale = new Vector3(battleParticipantsScaling, battleParticipantsScaling, battleParticipantsScaling);
    }

    private void FighterScaler(BattleSize battleSize, int spawnSize)
    {
        // The larger the battle, the smaller each unit needs to be
        // Default sizes are based on the largest battle, then multiplied by 1.5, 2, & 3 as the number of combatants shrinks
        if (battleSize == BattleSize.skirmish)
        {
            switch (spawnSize)
            {
                case 0:
                    battleParticipantsScaling = 0.6f;
                    break;
                case 1:
                    battleParticipantsScaling = 0.84f;
                    break;
                case 2:
                    battleParticipantsScaling = 1.2f;
                    break;
                case 3:
                    battleParticipantsScaling = 1.65f;
                    break;
            }
        }
        if (battleSize == BattleSize.brawl)
        {
            switch (spawnSize)
            {
                case 0:
                    battleParticipantsScaling = 0.4f;
                    break;
                case 1:
                    battleParticipantsScaling = 0.56f;
                    break;
                case 2:
                    battleParticipantsScaling = 0.8f;
                    break;
                case 3:
                    battleParticipantsScaling = 1.1f;
                    break;
            }
        }
        if (battleSize == BattleSize.raid)
        {
            switch (spawnSize)
            {
                case 0:
                    battleParticipantsScaling = 0.3f;
                    break;
                case 1:
                    battleParticipantsScaling = 0.42f;
                    break;
                case 2:
                    battleParticipantsScaling = 0.6f;
                    break;
                case 3:
                    battleParticipantsScaling = 0.825f;
                    break;
            }
        }
        if (battleSize == BattleSize.battle)
        {
            switch (spawnSize)
            {
                case 0:
                    battleParticipantsScaling = 0.2f;
                    break;
                case 1:
                    battleParticipantsScaling = 0.28f;
                    break;
                case 2:
                    battleParticipantsScaling = 0.4f;
                    break;
                case 3:
                    battleParticipantsScaling = 0.55f;
                    break;
            }
        }
    }

    //void CharacterUnlock(int levelIndex)
    //{

    //}

    void NewGamePlusStatScaler(GameObject Fighter)
    {
        Actor ScaledStats = Fighter.GetComponentInChildren<Actor>();
        ScaledStats.maxHealth *= MagicNumbers.Instance.newGamePlusEnemyHealthMultiplier[PlayerProfile.Instance.newGamePlusIterator];
        ScaledStats.attackDamage = (int)((float)ScaledStats.attackDamage * MagicNumbers.Instance.newGamePlusEnemyAutoAtkDamageMultiplier[PlayerProfile.Instance.newGamePlusIterator]);
        ScaledStats.abilityPower = (int)((float)ScaledStats.abilityPower * MagicNumbers.Instance.newGamePlusEnemyHealthMultiplier[PlayerProfile.Instance.newGamePlusIterator]);
    }


    internal void ProcessVictory()
    {
        Debug.Log("Victory");
        if ((!PlayerProfile.Instance.UnlockedHeroes.Contains(PlayerProfile.Instance.heroes[currentLevel.heroUnlockedOnVictoryIndex]) && currentLevel.levelUnlocksCharacter))
        {
            PlayerProfile.Instance.UnlockedHeroes.Add(PlayerProfile.Instance.heroes[currentLevel.heroUnlockedOnVictoryIndex]);
            Debug.Log($"Hero {PlayerProfile.Instance.heroes[currentLevel.heroUnlockedOnVictoryIndex].name} Added");
        }
        Debug.Log("Potential Hero Unlock Processed.");
        //    currentLevel = levelList.IndexOf(currentLevelInt)
        //    CharacterUnlock();
    }
}
