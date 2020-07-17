using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XpController : MonoBehaviour
{
    // 20 levels of XP costs
    public int[] xpCosts = { 100, 108, 116, 126, 136, 147, 159, 171, 185, 200, 216, 234, 253, 273, 295, 319, 345, 372, 402, 435 };
    public int nextXPCost;
    public int indexOfXpCosts;
    // _Index variables to track position in array for each
    public int healthArrayLevel;
    public int atkArrayLevel;
    public int abilityArrayLevel;
    public int[] atkLevel = { 5, 6, 7, 9, 10, 12, 14, 16, 17, 18, 19, 20, 22, 23, 24, 26, 27, 29, 31, 32, 34 };
    public int[] abilityLevel = { 9, 11, 13, 14, 15, 16, 17, 18, 20, 21, 22, 24, 26, 27, 29, 31, 33, 35, 37, 39 };
    public int[] healthLevel = { 100, 110, 120, 132, 143, 155, 168, 181, 195, 210, 226, 242, 259, 277, 296, 316, 337, 358, 381, 405, 431, 457, 485, 514 };
    public int[] tankHealthLevel = { 300, 330, 362, 395, 429, 466, 504, 544, 586, 631, 677, 726, 778, 831, 888, 947, 1010, 1075, 1144, 1216, 1292, 1372, 1455, 1543 };
    //public int ability2UnlockCost = 300;
    //public int ability3UnlockCost = 500;
    //public int potionUnlockCost = 800;
    // Eventually will need an index of character profiles:
    // public CharacterProfile[] characterProfiles = { PlayerProfile.Instance.mageHero, PlayerProfile.Instance.priestHero, PlayerProfile.Instance.warriorHero }
    public CharacterProfile SceneCharacter;
    // public CharacterProfile SceneCharacter;

    // Start is called before the first frame update

    void Start()
    {
        SceneCharacter = PlayerProfile.Instance.CurrentEditingCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneCharacter != null)
        indexOfXpCosts = (SceneCharacter.healthArrayLevel + SceneCharacter.atkArrayLevel + SceneCharacter.abilityArrayLevel);
        nextXPCost = xpCosts[indexOfXpCosts];
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
            nextXPCost = xpCosts[indexOfXpCosts];
            SceneCharacter.health = healthLevel[SceneCharacter.healthArrayLevel];
            IncrementLevel();
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
            nextXPCost = xpCosts[indexOfXpCosts];
            // atkIndex++;
            SceneCharacter.atk = atkLevel[SceneCharacter.atkArrayLevel];
            IncrementLevel();
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
            SceneCharacter.abilityArrayLevel++;
            // i++;
            nextXPCost = xpCosts[indexOfXpCosts];
            // abilityIndex++;
            SceneCharacter.abilityPower = abilityLevel[SceneCharacter.abilityArrayLevel];
            IncrementLevel();
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

    public void IncrementLevel()
    {
        SceneCharacter.totalLevel++;
    }

    public void SecondAbilityUnlockButton()
    {
        if ((SceneCharacter.characterAvailableXP > SceneCharacter.ability2.unlockCost) && !SceneCharacter.ability2Unlock)
        {
            SceneCharacter.characterAvailableXP -= SceneCharacter.ability2.unlockCost;
            SceneCharacter.ability2Unlock = true;
        }
    }

    public void ThirdAbilityUnlockButton()
    {
        if ((SceneCharacter.characterAvailableXP > SceneCharacter.ability3.unlockCost) && !SceneCharacter.ability3Unlock)
        {
            SceneCharacter.characterAvailableXP -= SceneCharacter.ability3.unlockCost;
            SceneCharacter.ability3Unlock = true;
        }
    }

    public void PotionUnlockButton()
    {
        if ((SceneCharacter.characterAvailableXP > SceneCharacter.potion.unlockCost) && !SceneCharacter.potionUnlock)
        {
            SceneCharacter.characterAvailableXP -= SceneCharacter.potion.unlockCost;
            SceneCharacter.potionUnlock = true;
        }
    }
}
