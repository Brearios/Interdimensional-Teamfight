using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class LevelPrefabData
{
    public GameObject characterPrefab;
    public enum Team { Blue, Red, Neutral };
    public enum RelativeSize { Small, Normal, Large, Boss };
    public bool DefaultUnitData;
    public Team team;
    public RelativeSize size;

    //// These numbers need to be built into XpController - for now, let's assume level 1
    //public int healthLevel;
    //public int abilityLevel;
    //public int autoAtkLevel;
    
    //public float healthScalingFactor;

    public int numberOfInstances;
    public enum SpawnRegion { Anywhere, AlliedMelee, AlliedRanged, AlliedSide, EnemyMelee, EnemyRanged, EnemySide };
    public SpawnRegion spawn;
    
}
