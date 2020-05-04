using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectProcessor
{
    public float remainingDuration;
    public int currentStacks;
    public ScriptableEffect effectData;

    public EffectProcessor(ScriptableEffect effect)
    {
        effectData = effect;
        remainingDuration = 0;
        
        // Stack Processing Code
    }
}
