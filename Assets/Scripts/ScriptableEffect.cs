using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class ScriptableEffect : ScriptableObject
{
    // public enum EffectType { Simple, RawChange, StackingEffect, HotOrDot };
    // public EffectType effectType;
    public enum Effect { Taunt, Stun, Slow, Haste, HealthOverTime, Buff, DeBuff }
    public Effect effect;
    public float effectTickDuration;
    public float totalDuration;
    public float remainingDuration;
    public bool canStack;
    public int effectPerStack;
    public int currentStacks;
    public int maxStacks;
    // public float CooldownsDeltaPerSecond; // Haste or Slow Abilities
    // public float knockbackDistance;
}
