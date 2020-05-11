using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class ScriptableEffect : ScriptableObject
{
    // public enum EffectType { Simple, RawChange, StackingEffect, HotOrDot };
    // public EffectType effectType;
    public enum Effect { Taunt, Stun, Slow, Haste, HealthOverTime, Buff, Debuff }
    public Effect effect;
    public float effectTickDuration;
    public float totalDuration;
    public bool canStack;
    public int totalHpDelta;
    public int effectPerStack;
    public int maxStacks;
    public string effectName;

    // Moved to EffectProcessor to update during play
    // public float remainingDuration;
    // public int currentStacks;

    public enum Bool { isTaunted,  }
    // public float CooldownsDeltaPerSecond; // Haste or Slow Abilities
    // public float knockbackDistance;
}
