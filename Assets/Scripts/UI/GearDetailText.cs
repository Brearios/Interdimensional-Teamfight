using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearDetailText : MonoBehaviour
{
    public static DisplayText Instance;
    public CharacterProfile SceneCharacter;
    public ScriptableGearItem CurrentDetailItem;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        // currentlyEditingProfile = PlayerProfile.Instance.CurrentEditingCharacter;
        // text.text = "Click an ability's name for details on that ability.     Click + to unlock that ability.";
        CurrentDetailItem = PlayerProfile.Instance.CurrentEditingCharacter.gearset.armor;
        //buttonAbility currentAbility = buttonAbility.Two;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentDetailItem = PlayerProfile.Instance.currentDetailItem;
        SceneCharacter = PlayerProfile.Instance.CurrentEditingCharacter;

        if (CurrentDetailItem.itemType == ScriptableGearItem.ItemType.armor)
        {
            text.text = $"Item Name: {CurrentDetailItem.itemName}{Environment.NewLine} " +
            $"Upgrade Level: {SceneCharacter.armorUpgradeLevel} {Environment.NewLine} " +
            $"Stat Added: {CurrentDetailItem.statAdded} {Environment.NewLine} " +
            $"Stat Points Added: {SceneCharacter.armorUpgradeLevel} {Environment.NewLine} " +
            $"Stat Multiplier Added: {MagicNumbers.Instance.statMultiplierLevels[SceneCharacter.armorUpgradeLevel]} {Environment.NewLine} " +
            $"Enhancement Slots: {MagicNumbers.Instance.enhancementSlotLevels[SceneCharacter.armorUpgradeLevel]}";
        }
        else if (CurrentDetailItem.itemType == ScriptableGearItem.ItemType.weapon)
        {
            text.text = $"Item Name: {CurrentDetailItem.itemName}{Environment.NewLine} " +
            $"Upgrade Level: {SceneCharacter.weaponUpgradeLevel} {Environment.NewLine} " +
            $"Stat Added: {CurrentDetailItem.statAdded} {Environment.NewLine} " +
            $"Stat Points Added: {SceneCharacter.weaponUpgradeLevel} {Environment.NewLine} " +
            $"Stat Multiplier Added: {MagicNumbers.Instance.statMultiplierLevels[SceneCharacter.weaponUpgradeLevel]} {Environment.NewLine} " +
            $"Enhancement Slots: {MagicNumbers.Instance.enhancementSlotLevels[SceneCharacter.weaponUpgradeLevel]}";
        }
        else if (CurrentDetailItem.itemType == ScriptableGearItem.ItemType.accessory)
        {
            text.text = $"Item Name: {CurrentDetailItem.itemName}{Environment.NewLine} " +
            $"Upgrade Level: {SceneCharacter.accessoryUpgradeLevel} {Environment.NewLine} " +
            $"Stat Added: {CurrentDetailItem.statAdded} {Environment.NewLine} " +
            $"Stat Points Added: {SceneCharacter.accessoryUpgradeLevel} {Environment.NewLine} " +
            $"Stat Multiplier Added: {MagicNumbers.Instance.statMultiplierLevels[SceneCharacter.accessoryUpgradeLevel]} {Environment.NewLine} " +
            $"Enhancement Slots: {MagicNumbers.Instance.enhancementSlotLevels[SceneCharacter.accessoryUpgradeLevel]}";
        }
    }
}