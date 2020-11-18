using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goldAvailableText : MonoBehaviour
{
    public static DisplayText Instance;
    public CharacterProfile SceneCharacter;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        SceneCharacter = PlayerProfile.Instance.CurrentEditingCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Current Gold: {PlayerProfile.Instance.currentGold}{Environment.NewLine} " +
        $"Upgrade Cost by Item: {Environment.NewLine}" +
        $"Armor: {MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.armor.upgradeLevel]} {Environment.NewLine} " +
        $"Weapon: {MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.weapon.upgradeLevel]} {Environment.NewLine} " +
        $"Accessory: {MagicNumbers.Instance.goldUpgradeCosts[SceneCharacter.gearset.accessory.upgradeLevel]}";
    }
}