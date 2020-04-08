using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class ScriptableEffects : ScriptableObject
{
    public enum EffectType { Simple, RawChange, StackingEffect, HotOrDot };

    public EffectType effectType;
    public string effectName;
    public float effectMagnitude;
    public float effectTickDuration;
    public float effectTotalDuration;
    public bool canStack;
    public int numStacks;
    public int maxStacks;
    
}
