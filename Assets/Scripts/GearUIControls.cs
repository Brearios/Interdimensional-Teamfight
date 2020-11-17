using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearUIControls : MonoBehaviour
{

    public ScriptableGearItem gearUpgradeItem;
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
        MenuVariables.Instance.gearType = MenuVariables.GearTypeToDisplay.Armor;
    }

    public void ShowWeaponUpgradeDetails()
    {
        MenuVariables.Instance.gearType = MenuVariables.GearTypeToDisplay.Weapon;
    }

    public void ShowAccessoryUpgradeDetails()
    {
        MenuVariables.Instance.gearType = MenuVariables.GearTypeToDisplay.Accessory;
    }

    public void UpgradeArmor()
    {
        if (PlayerProfile.Instance.currentGold >= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.currentArmorLevel])
        {
            PlayerProfile.Instance.currentGold -= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.currentArmorLevel];
        }
    }

    public void UpgradeWeapon()
    {
        if (PlayerProfile.Instance.currentGold >= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.currentWeaponLevel])
        {
            PlayerProfile.Instance.currentGold -= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.currentWeaponLevel];
        }
    }

    public void UpgradeAccessory()
    {
        if (PlayerProfile.Instance.currentGold >= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.currentAccessoryLevel])
        {
            PlayerProfile.Instance.currentGold -= MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.currentAccessoryLevel];
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
