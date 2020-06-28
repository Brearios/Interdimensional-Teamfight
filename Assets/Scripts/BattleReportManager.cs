using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReportManager : MonoBehaviour
{
    public static BattleReportManager Instance;
    public string outcome;
    public string details;
    public string survivorsAndKills;
    public string currencyGains;
    public bool stringsBuilt;
    public bool colorSet;
    //public GameObject battleReportContainer;
    public Text outcomeText;
    public Text detailsText;
    public Text survivorsKillsText;
    public Text currencyGainsText;
    public GameObject outcomeBackground;
    public Image outcomeBackgroundImage;
    public Color outcomeBackgroundColor;


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

    // Start is called before the first frame update
    void Start()
    {
        stringsBuilt = false;
        //battleReportContainer = GameObject.Find("BattleReportContainer");
        outcomeText = GameObject.Find ("WinLoseText").GetComponent<Text>();
        detailsText = GameObject.Find("DetailsText").GetComponent<Text>();
        survivorsKillsText = GameObject.Find("SurvivorsKillsText").GetComponent<Text>();
        currencyGainsText = GameObject.Find("CurrencyGainsText").GetComponent<Text>();
        outcomeBackground = GameObject.Find("OutcomeBackground");
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameManager.Instance.playerLoss) || (GameManager.Instance.playerVictory))
        {
            if (!stringsBuilt)
            {
                // DisplayBattleReport();
                BuildStrings();
                stringsBuilt = true;
            }
            if (!colorSet)
            {
                SetColor();
                colorSet = true;
            }
        }
    }

    void BuildStrings()
    {
        OutcomeStringBuilder();
        DetailsStringBuilder();
        SurivorsAndKillsStringBuilder();
        CurrencyGainsStringBuilder();
    }

    void SetColor()
    {
        if (GameManager.Instance.playerLoss)
        {
            outcomeBackgroundColor = new Color32(210, 30, 30, 150);
        }
        if (GameManager.Instance.playerVictory)
        {
            outcomeBackgroundColor = new Color32(30, 30, 210, 150);
        }
        outcomeBackground.GetComponent<Image>().color = outcomeBackgroundColor;
    }

    void DisplayBattleReport()
    {
        // Updates Text Strings
        // Activates GameObject
    }

    void OutcomeStringBuilder()
    {
        if (GameManager.Instance.playerVictory)
        {
            outcome = "VICTORY!";
        }
        if (GameManager.Instance.playerLoss)
        {
            outcome = "DEFEAT!";
        }
        outcomeText.text = outcome;
    }

    void DetailsStringBuilder()
    {
        details = $"Damage Dealt: {GameManager.Instance.heroDamageDone} \n Damage Taken: {GameManager.Instance.heroDamageTaken} \n Healing Received: {GameManager.Instance.heroHealingDone}";
        detailsText.text = details;
    }

    void SurivorsAndKillsStringBuilder()
    {
        survivorsAndKills = $"Heroes Survived: {GameManager.Instance.survivingHeroes} of {GameManager.Instance.totalHeroes} \n Enemies Defeated: {GameManager.Instance.enemyDeaths} of {GameManager.Instance.totalEnemies}";
        survivorsKillsText.text = survivorsAndKills;
    }
    void CurrencyGainsStringBuilder()
    {
        currencyGains = $"XP per Hero: {GameManager.Instance.xpPerCharacter} \n Gold Gained: {GameManager.Instance.earnedBattleGold} \n Crystals Acquired: {GameManager.Instance.earnedBattleCrystals}";
        currencyGainsText.text = currencyGains;
    }
}