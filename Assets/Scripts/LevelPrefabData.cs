﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class LevelPrefabData
{
    public GameObject characterPrefab;
    public ScriptableTeam team;
    public int numberOfInstances;
    public enum SpawnRegion { Anywhere, AlliedMelee, AlliedRanged, EnemyMelee, EnemyRanged };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
