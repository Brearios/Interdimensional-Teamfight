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
    public List<LevelPrefabData> unitSpawnData;
    // Going with random positions in zones
    //public List<Transform> heroSpawnPositions;
    //public List<Transform> enemySpawnPositions;
    public enum Setting { Fantasy, SteamWest, PostAnthropolypse, SciFi, Hell, Void };
    // Code for background/Map

    // public characterProfile characterUnlockedAfterVictory - Not sure what this needs to be  
}
