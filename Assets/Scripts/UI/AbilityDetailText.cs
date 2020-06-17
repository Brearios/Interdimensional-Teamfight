using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDetailText : MonoBehaviour
{
    public static DisplayText Instance;
    public CharacterProfile currentlyEditingProfile;
    // public enum buttonAbility { Two, Three, Potion }
    public ScriptableAbility currentDetailAbility;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        currentlyEditingProfile = PlayerProfile.Instance.CurrentEditingCharacter;
        text.text = "Click an ability's name for details on that ability.     Click + to unlock that ability.";
        currentDetailAbility = currentlyEditingProfile.ability2;
        //buttonAbility currentAbility = buttonAbility.Two;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Ability: {currentDetailAbility.description} XP Cost: {currentDetailAbility.unlockCost}";
    }

    public void ShowAbility2Details()
    {
        currentDetailAbility = currentlyEditingProfile.ability2;
        // buttonAbility currentAbility = buttonAbility.Two;
    }

    public void ShowAbility3Details()
    {
        currentDetailAbility = currentlyEditingProfile.ability3;
        // buttonAbility currentAbility = buttonAbility.Three;
    }

    public void ShowPotionDetails()
    {
        currentDetailAbility = currentlyEditingProfile.potion;
        // buttonAbility currentAbility = buttonAbility.Potion;
    }
}