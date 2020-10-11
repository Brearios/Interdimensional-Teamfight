using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // <- reference link to GameManager
    public GameObject battleReportCanvas;
    public bool IsRunning = false; // Allows pausing the game
    public bool DisplayHealth = true; // Shows or hides health bars
    public bool ShowTarget = true;
    public int earnedBattleXP;
    public int earnedBattleCrystals;
    public int earnedBattleGold;
    public float gameSpeed;
    public float deltaTime;
    public float timeIncrement;
    public bool startingCharactersSpawned;
    public bool ActorDiedTestIfBattleIsOver;
    public bool BattleOver;
    public bool BattleGainsCalculated;
    public ScriptableTeam winningTeam;
    public ScriptableTeam winCheckTeam;
    public Color winningTeamColor;
    // public int playersReceivingXP;
    public int xpPerCharacter;
    public bool xpDistributed;
    public int totalHeroes;
    public int totalEnemies;
    Actor[] allActors;
    public int activeScene;
    public bool currencyDistributed;
    public int heroDeaths;
    public int enemyDeaths;
    public float heroDamageDone;
    public float heroDamageTaken;
    public float heroHealingDone;
    public float enemyHealingDone;
    public bool playerVictory;
    public bool playerLoss;
    public bool playerRetreat;
    public string outcome;
    public int survivingHeroes;
    public int retreatCounter;
    // public int nextBattleScene = 0;
    // public int[] nextBattle = new int[5] { 2, 3, 4, 5, 6 };

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
            return;
        }
    }

    void Start()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        BattleOver = false;
        BattleGainsCalculated = false;
        gameSpeed = 1.0f;
        timeIncrement = .2f;
        earnedBattleXP = 0;
        earnedBattleCrystals = 0;
        earnedBattleGold = 0;
        xpDistributed = false;
        totalHeroes = 0;
        heroDamageDone = 0;
        heroDamageTaken = 0;
        heroHealingDone = 0;
        enemyHealingDone = 0;
        playerVictory = false;
        playerLoss = false;
        ActorDiedTestIfBattleIsOver = false;
        totalEnemies = 0;
        heroDeaths = 0;
        enemyDeaths = 0;
        survivingHeroes = 0;
        retreatCounter = 0;

        CountPlayersAndEnemies();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsRunning = !IsRunning;  // Toggles the pause Bool when space is pressed
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            DisplayHealth = !DisplayHealth; // Toggles health bars when V is pressed
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowTarget = !ShowTarget; // Toggles target lines when T is pressed
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            retreatCounter++;
            if (retreatCounter == 3)
            { 
                Retreat();
            }
        }

        TimeControls();

        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
        // Prevent IsBattleOver from erroring on menu/title scenes
        if (allActors.Length == 0)
        {
            return;
        }
        if (ActorDiedTestIfBattleIsOver)
        {
            IsBattleOver();
        }
        if (BattleOver == true)
        {
            ProcessRewards();
            PrepareCanvas();
        }
    }

    void Retreat()
    {

        playerRetreat = true;
        CountSurvivors();
        if (xpDistributed == false)
        {
            TallyXP();
            DivideExperience();
            if (xpPerCharacter > 0)
            {
                DistributeXP();
            }
            xpDistributed = true;
        }
        if (currencyDistributed == false)
        {
            DistributeCurrencies();
            currencyDistributed = true;
        }
        battleReportCanvas.SetActive(true);
    }

    void IsBattleOver()
    {
        // Identify Team of Actor
        // Look for Actors on !Team
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

        int arrayPosition = 0;
        while (allActors[arrayPosition].isDead)
        {
            arrayPosition += 1;
        }

        winCheckTeam = allActors[arrayPosition].team;

        // Compare Other Actors to that Team
        foreach (Actor Team in allActors)

            if (!Team.isDead && Team.team != winCheckTeam)
            {
                BattleOver = false;
                break;
            }
            else if (!Team.isDead && Team.team == winCheckTeam)
            {
                BattleOver = true;
                winningTeam = winCheckTeam;
            }
        ActorDiedTestIfBattleIsOver = false;
    }

    void TallyXP()
    {
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
        foreach (Actor Actor in allActors)
            if (Actor.isDead == true && !Actor.isPlayer)
            {
                earnedBattleXP += Actor.xpWhenKilled;
            }
    }

    void DistributeXP()
    {
        // Old code for single hero
        // CharacterProfile ActiveCharacters = GameObject.FindObjectOfType<CharacterProfile>();
        // ActiveCharacters.characterTotalXP += earnedBattleXP;
        // ActiveCharacters.characterAvailableXP += earnedBattleXP;

        Actor[] endActors = GameObject.FindObjectsOfType<Actor>();

        foreach (Actor Actor in endActors)
        {
            if (Actor.isPlayer)
            {
                CharacterProfile xpProfile = PlayerProfile.Instance.GetCharacterProfileForUnit(Actor.unit);
                xpProfile.characterTotalXP += xpPerCharacter;
                Debug.Log($"Adding {xpPerCharacter} to {xpProfile.heroName}'s total XP. They now have {xpProfile.characterTotalXP} total XP.");
                xpProfile.characterAvailableXP += xpPerCharacter;
                Debug.Log($"Adding {xpPerCharacter} to {xpProfile.heroName}'s available XP. They now have {xpProfile.characterAvailableXP} available XP.");
            }
        }
    }

    void DistributeCurrencies()
    {
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
        foreach (Actor Actor in allActors)
            if (Actor.isDead == true && !Actor.isPlayer)
            {
                earnedBattleCrystals += Actor.crystalsWhenKilled;
                earnedBattleGold += Actor.goldWhenKilled;
            }

        PlayerProfile.Instance.earnedCrystals += earnedBattleCrystals;
        PlayerProfile.Instance.currentCrystals += earnedBattleCrystals;
        PlayerProfile.Instance.totalGold += earnedBattleGold;
        PlayerProfile.Instance.currentGold += earnedBattleGold;
    }

    void CountPlayersAndEnemies()
    {
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
        foreach (Actor Actor in allActors)
        {
            if (Actor.isPlayer == true)
            {
                totalHeroes++;
            }
            if (!Actor.isPlayer)
            {
                totalEnemies++;
            }
        }
    }

    void DivideExperience()
    {
        xpPerCharacter = (earnedBattleXP / totalHeroes);
        // xpPerCharacter = ((xpPerCharacter / 3) + 1); // Clunky fix to this running 3 times for unknown reasons
    }

    void TimeControls()
    {
        deltaTime = (gameSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (gameSpeed > 0)
            {
                gameSpeed *= 2;
                //gameSpeed += timeIncrement;
            }
            else if (gameSpeed >= 0)
            {
                gameSpeed += 0.1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gameSpeed <= timeIncrement)
            {
                gameSpeed = 0f;
            }

            else
            {
                gameSpeed /= 2;
                //gameSpeed /= timeIncrement;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            gameSpeed = 1.0f;
        }
    }

    void IncrementNextBattleIfVictorious()
    {
        if (winningTeam.team == ScriptableTeam.Team.Blue) //&& (activeScene == PlayerProfile.Instance.nextBattleScene))
        {
            LevelManager.Instance.ProcessVictory();
            PlayerProfile.Instance.nextBattleScene++;
        }
    }
    void MenuScreen()
    {
        if (winningTeam.team == ScriptableTeam.Team.Blue && (activeScene == PlayerProfile.Instance.nextBattleScene))
        {
            PlayerProfile.Instance.nextBattleScene++;
        }
        SceneManager.LoadScene(0);
    }

    void NextBattle()
    {
        Debug.Log("You selected the next battle.");
        Debug.Log("Going from Battle " + activeScene + " to " + (activeScene + 1) + ".");
        if(activeScene == PlayerProfile.Instance.nextBattleScene)
        {
            PlayerProfile.Instance.nextBattleScene++;
            SceneManager.LoadScene(PlayerProfile.Instance.nextBattleScene);
        }
        else
        {
            SceneManager.LoadScene(activeScene + 1);
        }
        // Debug.Log("Next scene updated to " + PlayerProfile.Instance.nextBattleScene + ".");
        // Debug.Log("Loading scene number " + PlayerProfile.Instance.nextBattleScene + ".");
        
    }

    void HighestBattle()
    {
        Debug.Log("Going to the highest uncleared level.");
        if (activeScene == PlayerProfile.Instance.nextBattleScene)
        {
            PlayerProfile.Instance.nextBattleScene++;
        }
        SceneManager.LoadScene(PlayerProfile.Instance.nextBattleScene);
    }

    void RepeatBattle()
    {
        SceneManager.LoadScene(PlayerProfile.Instance.nextBattleScene);
    }

    void DetermineOutcome()
    {
        
        if (retreatCounter == 3)
        {
            playerRetreat = true;
            return;
        }
        // This is a holdover from having teams as shapes with colors. Blue = the player team.
        if (winningTeam.team == ScriptableTeam.Team.Blue)
        {
            playerVictory = true;
            // outcome = "VICTORY!";
        }
        if (winningTeam.team == ScriptableTeam.Team.Red)
        {
            playerLoss = true;
            // outcome = "DEFEAT!";
        }
    }

    void CountSurvivors()
    {
        survivingHeroes = totalHeroes - heroDeaths;
        // defeatedEnemies = totalEnemies - enemyDeaths; Unneeded
    }

    void ProcessRewards()
    {
        if (xpDistributed == false)
        {
            TallyXP();
            DivideExperience();
            if (xpPerCharacter > 0)
            {
                DistributeXP();
            }
            xpDistributed = true;
        }
        if (currencyDistributed == false)
        {
            DistributeCurrencies();
            currencyDistributed = true;
        }
    }

    void PrepareCanvas()
    {
        if (BattleOver == true)
        {
            winningTeamColor = winCheckTeam.color;
            DetermineOutcome();
            if (BattleGainsCalculated == false)
            {
                IncrementNextBattleIfVictorious();
            }
            CountSurvivors();
            battleReportCanvas.SetActive(true);
            BattleGainsCalculated = true;
        }
    }
}
