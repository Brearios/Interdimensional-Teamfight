using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearUIControls : MonoBehaviour
{
    public CharacterProfile SceneCharacter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SceneCharacter = PlayerProfile.Instance.CurrentEditingCharacter;
    }

    public void ShowArmorUpgradeDetails()
    {
        PlayerProfile.Instance.currentDetailItem = SceneCharacter.gearset.armor;
    }

    public void ShowWeaponUpgradeDetails()
    {
        PlayerProfile.Instance.currentDetailItem = SceneCharacter.gearset.weapon;
    }

    public void ShowAccessoryUpgradeDetails()
    {
        PlayerProfile.Instance.currentDetailItem = SceneCharacter.gearset.accessory;
    }

    public void UpgradeArmor()
    {
        if (PlayerProfile.Instance.currentGold >= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.armorUpgradeLevel])
        {
            PlayerProfile.Instance.currentGold -= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.armorUpgradeLevel];
            SceneCharacter.armorUpgradeLevel += 1;
            SceneCharacter.armorEnhancementSlots = MagicNumbers.Instance.enhancementSlotLevels[SceneCharacter.armorUpgradeLevel];
            SceneCharacter.armorStatPoints = SceneCharacter.armorUpgradeLevel;
            SceneCharacter.armorStatMultiplier = MagicNumbers.Instance.statMultiplierLevels[SceneCharacter.armorUpgradeLevel];
        }
    }

    public void UpgradeWeapon()
    {
        if (PlayerProfile.Instance.currentGold >= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.weaponUpgradeLevel])
        {
            PlayerProfile.Instance.currentGold -= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.weaponUpgradeLevel];
            SceneCharacter.weaponUpgradeLevel += 1;
            SceneCharacter.weaponEnhancementSlots = MagicNumbers.Instance.enhancementSlotLevels[SceneCharacter.weaponUpgradeLevel];
            SceneCharacter.weaponStatPoints = SceneCharacter.weaponUpgradeLevel;
            SceneCharacter.weaponStatMultiplier = MagicNumbers.Instance.statMultiplierLevels[SceneCharacter.weaponUpgradeLevel];
        }
    }

    public void UpgradeAccessory()
    {
        if (PlayerProfile.Instance.currentGold >= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.accessoryUpgradeLevel])
        {
            PlayerProfile.Instance.currentGold -= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.accessoryUpgradeLevel];
            SceneCharacter.accessoryUpgradeLevel += 1;
            SceneCharacter.accessoryEnhancementSlots = MagicNumbers.Instance.enhancementSlotLevels[SceneCharacter.accessoryUpgradeLevel];
            SceneCharacter.accessoryStatPoints = SceneCharacter.accessoryUpgradeLevel;
            SceneCharacter.accessoryStatMultiplier = MagicNumbers.Instance.statMultiplierLevels[SceneCharacter.accessoryUpgradeLevel];
        }
    }

    public void AddArmorEnhancement(ScriptableEnhancement addedEnhancement)
    {
        
    }

    public void AddWeaponEnhancement(ScriptableEnhancement addedEnhancement)
    {

    }

    public void AddAccessoryEnhancement(ScriptableEnhancement addedEnhancement)
    {

    }
}
