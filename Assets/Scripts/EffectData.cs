using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectData
{
    public float remainingDuration;
    public float effectTickDurationCount;
    public int currentStacks;
    public int effectHpDelta;
    public float hpDeltaPerTick;
    public int hpDeltaPerTickInt;
    public ScriptableEffect effectSettings;
    public bool isDamage;
    public string id;
    

    public EffectData(ScriptableEffect effect, int casterAbilityPower)
    {
        effectSettings = effect;
        id = System.Guid.NewGuid().ToString();
        effectTickDurationCount = 0;
        remainingDuration = 0;
        effectHpDelta = effectSettings.totalHpDelta;
        // Determining HoT/DoT effect - planning for 100% of direct damage/heal
        hpDeltaPerTick = ((effectSettings.totalHpDelta * casterAbilityPower) * (effect.effectTickDuration / effect.totalDuration));

        hpDeltaPerTickInt = (int)hpDeltaPerTick;
        
        // Stack Processing Code
    }
}
