using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDetailText : MonoBehaviour
{
    public static DisplayText Instance;
    // public CharacterProfile currentlyEditingProfile;
    // public enum buttonAbility { Two, Three, Potion }
    public ScriptableAbility currentDetailAbility;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        // currentlyEditingProfile = PlayerProfile.Instance.CurrentEditingCharacter;
        // text.text = "Click an ability's name for details on that ability.     Click + to unlock that ability.";
        currentDetailAbility = PlayerProfile.Instance.CurrentEditingCharacter.ability2;
        PlayerProfile.Instance.currentDetailAbility = currentDetailAbility;
        //buttonAbility currentAbility = buttonAbility.Two;
    }

    // Update is called once per frame
    void Update()
    {
        currentDetailAbility = PlayerProfile.Instance.currentDetailAbility;

        text.text = $"Ability: {currentDetailAbility.abilityName} \n Description: {currentDetailAbility.description} \n XP Cost {currentDetailAbility.unlockCost}";
        //text.text = "This is text test to see where the issue is.";
    }
}