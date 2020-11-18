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
        if (PlayerProfile.Instance.currentGold >= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.armor.upgradeLevel])
        {
            PlayerProfile.Instance.currentGold -= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.armor.upgradeLevel];
            SceneCharacter.gearset.armor.upgradeLevel += 1;
            SceneCharacter.gearset.armor.enhancementSlots = MagicNumbers.Instance.enhancementSlotLevels[SceneCharacter.gearset.weapon.upgradeLevel];
            SceneCharacter.gearset.armor.statPoints = SceneCharacter.gearset.weapon.upgradeLevel;
            SceneCharacter.gearset.armor.statMultiplier = MagicNumbers.Instance.statMultiplierLevels[SceneCharacter.gearset.weapon.upgradeLevel];
        }
    }

    public void UpgradeWeapon()
    {
        if (PlayerProfile.Instance.currentGold >= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.weapon.upgradeLevel])
        {
            PlayerProfile.Instance.currentGold -= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.weapon.upgradeLevel];
            SceneCharacter.gearset.weapon.upgradeLevel += 1;
            SceneCharacter.gearset.weapon.enhancementSlots = MagicNumbers.Instance.enhancementSlotLevels[SceneCharacter.gearset.weapon.upgradeLevel];
            SceneCharacter.gearset.weapon.statPoints = SceneCharacter.gearset.weapon.upgradeLevel;
            SceneCharacter.gearset.weapon.statMultiplier = MagicNumbers.Instance.statMultiplierLevels[SceneCharacter.gearset.weapon.upgradeLevel];
        }
    }

    public void UpgradeAccessory()
    {
        if (PlayerProfile.Instance.currentGold >= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.accessory.upgradeLevel])
        {
            PlayerProfile.Instance.currentGold -= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.accessory.upgradeLevel];
            SceneCharacter.gearset.accessory.upgradeLevel += 1;
            SceneCharacter.gearset.accessory.enhancementSlots = MagicNumbers.Instance.enhancementSlotLevels[SceneCharacter.gearset.weapon.upgradeLevel];
            SceneCharacter.gearset.accessory.statPoints = SceneCharacter.gearset.weapon.upgradeLevel;
            SceneCharacter.gearset.accessory.statMultiplier = MagicNumbers.Instance.statMultiplierLevels[SceneCharacter.gearset.weapon.upgradeLevel];
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
