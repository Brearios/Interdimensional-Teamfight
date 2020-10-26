using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterProfile 
{
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

    //public List<AbilityUnlock> AbilityUnlocks = new List<AbilityUnlock>(){ autoAtk, ability1, ability2, ability3, potion };
    public List<AbilityUnlock> AbilityUnlocks;
    // Each character should have autoAtk and their first ability unlocked by default, but will need to unlock abilities 2 & 3 and their potion.
}