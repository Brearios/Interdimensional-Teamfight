using System.Collections.Generic;
using UnityEngine;


public class MagicNumbers : MonoBehaviour
{
    public static MagicNumbers Instance;
    public int skirmishScalingParticipantThreshold = 7;
    public int brawlScalingParticipantThreshold = 12;
    public int raidScalingParticipantThreshold = 20;
    public int epicBattleMaxScalingParticipantThreshold = 28;
    public float tankBonusThreatIterator = 0.25f; // Originally set to 0.25f
    public float tankHealthMultiplier = 3;
    public float targetCheckRandomRangeLowerBound;

    public int startingXpCost = 100;
    public int startingAtkPower = 5;
    public int startingAbilityPower = 8;
    public int startingHealth = 100;
    // 1.5x by level 10 is 1.0461, 2x is 1.0805, 2.5x is 1.1072, 5x is 1.1959, 10x is 1.2916, 20x is 1.395
    // Choosing 1.5x initially so that Xp cost growth outpaces power increase
    public double statScalingFactor = 1.0461;

    public int nextXpCost = 108;
    public int nextAtkPower = 6;
    public int nextAbilityPower = 9;
    public int nextHealth = 104;

    // This rate will double the number every ten levels
    public double NextXpCostMultiplier = 1.0805;

    public float[] newGamePlusEnemyHealthMultiplier = { 1f, 1.5f, 2.4f, 3.2f };
    // public float[] newGamePlusEnemyHealthMultiplier;
    // No rhyme or reason to these amounts
    public float[] newGamePlusEnemyAutoAtkDamageMultiplier = { 1f, 1.5f, 2.4f, 3.2f };
    // public float[] newGamePlusEnemyAutoAtkDamageMultiplier;
    public float[] newGamePlusEnemyAbilityPowerMultiplier = { 1f, 1.5f, 2.4f, 3.2f };
    // public float[] newGamePlusEnemyAbilityPowerMultiplier;
    //public float epicBattleSmallEnemy;
    //public float epicBattleNormalEnemy;
    //public float epicBattleLargeEnemy;
    //public float epicBattleBossEnemy;

    public string[] qualityNames;
    public string[] improvementNames;

    // Rebuild to have a base variable, and a level-up ratio
    public int[] goldUpgradeCosts = { 10, 30, 90, 270, 810, 2430, 7290, 21870, 65610, 196830, 590490, 1771470, 5314410, 15943230, 47829630, 143489070,
        430467210, 1291401630, }; // 3874204890, 11622614670, 34867844010, 104603532030, 313810596090, 941431788270 };

    public float[] gearMultiplierLevels = { 1.10f, 1.14f, 1.17f, 1.20f, 1.28f, 1.34f, 1.40f, 1.56f, 1.68f, 1.80f,
         2.12f, 2.36f, 2.60f, 3.24f, 3.72f, 4.20f, 5.48f, 6.44f }; // 7.40f, 9.96f, 11.88f, 13.80f, 18.92f, 22.76f, };

    // Assuming 9 armor ranks total - spread slots through number of levels
    public int[] enhancementSlotLevels = { 0, 0, 1, 1, 2, 2, 3, 3, 4 };

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

    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
