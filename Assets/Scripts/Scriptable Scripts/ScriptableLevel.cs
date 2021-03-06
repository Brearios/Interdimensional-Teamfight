﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class ScriptableLevel : ScriptableObject
{
    public int levelNumber;

    // Unused, but might want limits
    public int maxFriendlyCharacters;

    // Currently unused, but will be necessary with more levels than heroes
    public bool levelUnlocksCharacter;

    public int heroUnlockedOnVictoryIndex;
    // This part would go in LevelManager
    // public HeroData heroUnlockedWithVictory = PlayerProfile.Instance.heroes[heroUnlockIndex];
    public string GameObject;
    public List<LevelPrefabData> nPCSpawnData;
    // Going with random positions in zones
    //public List<Transform> heroSpawnPositions;
    //public List<Transform> enemySpawnPositions;
    public enum Setting { Fantasy, SteamWest, PostAnthropolypse, SciFi, Hell, Void };
    // Code for background/Map

    // public characterProfile characterUnlockedAfterVictory - Not sure what this needs to be  
}
