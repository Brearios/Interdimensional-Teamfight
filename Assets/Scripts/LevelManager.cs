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

        // Code to select level in list that matches CurrentLevelInt and set to currentLevel;
        foreach (LevelPrefabData unitSpawnData in currentLevel.nPCSpawnData)
        {
            for (int i = 0; i < unitSpawnData.numberOfInstances; i++)
            {
                SpawnFighter(unitSpawnData.characterPrefab, GenerateRandomPosition(unitSpawnData.spawn));
            }                       
        }
        foreach (HeroData unlockedHeroSpawnData in PlayerProfile.Instance.UnlockedHeroes)
        {
            SpawnFighter(unlockedHeroSpawnData.characterPrefab, GenerateRandomPosition(unlockedHeroSpawnData.spawn));
            Debug.Log($"Spawned hero { unlockedHeroSpawnData.characterPrefab.name}");
        }
        startingCharactersSpawned = true;
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

    void SpawnFighter(GameObject characterPrefab, Vector3 randomSpawnPosition)
    {
        Instantiate(characterPrefab, randomSpawnPosition, characterPrefab.transform.rotation);
    }

    //void CharacterUnlock(int levelIndex)
    //{
        
    //}

    internal void ProcessVictory()
    {
        Debug.Log("Victory");
        if (!PlayerProfile.Instance.UnlockedHeroes.Contains(PlayerProfile.Instance.heroes[currentLevel.heroUnlockIndex]))
        {
            PlayerProfile.Instance.UnlockedHeroes.Add(PlayerProfile.Instance.heroes[currentLevel.heroUnlockIndex]);
            Debug.Log($"Hero {PlayerProfile.Instance.heroes[currentLevel.heroUnlockIndex].name} Added");
        }
        Debug.Log("Potential Hero Unlock Processed.");
        //    currentLevel = levelList.IndexOf(currentLevelInt)
        //    CharacterUnlock();
    }
}
