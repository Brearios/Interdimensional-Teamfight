using System.Collections.Generic;
using UnityEngine;


public class MagicNumbers : MonoBehaviour
{
    public static MagicNumbers Instance;
    public int skirmishScalingParticipantThreshold;
    public int brawlScalingParticipantThreshold;
    public int raidScalingParticipantThreshold;
    public int epicBattleMaxScalingParticipantThreshold;
    public float tankBonusThreatIterator; // Originally set to 0.25f
    public float tankHealthMultiplier;
    public float targetCheckRandomRangeLowerBound;

    public List<double> xpCosts = new List<double>();
    public List<int> atkPowerLevels = new List<int>();
    public List<int> abilityPowerLevels = new List<int>();
    public List<int> healthLevels = new List<int>();

    public int startingXpCost = 100;
    public int startingAtkPower = 5;
    public int startingAbilityPower = 8;
    public int startingHealth = 100;

    // This rate will double the number every ten levels
    public double NextXpCostMultiplier = 1.0805;

    // public float[] newGamePlusEnemyHealthMultiplier = { 1.8f, 2.6f, 4.2f };
    public float[] newGamePlusEnemyHealthMultiplier;
    // No rhyme or reason to these amounts
    // public float[] newGamePlusEnemyAutoAtkDamageMultiplier = { 2f, 3.5f, 4.5f };
    public float[] newGamePlusEnemyAutoAtkDamageMultiplier;
    // public float[] newGamePlusEnemyAbilityPowerMultiplier = { 2f, 3.5f, 4.5f };
    public float[] newGamePlusEnemyAbilityPowerMultiplier;
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

        xpCosts.Add(startingXpCost);
        atkPowerLevels.Add(startingAtkPower);
        abilityPowerLevels.Add(startingAbilityPower);
        healthLevels.Add(startingHealth);
        }
    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
