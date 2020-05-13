using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectProcessor
{
    public float remainingDuration;
    public float effectTickDurationCount;
    public int currentStacks;
    public int effectHpDelta;
    public float hpDeltaPerTick;
    public int hpDeltaPerTickInt;
    public ScriptableEffect effectData;
    public bool isDamage;

    public EffectProcessor(ScriptableEffect effect, int casterAbilityPower)
    {
        effectData = effect;
        effectTickDurationCount = 0;
        remainingDuration = 0;
        effectHpDelta = effectData.totalHpDelta;
        // Determining HoT/DoT effect - planning for 100% of direct damage/heal
        hpDeltaPerTick = ((effectData.totalHpDelta * casterAbilityPower) * (effect.effectTickDuration / effect.totalDuration));
        hpDeltaPerTickInt = (int)hpDeltaPerTick;

        // Stack Processing Code
    }
}
