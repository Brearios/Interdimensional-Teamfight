using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProcessor
{
    public float cooldownCount;
    public Actor currentTarget;
    // public string abilitySlot;
    public ScriptableAbility abilityData;    

    public AbilityProcessor(ScriptableAbility ability)
    {
        // abilitySlot = ability.ToString();
        abilityData = ability;
        if (ability.startsOnCooldown == false)
        {
            cooldownCount = ability.cooldown;
        }
        else
        {
            cooldownCount = 0;
        }
        currentTarget = null;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Code to Update the Cooldown
        // Code to manage Target
    }
}
