using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class ScriptableAbility : ScriptableObject
{
    public enum TargetType { EnemyDamage, EnemyDot, EnemyDebuff, FriendlyHeal, FriendlyHot, FriendlyBuff, Self }; // Options for all enemies or all allies or 3 enemies or 3 allies?
    // EnemyDamage is highest threat with some range consideration. EnemyDebuff is highest damage. FriendlyHeal is lowest health percentage. FriendlyBuff is highest friendly damage. Self is the actor.
    public enum EffectType { Damage, Heal, Status, DamageAndStatus, HealAndStatus };
    public TargetType targetType;
    // Unneeded due to scriptable effect
    // public EffectType effectType;
    // public float effectDuration;
    // public int HpDeltaPerSecond; // DoT or HoT Abilities
    public string abilityName;
    public int hpDelta; // multiplies by ability power to determine ability strength
    public float abilityRange;
    // Ability Range must be equal to or larger than Auto Atk Range due to targeting logic.
    public float cooldown;
    // public float cooldownCount;
    public bool startsOnCooldown;
    public bool isAutoAtk;
    public string description;
    // public Actor currentTarget;
    public ScriptableEffect effect;
    public float abilityCharges; // Not sure if we'll do this
    // public bool isTauntable; Unnecessary due to use of TargetType - only EnemyDamage is tauntable
    // public int rank; Do I want ranks?
}