using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class ScriptableAbility : ScriptableObject
{
    public enum TargetType { Enemy, Friendly, Self }; // Options for all enemies or all allies or 3 enemies or 3 allies?
    public enum EffectType { Damage, Heal, Status, DamageAndStatus, HealAndStatus };
    public TargetType targetType;
    public EffectType effectType;
    public string abilityName;
    public int rank;
    public int hpDelta; // multiplies by ability power to determine ability strength
    public float abilityRange;
    public float abilityCooldown;
    public string description;
    public float effectDuration;
    public int HpDeltaPerSecond; // DoT or HoT Abilities
    public float CooldownsDeltaPerSecond; // Haste or Slow Abilities
    public float knockbackDistance;
    public float abilityStartingCooldownCredit; // Can it be used immediately, after a set delay, or a short cooldown?
}