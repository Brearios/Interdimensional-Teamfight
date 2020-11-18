using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gear Item", menuName = "GearItem")]

public class ScriptableGearItem : ScriptableObject
{
    // I likely don't need this script at all, but keeping it in case we go with individual items.
    // Moved arrays to MagicNumbers and other details to GearSet

    public string itemName;
    public int upgradeLevel;
    public int enhancementSlots;
    public string statAdded;
    public int statPoints;
    public float statMultiplier;
    //public int goldNextUpgradeCost;

    public ScriptableEnhancement[] currentEnhancements;
}
