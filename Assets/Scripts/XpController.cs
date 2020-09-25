using System;
using System.Collections.Generic;
using UnityEngine;

// Add Atk, Ability, and Health calculators

public class XpController : MonoBehaviour
{
    public static XpController Instance;
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

    public List<int> atkLevel = new List<int>();
    public List<int> abilityLevel = new List<int>();
    public List<int> healthLevel = new List<int>();

    public double startingAtkIncrease;
    public double startingAbilityIncrease;
    public double startingHealthIncrease;

    //public int ability2UnlockCost = 300;
    //public int ability3UnlockCost = 500;
    //public int potionUnlockCost = 800;
    public CharacterProfile SceneCharacter;

    // Start is called before the first frame update

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        SceneCharacter.nextXpCost = (int)MagicNumbers.Instance.xpCosts[indexOfXpCosts];
        if (indexOfXpCosts == MagicNumbers.Instance.xpCosts.Count)
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
            SceneCharacter.healthListLevel++;

            if (SceneCharacter.healthListLevel == MagicNumbers.Instance.healthLevels.Count)
            {
                GenerateMoreHealthStats(MagicNumbers.Instance.healthLevels[MagicNumbers.Instance.abilityPowerLevels.Count], MagicNumbers.Instance.healthLevels[(MagicNumbers.Instance.healthLevels.Count - 1)]);
            }

            SceneCharacter.health = healthLevel[SceneCharacter.healthListLevel];
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
            SceneCharacter.atkListLevel++;
            // i++;

            if (SceneCharacter.atkListLevel == MagicNumbers.Instance.atkPowerLevels.Count)
            {
                GenerateMoreAtkStats(MagicNumbers.Instance.atkPowerLevels[MagicNumbers.Instance.atkPowerLevels.Count], MagicNumbers.Instance.abilityPowerLevels[(MagicNumbers.Instance.atkPowerLevels.Count - 1)]);
            }

            SceneCharacter.attackPower = atkLevel[SceneCharacter.atkListLevel];
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
            SceneCharacter.abilityListLevel++;
            // i++;

            if (SceneCharacter.abilityListLevel == MagicNumbers.Instance.abilityPowerLevels.Count)
                {
                    GenerateMoreAbilityStats(MagicNumbers.Instance.abilityPowerLevels[MagicNumbers.Instance.abilityPowerLevels.Count], MagicNumbers.Instance.abilityPowerLevels[(MagicNumbers.Instance.abilityPowerLevels.Count - 1)]);
                }

            // abilityIndex++;
            SceneCharacter.abilityPower = MagicNumbers.Instance.abilityPowerLevels[SceneCharacter.abilityListLevel];
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
            
            
            // SceneCharacter.selectedPotionType = SceneCharacter.Reg
        }
    }

    public void GenerateMoreXpCosts()
    {
        //Calculate XP cost as double
        for (int i = 0; i < 10; i++)
        {
            double calculatedNextXpCost = MagicNumbers.Instance.xpCosts[MagicNumbers.Instance.xpCosts.Count];
            calculatedNextXpCost *= MagicNumbers.Instance.NextXpCostMultiplier;

            //Add to list as double
            MagicNumbers.Instance.xpCosts.Add(calculatedNextXpCost);
        }
    }

    public void GenerateMoreAbilityStats(int highestAbilityPowerLevel, int previousAbilityPowerLevel)
    {
        int lastAbilityPowerIncrease = highestAbilityPowerLevel - previousAbilityPowerLevel;
        int nextAbilityPower;
        double nextAbilityPowerIncrease;

        for (int i = 0; i < 10; i++)
        {
            nextAbilityPowerIncrease = ((startingAbilityIncrease + (lastAbilityPowerIncrease * .1)) / 2);
            if (nextAbilityPowerIncrease < 1)
            {
                nextAbilityPowerIncrease = 1;
            }

            nextAbilityPower = Convert.ToInt32(previousAbilityPowerLevel + nextAbilityPowerIncrease);
            
            //Add to list as double
            MagicNumbers.Instance.abilityPowerLevels.Add(nextAbilityPower);
        }
    }

    public void GenerateMoreAtkStats(int highestAtkPowerLevel, int previousAtkPowerLevel)
    {
        int lastAtkPowerIncrease = highestAtkPowerLevel - previousAtkPowerLevel;
        int nextAtkPower;
        double nextAtkPowerIncrease;

        for (int i = 0; i < 10; i++)
        {
            nextAtkPowerIncrease = ((startingAtkIncrease + (lastAtkPowerIncrease * .1)) / 2);
            if (nextAtkPowerIncrease < 1)
            {
                nextAtkPowerIncrease = 1;
            }

            nextAtkPower = Convert.ToInt32(previousAtkPowerLevel + nextAtkPowerIncrease);

            //Add to list as double
            MagicNumbers.Instance.atkPowerLevels.Add(nextAtkPower);
        }
    }

    public void GenerateMoreHealthStats(int highestHealthLevel, int previousHealthLevel)
    {
        int lastHealthIncrease = highestHealthLevel - previousHealthLevel;
        int nextHealthAmount;
        double nextHealthIncrease;

        for (int i = 0; i < 10; i++)
        {
            nextHealthIncrease = ((startingHealthIncrease + (lastHealthIncrease * .1)) / 2);
            if (nextHealthIncrease < 1)
            {
                nextHealthIncrease = 1;
            }

            nextHealthAmount = Convert.ToInt32(previousHealthLevel + nextHealthIncrease);

            //Add to list as double
            MagicNumbers.Instance.abilityPowerLevels.Add(nextHealthAmount);
        }
    }
}
