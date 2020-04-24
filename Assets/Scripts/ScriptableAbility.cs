using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class ScriptableAbility : ScriptableObject
{
    public enum TargetType { EnemyDamage, EnemyDebuff, FriendlyHeal, FriendlyBuff, Self }; // Options for all enemies or all allies or 3 enemies or 3 allies?
    public enum EffectType { Damage, Heal, Status, DamageAndStatus, HealAndStatus };
    public TargetType targetType;
    // Unneeded due to scriptable effect
    // public EffectType effectType;
    // public float effectDuration;
    // public int HpDeltaPerSecond; // DoT or HoT Abilities
    public string abilityName;
    public int hpDelta; // multiplies by ability power to determine ability strength
    public float abilityRange;
    public float abilityCooldown;
    public float abilityCooldownCount;
    public string description;
    public float abilityStartingCooldownCredit; // Can it be used immediately, after a set delay, or a short cooldown?
    public Actor currentTarget;
    public ScriptableEffect effect;
    public float abilityCharges; // Not sure if we'll do this
    public bool isTauntable;
    // public int rank; Do I want ranks?
}