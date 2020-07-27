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
    public int heroUnlockIndex;
    // This part would go in LevelManager
    // public HeroData heroUnlockedWithVictory = PlayerProfile.Instance.heroes[heroUnlockIndex];
    public string GameObject;
    public List<LevelPrefabData> nPCSpawnData;
    public List<HeroData> heroSpawnData = PlayerProfile.Instance.UnlockedHeroes;
    // Going with random positions in zones
    //public List<Transform> heroSpawnPositions;
    //public List<Transform> enemySpawnPositions;
    public enum Setting { Fantasy, SteamWest, PostAnthropolypse, SciFi, Hell, Void };
    // Code for background/Map

    // public characterProfile characterUnlockedAfterVictory - Not sure what this needs to be  
}
