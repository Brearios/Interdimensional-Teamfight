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
    public float[] newGamePlusEnemyHealthMultiplier = { 1.8f, 2.6f, 4.2f };
    // No rhyme or reason to these amounts
    public float[] newGamePlusEnemyAutoAtkDamageMultiplier = { 2f, 3.5f, 4.5f };
    public float[] newGamePlusEnemyAbilityPowerMultiplier = { 2f, 3.5f, 4.5f };
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
