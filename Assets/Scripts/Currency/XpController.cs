using System;
using System.Collections.Generic;
using UnityEngine;

// Add Atk, Ability, and Health calculators

public class XpController : MonoBehaviour
{
    // 20 levels of XP costs
    // public int[] xpCosts = { 100, 108, 116, 126, 136, 147, 159, 171, 185, 200, 216, 234, 253, 273, 295, 319, 345, 372, 402, 435, 470, 508, 549, 593, 641, 692, 748, 808, 873, 944, 1020, 1102, 1191, 1287, 1390, 1502, 1623, 1754, 1895, 2048, 2213, 2391, 2583, 2791, 3016, 3259, 3521, 3805, 4111, 4442, 4799, 5186, 5603, 6054, 6542, 7069, 7638, 8253, 8917, 9635, 10410 };
    // public int startingXpCost; Moved to MagicNumbers.cs

    // Can just use SceneCharacter.nextXpCost;
    // public int nextXPCost;
    public int indexOfXpCosts;
    // _Index variables to track position in array for each
    //public int healthArrayLevel;
    //public int atkArrayLevel;
    //public int abilityArrayLevel;

    //public int[] atkLevel = { 5, 6, 7, 9, 10, 12, 14, 16, 17, 18, 19, 20, 22, 23, 24, 26, 27, 29, 31, 32, 34 };
    //public int[] abilityLevel = { 8, 9, 11, 13, 14, 15, 16, 17, 18, 20, 21, 22, 24, 26, 27, 29, 31, 33, 35, 37, 39 };
    //public int[] healthLevel = { 100, 110, 120, 132, 143, 155, 168, 181, 195, 210, 226, 242, 259, 277, 296, 316, 337, 358, 381, 405, 431, 457, 485, 514 };

    //public List<int> atkLevel = new List<int>();
    //public List<int> abilityLevel = new List<int>();
    //public List<int> healthLevel = new List<int>();

    public double startingAtkIncrease;
    public double startingAbilityIncrease;
    public double startingHealthIncrease;

    //public int ability2UnlockCost = 300;
    //public int ability3UnlockCost = 500;
    //public int potionUnlockCost = 800;
    public CharacterProfile SceneCharacter;

