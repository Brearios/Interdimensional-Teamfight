using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUIUpdater : MonoBehaviour
{
    CharacterProfile currentlyEditingProfile;
    public string ability2Name;
    public string ability2DescriptionText;
    public int ability2Cost;
    public string ability2CostText;
    public string ability3Name;
    public string ability3DescriptionText;
    public int ability3Cost;
    public string ability3CostText;
    public string abilityPotionName;
    public string abilityPotionDescriptionText;
    public int abilityPotionCost;
    public string abilityPotionCostText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateButtons()
    {
        Button2DataUpdate();
        Button3DataUpdate();
        ButtonPotionDataUpdate();
        // ForceUpdateCanvases(); May not be needed
    }

    void Button2DataUpdate()
    {
        currentlyEditingProfile = PlayerProfile.Instance.characterProfiles[PlayerProfile.Instance.currentEditingInteger];

        ability2Name = currentlyEditingProfile.ability2.abilityName;
        ability2DescriptionText = currentlyEditingProfile.ability2.description;
        ability2Cost = currentlyEditingProfile.ability2.unlockCost;
        ability2CostText = ability2Cost.ToString();
    }

    void Button3DataUpdate()
    {
        currentlyEditingProfile = PlayerProfile.Instance.characterProfiles[PlayerProfile.Instance.currentEditingInteger];

        ability3Name = currentlyEditingProfile.ability3.abilityName;
        ability3DescriptionText = currentlyEditingProfile.ability3.description;
        ability3Cost = currentlyEditingProfile.ability3.unlockCost;
        ability3CostText = ability3Cost.ToString();
    }

    void ButtonPotionDataUpdate()
    {
        currentlyEditingProfile = PlayerProfile.Instance.characterProfiles[PlayerProfile.Instance.currentEditingInteger];

        abilityPotionName = currentlyEditingProfile.potion.abilityName;
        abilityPotionDescriptionText = currentlyEditingProfile.potion.description;
        abilityPotionCost = currentlyEditingProfile.potion.unlockCost;
        abilityPotionCostText = abilityPotionCost.ToString();
    }
}

