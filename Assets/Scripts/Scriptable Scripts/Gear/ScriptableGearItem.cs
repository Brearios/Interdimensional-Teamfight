using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gear Item", menuName = "GearItem")]

public class ScriptableGearItem : ScriptableObject
{
    // Moved dynamic stats to CharacterProfile
    // Moved arrays to MagicNumbers and other details to GearSet
    public enum ItemType { armor, weapon, accessory };
    public ItemType itemType;
    public string itemName;
    // public int upgradeLevel;
    // public int enhancementSlots;
    public string statAdded;
    // public int statPoints;
    // public float statMultiplier;
    // public int goldNextUpgradeCost;

    // public ScriptableEnhancement[] currentEnhancements;
}