    void Start()
    {
        SceneCharacter = PlayerProfile.Instance.CurrentEditingCharacter;
        startingAtkIncrease = (MagicNumbers.Instance.startingAtkPower * .1);
        startingAbilityIncrease = (MagicNumbers.Instance.startingAbilityPower * .1);
        startingHealthIncrease = (MagicNumbers.Instance.startingHealth * .1);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneCharacter != null)
            indexOfXpCosts = (SceneCharacter.healthListLevel + SceneCharacter.atkListLevel + SceneCharacter.abilityListLevel);
        SceneCharacter.nextXpCost = (int)StatCalculator.Instance.xpCosts[indexOfXpCosts];
        if (indexOfXpCosts >= StatCalculator.Instance.xpCosts.Count - 1)
        {
            GenerateMoreXpCosts(); // indexOfXpCosts, SceneCharacter.nextXpCost);
        }
    }
    public void IncrementHealth()
    {
        // Check for sufficientXP
        // Remove XP
        // IncrementLevel
        // IncrementXPCost
        if (SceneCharacter.characterAvailableXP >= SceneCharacter.nextXpCost)
        {
            SceneCharacter.characterAvailableXP -= SceneCharacter.nextXpCost;

            if (SceneCharacter.healthListLevel >= StatCalculator.Instance.healthLevels.Count - 1)
            {
                GenerateMoreHealthStats(StatCalculator.Instance.healthLevels[StatCalculator.Instance.healthLevels.Count - 1]);
            }

            SceneCharacter.healthListLevel++;

            SceneCharacter.health = (int)StatCalculator.Instance.healthLevels[SceneCharacter.healthListLevel];
            IncrementLevel();
        }
    }
    public void IncrementAtk()
    {
        // Check for sufficientXP
        // Remove XP
        // IncrementLevel
        // IncrementXPCost
        if (SceneCharacter.characterAvailableXP >= SceneCharacter.nextXpCost)
        {
            SceneCharacter.characterAvailableXP -= SceneCharacter.nextXpCost;

            // i++;

            if (SceneCharacter.atkListLevel >= StatCalculator.Instance.atkPowerLevels.Count - 1)
            {
                GenerateMoreAtkStats(StatCalculator.Instance.atkPowerLevels[StatCalculator.Instance.atkPowerLevels.Count - 1]);
            }

            SceneCharacter.atkListLevel++;

            SceneCharacter.attackPower = (int)StatCalculator.Instance.atkPowerLevels[SceneCharacter.atkListLevel];
            IncrementLevel();
        }
    }

    public void IncrementAbility()
    {
        // Check for sufficientXP
        // Remove XP
        // IncrementLevel
        // IncrementXPCost
        if (SceneCharacter.characterAvailableXP >= SceneCharacter.nextXpCost)
        {
            SceneCharacter.characterAvailableXP -= SceneCharacter.nextXpCost;

            if (SceneCharacter.abilityListLevel >= StatCalculator.Instance.abilityPowerLevels.Count - 1)
            {
                GenerateMoreAbilityStats(StatCalculator.Instance.abilityPowerLevels[StatCalculator.Instance.abilityPowerLevels.Count - 1]);
            }
            SceneCharacter.abilityListLevel++;
            // i++;

            // abilityIndex++;
            SceneCharacter.abilityPower = (int)StatCalculator.Instance.abilityPowerLevels[SceneCharacter.abilityListLevel];
            IncrementLevel();
        }
    }

    public void ResetXP()
    {
        SceneCharacter.characterAvailableXP = 0;
    }

    public void ResetGold()
    {
        PlayerProfile.Instance.currentGold = 0;
    }

    public void AddXP()
    {
        SceneCharacter.characterAvailableXP += 500;
    }

    public void AddGold()
    {
        PlayerProfile.Instance.currentGold += 500;
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


            // SceneCharacter.selectedPotionType = SceneCharacter.Reg
        }
    }

    public void GenerateMoreXpCosts()
    {
        //Calculate XP cost as double

        double calculatedNextXpCost = StatCalculator.Instance.xpCosts[StatCalculator.Instance.xpCosts.Count - 1];
        calculatedNextXpCost *= MagicNumbers.Instance.NextXpCostMultiplier;

        //Add to list as double
        StatCalculator.Instance.xpCosts.Add(calculatedNextXpCost);
    }

    public void GenerateMoreAbilityStats(double currentHighAbilityPower)
    {
        double nextAbilityPower = currentHighAbilityPower * MagicNumbers.Instance.statScalingFactor;
        if ((nextAbilityPower - currentHighAbilityPower) < 1)
        {
            nextAbilityPower += 1;
        }
        StatCalculator.Instance.currentHighAbilityPower = nextAbilityPower;
        StatCalculator.Instance.abilityPowerLevels.Add(nextAbilityPower);
    }

    public void GenerateMoreAtkStats(double currentHighAtkPower)
    {
        double nextAtkPower = currentHighAtkPower * MagicNumbers.Instance.statScalingFactor;
        if ((nextAtkPower - currentHighAtkPower) < 1)
        {
            nextAtkPower += 1;
        }
        StatCalculator.Instance.currentHighAtkPower = nextAtkPower;
        StatCalculator.Instance.atkPowerLevels.Add(nextAtkPower);
    }

    public void GenerateMoreHealthStats(double currentHighHealth)
    {
        double nextHealth = currentHighHealth * MagicNumbers.Instance.statScalingFactor;
        if ((nextHealth - currentHighHealth) < 1)
        {
            nextHealth += 1;
        }
        StatCalculator.Instance.currentHighHealth = nextHealth;
        StatCalculator.Instance.healthLevels.Add(nextHealth);
    }
}
