using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gear Item", menuName = "GearSet")]

public class ScriptableGearSet : ScriptableObject
{
    //public int currentArmorLevel;
    //public int currentWeaponLevel;
    //public int currentAccessoryLevel;

    public ScriptableGearItem armor;
    public ScriptableGearItem weapon;
    public ScriptableGearItem accessory;

    //public int armorEnhancementSlots;
    //public int weaponEnhancementSlots;
    //public int accessoryEnhancementSlots;

    public ScriptableEnhancement[] currentArmorEnhancements;
    public ScriptableEnhancement[] currentWeaponEnhancements;
    public ScriptableEnhancement[] currentAccessoryEnhancements;
}
