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
    // public Sprite characterSprite;
    public int totalLevel;
    public List<AbilityUnlock> AbilityUnlockList;
}