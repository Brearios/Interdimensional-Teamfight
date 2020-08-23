using UnityEngine;

public class MagicNumbers : MonoBehaviour
{
    public static MagicNumbers Instance;
    public int skirmishScalingParticipantThreshold;
    public int brawlScalingParticipantThreshold;
    public int raidScalingParticipantThreshold;
    public int epicBattleMaxScalingParticipantThreshold;
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
