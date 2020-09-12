using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xpAvailableText : MonoBehaviour
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
        text.text = $"Current Level: {SceneCharacter.totalLevel +1}{Environment.NewLine} " +
            $"Upgrade Cost: {SceneCharacter.nextXPCost} {Environment.NewLine}" +
            $"Health: {SceneCharacter.health} {Environment.NewLine} " +
            $"Attack Damage: {SceneCharacter.attackPower} {Environment.NewLine} " +
            $"Ability Power: {SceneCharacter.abilityPower} {Environment.NewLine} " +
            $"{SceneCharacter.characterAvailableXP} XP Remaining";
    }
}