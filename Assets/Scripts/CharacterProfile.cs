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
    // _ArrayLevel variables are to count XP cost increments
    public int healthArrayLevel;
    public int atkArrayLevel;
    public int abilityArrayLevel;
    public int nextXPCost;
    public int health;
    public int atk;
    public int abilityPower;
    public bool autoAtkUnlock;
    public bool ability1Unlock;
    public bool ability2Unlock;
    public bool ability3Unlock;
    public bool potionUnlock;
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