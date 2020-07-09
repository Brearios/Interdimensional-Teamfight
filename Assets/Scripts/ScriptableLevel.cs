using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class ScriptableLevel : ScriptableObject
{
    // public enum EffectType { Simple, RawChange, StackingEffect, HotOrDot };
    // public EffectType effectType;
    public int levelNumber;
    public int maxFriendlyCharacters;
    public enum Setting { Fantasy, SteamWest, PostAnthropolypse, SciFi, Hell, Void }

    
}
