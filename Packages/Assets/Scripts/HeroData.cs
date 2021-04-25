using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HeroData
{
    public GameObject characterPrefab;
    public string name;
    public enum Team { Blue, Red, Neutral };
    public Team team = HeroData.Team.Blue;
    public enum SpawnRegion { Anywhere, AlliedMelee, AlliedRanged, AlliedSide, EnemyMelee, EnemyRanged, EnemySide };
    public SpawnRegion spawn;
}