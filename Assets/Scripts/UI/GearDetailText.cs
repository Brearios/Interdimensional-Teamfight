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
        CurrentDetailItem = MenuVariables.Instance.SceneCharacter.gearset.armor;
        //buttonAbility currentAbility = buttonAbility.Two;
    }

    // Update is called once per frame
    void Update()
    {
        SceneCharacter = PlayerProfile.Instance.CurrentEditingCharacter;
        currentDetailAbility = PlayerProfile.Instance.currentDetailAbility;

        text.text = $"Ability: {currentDetailAbility.abilityName} \n Description: {currentDetailAbility.description} \n XP Cost: {currentDetailAbility.unlockCost}";
        //text.text = "This is text test to see where the issue is.";
    }
}