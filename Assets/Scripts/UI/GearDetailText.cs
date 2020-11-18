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
        CurrentDetailItem = PlayerProfile.Instance.currentDetailItem;
        //buttonAbility currentAbility = buttonAbility.Two;
    }

    // Update is called once per frame
    void Update()
    {
        SceneCharacter = PlayerProfile.Instance.CurrentEditingCharacter;
        CurrentDetailItem = PlayerProfile.Instance.currentDetailItem;

        text.text = $"Item Name: {CurrentDetailItem.itemName}{Environment.NewLine} " +
        $"Enhancement Slots: {CurrentDetailItem.enhancementSlots}{Environment.NewLine}" +
        $"Upgrade Level: {CurrentDetailItem.upgradeLevel} {Environment.NewLine} " +
        $"Stat Added: {CurrentDetailItem.statAdded} {Environment.NewLine} " +
        $"Stat Points Added: {CurrentDetailItem.statPoints} {Environment.NewLine} " +
        $"Stat Multiplier Added: {CurrentDetailItem.statMultiplier}";
    }
}