using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpController : MonoBehaviour
{
    // 20 levels of XP costs
    public int[] xpCosts = { 100, 108, 116, 126, 136, 147, 159, 171, 185, 200, 216, 234, 253, 273, 295, 319, 345, 372, 402, 435 };
    public int nextXPCost;
    public int i;
    // _Index variables to track position in array for each
    public int healthIndex;
    public int atkIndex;
    public int abilityIndex;
    public int[] atkLevel = { 5, 6, 7, 9, 10, 12, 14, 16, 17, 18, 19, 20, 22, 23, 24, 26, 27, 29, 31, 32, 34 };
    public int[] abilityLevel = { 9, 11, 13, 14, 15, 16, 17, 18, 20, 21, 22, 24, 26, 27, 29, 31, 33, 35, 37, 39 };
    public int[] healthLevel = { 100, 110, 120, 132, 143, 155, 168, 181, 195, 210, 226, 242, 259, 277, 296, 316, 337, 358, 381, 405, 431, 457, 485, 514 };
    public CharacterProfile SceneCharacter;

    // Start is called before the first frame update

    void Start()
    {
        SceneCharacter = GameObject.FindObjectOfType<CharacterProfile>();
    }

    // Update is called once per frame
    void Update()
    {
        i = (SceneCharacter.healthArrayLevel + SceneCharacter.atkArrayLevel + SceneCharacter.abilityArrayLevel);
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
            SceneCharacter.healthArrayLevel++;
            // i++;
            nextXPCost = xpCosts[i];
            // healthIndex++; - Wrong way to calculate, didn't allow persistence
            SceneCharacter.health = healthLevel[SceneCharacter.healthArrayLevel];
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
            SceneCharacter.atkArrayLevel++;
            // i++;
            nextXPCost = xpCosts[i];
            // atkIndex++;
            SceneCharacter.atk = atkLevel[SceneCharacter.atkArrayLevel];
        }
    }
    /* Pretty sure I goofed the -= nextXPCost;
    public void DecrementAtk()
    {
        // Check for sufficientXP
        // Remove XP
        // IncrementLevel
        // IncrementXPCost
        if (SceneCharacter.atkArrayLevel >= 1)
        {
            SceneCharacter.characterAvailableXP -= nextXPCost;
            SceneCharacter.atkArrayLevel--;
            i--;
            nextXPCost = xpCosts[i];
            atkIndex--;
            SceneCharacter.atk = atkLevel[atkIndex];
        }
    }
    */

    public void IncrementAbility()
    {
        // Check for sufficientXP
        // Remove XP
        // IncrementLevel
        // IncrementXPCost
        if (SceneCharacter.characterAvailableXP >= nextXPCost)
        {
            SceneCharacter.characterAvailableXP -= nextXPCost;
            SceneCharacter.abilityArrayLevel++;
            // i++;
            nextXPCost = xpCosts[i];
            // abilityIndex++;
            SceneCharacter.abilityPower = abilityLevel[SceneCharacter.abilityArrayLevel];
        }
    }

    public void ResetXP()
    {
        SceneCharacter.characterAvailableXP = 0;
    }

    public void AddXP()
    {
        SceneCharacter.characterAvailableXP += 500;
    }
}
