using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Ability", menuName = "Boss Ability")]

public class ScriptableBossAbility : ScriptableObject
{
    // Single = ThreatTarget, RandomAoE = random number of players, Zone = Location, Chain = Random player then jumping, PlayerZone = Zones centered on random player locations, TargetZoneCleave = hits anyone standing near target
    public enum AbilityType { Single, RandomAOE, Zone, Chain, PlayerZone, TargetZoneCleave, SpawnAdds };
    public enum TargetGroup { Player, Enemies }
    public enum EffectType { Damage, Status, DamageAndStatus, Create, Heal };
    public AbilityType abilityType;
    public TargetGroup targetGroup;
    public EffectType effectType;
    public string abilityName;
    public int minTargetsAffected;
    public int percentPlayersAffected;
    public int maxTargetsAffected;
    public int damageRadius;
    public float effectDuration;
    public int chainDistance;
    public int damage;
    public float chainDamageReduction;
    public float chainDurationReduction;
    public string statusEffectApplied;
    public ScriptableEffect effect;
    // public bool transfersToOthers; I believe the enum choices make this redundant

}