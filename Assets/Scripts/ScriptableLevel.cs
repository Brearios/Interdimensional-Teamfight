using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class ScriptableLevel : ScriptableObject
{
    public int levelNumber;
    public int maxFriendlyCharacters;
    public bool levelUnlocksCharacter;
    public string charUnlockWithVictory;
    public List<LevelPrefabData> unitSpawnSettings;
    public List<Transform> heroSpawnPositions;
    public List<Transform> enemySpawnPositions;
    //public GameObject EnemyType1;
    //public int EnemyType1Number;
    //public GameObject EnemyType2;
    //public int EnemyType2Number;
    //public GameObject EnemyType3;
    //public int EnemyType3Number;
    //public GameObject EnemyType4;
    //public int EnemyType4Number;
    public enum Setting { Fantasy, SteamWest, PostAnthropolypse, SciFi, Hell, Void };
    // Code for background/Map

    // public characterProfile characterUnlockedAfterVictory - Not sure what this needs to be  
}
