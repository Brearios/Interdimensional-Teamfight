using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUpdater : MonoBehaviour
{
    //public CharacterProfile currentlyEditingProfile;
    //public ScriptableAbility currentDetailAbility;
    //public string ability2Name;
    //public string ability2DescriptionText;
    //public int ability2Cost;
    //public string ability2CostText;
    //public string ability3Name;
    //public string ability3DescriptionText;
    //public int ability3Cost;
    //public string ability3CostText;
    //public string abilityPotionName;
    //public string abilityPotionDescriptionText;
    //public int abilityPotionCost;
    //public string abilityPotionCostText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtons();
    }

    void UpdateButtons()
    {
        GameObject.Find("Ability2UnlockButton").GetComponentInChildren<Text>().text = PlayerProfile.Instance.CurrentEditingCharacter.ability2.abilityName;
        GameObject.Find("Ability3UnlockButton").GetComponentInChildren<Text>().text = PlayerProfile.Instance.CurrentEditingCharacter.ability3.abilityName;
        GameObject.Find("PotionUnlockButton").GetComponentInChildren<Text>().text = PlayerProfile.Instance.CurrentEditingCharacter.potion.abilityName;
        //ForceUpdateCanvases();
    }

    public void Button2DataUpdate()
    {
        PlayerProfile.Instance.currentDetailAbility = PlayerProfile.Instance.CurrentEditingCharacter.ability2;
        //ability2Name = currentlyEditingProfile.ability2.abilityName;
        //ability2DescriptionText = currentlyEditingProfile.ability2.description;
        //ability2Cost = currentlyEditingProfile.ability2.unlockCost;
        //ability2CostText = ability2Cost.ToString();
    }

    public void Button3DataUpdate()
    {
        PlayerProfile.Instance.currentDetailAbility = PlayerProfile.Instance.CurrentEditingCharacter.ability3;
        //ability3Name = currentlyEditingProfile.ability3.abilityName;
        //ability3DescriptionText = currentlyEditingProfile.ability3.description;
        //ability3Cost = currentlyEditingProfile.ability3.unlockCost;
        //ability3CostText = ability3Cost.ToString();
    }

    public void ButtonPotionDataUpdate()
    {
        PlayerProfile.Instance.currentDetailAbility = PlayerProfile.Instance.CurrentEditingCharacter.potion;
        //abilityPotionName = currentlyEditingProfile.potion.abilityName;
        //abilityPotionDescriptionText = currentlyEditingProfile.potion.description;
        //abilityPotionCost = currentlyEditingProfile.potion.unlockCost;
        //abilityPotionCostText = abilityPotionCost.ToString();
    }
}

