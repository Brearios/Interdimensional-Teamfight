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
