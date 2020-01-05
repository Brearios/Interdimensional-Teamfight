using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpController : MonoBehaviour
{
    // 20 levels of XP costs
    public int[] xpCosts = { 100, 108, 116, 126, 136, 147, 159, 171, 185, 200, 216, 234, 253, 273, 295, 319, 345, 372, 402, 435 };
    public int nextXPCost;
    public int i;
    public CharacterProfile SceneCharacter;

    // Start is called before the first frame update

    void Start()
    {
        SceneCharacter = GameObject.FindObjectOfType<CharacterProfile>();
    }

    // Update is called once per frame
    void Update()
    {
        i = (SceneCharacter.healthLevel + SceneCharacter.AtkLevel + SceneCharacter.AbilityLevel);
        nextXPCost = xpCosts[i];
    }
    public void IncrementHealth()
    {
        // Check for sufficientXP
        // Remove XP
        // IncrementLevel
        // IncrementXPCost
        if (SceneCharacter.characterAvailableXP >= nextXPCost)
        {
            SceneCharacter.characterAvailableXP -= nextXPCost;
            SceneCharacter.healthLevel++;
            i++;
            nextXPCost = xpCosts[i];
        }

    }
    public void IncrementAtk()
    {
        // Check for sufficientXP
        // Remove XP
        // IncrementLevel
        // IncrementXPCost
        if (SceneCharacter.characterAvailableXP >= nextXPCost)
        {
            SceneCharacter.characterAvailableXP -= nextXPCost;
            SceneCharacter.AtkLevel++;
            i++;
            nextXPCost = xpCosts[i];
        }
    }

    public void IncrementAbility()
    {
        // Check for sufficientXP
        // Remove XP
        // IncrementLevel
        // IncrementXPCost
        if (SceneCharacter.characterAvailableXP >= nextXPCost)
        {
            SceneCharacter.characterAvailableXP -= nextXPCost;
            SceneCharacter.AbilityLevel++;
            i++;
            nextXPCost = xpCosts[i];
        }
    }
}
