﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterProfile 
{
    public int armorUpgradeLevel;
    public int armorEnhancementSlots;
    public List<ScriptableEnhancement> armorEnhancements;
    public int armorStatPoints;
    public float armorStatMultiplier;

    public int weaponUpgradeLevel;
    public int weaponEnhancementSlots;
    public List<ScriptableEnhancement> weaponEnhancements;
    public int weaponStatPoints;
    public float weaponStatMultiplier;

    public int accessoryUpgradeLevel;
    public int accessoryEnhancementSlots;
    public List<ScriptableEnhancement> accessoryEnhancements;
    public int accessoryStatPoints;
    public float accessoryStatMultiplier;

    // public static CharacterProfile Instance;
    public string heroName;
    public int characterTotalXP;
    public int characterAvailableXP;
    public int nextXpCost;
    // _ArrayLevel variables are to count XP cost increments
    public int healthListLevel;
    public int atkListLevel;
    public int abilityListLevel;
    public bool isTank;
    
    public int health;
    public int attackPower;
    public int abilityPower;
    public bool charUnlock;
    public bool autoAtkUnlock;
    public bool ability1Unlock;
    public bool ability2Unlock;
    public bool ability3Unlock;
    public bool potionUnlock;
    public enum PotionType { Regen, AtkPower, AbilityPower };
    public PotionType selectedPotionType;
    // public Sprite characterSprite;
    public int totalLevel;
    public ScriptableAbility autoAtk;
    public ScriptableAbility ability1;
    public ScriptableAbility ability2;
    public ScriptableAbility ability3;
    public ScriptableAbility potion;


    public ScriptableGearSet gearset;

    public float armorMultiplier;
    public float weaponMultiplier;
    public float accessoryMultiplier;

    public int nextArmorUpgradeIndex;
    public int nextWeaponUpgradeIndex;
    public int nextAccessoryUpgradeIndex;


    //public List<AbilityUnlock> AbilityUnlocks = new List<AbilityUnlock>(){ autoAtk, ability1, ability2, ability3, potion };
    public List<AbilityUnlock> AbilityUnlocks;
    // Each character should have autoAtk and their first ability unlocked by default, but will need to unlock abilities 2 & 3 and their potion.
}