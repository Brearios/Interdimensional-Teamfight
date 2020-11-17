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
    public enum GearType { Armor, Weapon, Accessory };
    public GearType gearType;
    //public int statMultiplier;
    //public int statAddition;
    //// public string itemName;
    //public int enhancementSlots;
    //public int goldNextUpgradeCost;

    //public ScriptableEnhancement[] currentEnhancement;
}
