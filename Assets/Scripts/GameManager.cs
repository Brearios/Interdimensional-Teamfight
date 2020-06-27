using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // <- reference link to GameManager
    public bool IsRunning = false; // Allows pausing the game
    public bool DisplayHealth = true; // Shows or hides health bars
    public bool ShowTarget = true;
    public int earnedBattleXP;
    public int earnedBattleCrystals;
    public int earnedBattleGold;
    public float gameSpeed;
    public float deltaTime;
    public float timeIncrement = .5f;
    public bool DeclareVictory;
    public ScriptableTeam winningTeam;
    public Color winningTeamColor;
    // public int playersReceivingXP;
    public int xpPerCharacter;
    public bool xpCounted;
    public bool xpDistributed;
    public int playerCharacterStartingCount;
    Actor[] allActors;
    public int activeScene;
    public bool currencyDistributed;
    public int heroDamageDone;
    public int heroDamageTaken;
    public int heroHealingDone;
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
        }
    }

    void Start()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        DeclareVictory = false;
        gameSpeed = 1.0f;
        timeIncrement = .2f;
        earnedBattleXP = 0;
        earnedBattleCrystals = 0;
        earnedBattleGold = 0;
        xpCounted = false;
        xpDistributed = false;
        playerCharacterStartingCount = 0;
        heroDamageDone = 0;
        heroDamageTaken = 0;
        heroHealingDone = 0;

        CountPlayers();
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

        TimeControls();

        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
        // Prevent IsBattleOver from erroring on menu/title scenes
        if (allActors.Length == 0)
        {
            return;
        }
        if (activeScene > 1)
        {
            IsBattleOver();
        }
        if (DeclareVictory == true)
        {
            /* if (xpCounted == false)
            {
                TallyXP();
                xpCounted = true;
            } */
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
            // Load Upgrade Screen?
            if (Input.GetKeyDown(KeyCode.M))
            {
                MenuScreen();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                NextBattle();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                HighestBattle();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RepeatBattle();
            }
        }
    }

    //private void LateUpdate()
    //{
    //    ManageLayers();
    //}

    void IsBattleOver()
    {
        // Identify Team of Actor
        // Look for Actors on !Team
        // If none found, WinningTeam = Actor.Team 
        // Prevent IsBattleOver from erroring on menu/title scenes
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();


        // Keep the script from running on menu screens
        if (allActors.Length == 0)
        {
            return;
        }

        // Get Team of Any One (Alive) Unit
        int arrayPosition = 0;
        while (allActors[arrayPosition].isDead)
        {
            arrayPosition += 1;
        }

        ScriptableTeam winCheckTeam = allActors[arrayPosition].team;

        // Compare Other Actors to that Team
        foreach (Actor Team in allActors)

            // The dead are not relevant to our evaluation
            //if (Team.isDead)
            //{
            //    continue;
            //}

            if (!Team.isDead && Team.team != winCheckTeam)
            {
                DeclareVictory = false;
                break;
            }
            else if (!Team.isDead && Team.team == winCheckTeam)
            {
                DeclareVictory = true;
                winningTeam = winCheckTeam;
            }
        if (DeclareVictory == true)
        {
            winningTeamColor = winCheckTeam.color;
            // winningTeamColor = allActors[0].GetComponentInChildren<Color>();
        }
        

    }

    void TallyXP() // Not used - XP is added from Actor script, if not player and not previously added
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

    void CountPlayers()
    {
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
        foreach (Actor Actor in allActors)
        {
            if (Actor.isPlayer == true)
            {
                playerCharacterStartingCount++;
            }
        }
    }

    void DivideExperience()
    {
        xpPerCharacter = (earnedBattleXP / playerCharacterStartingCount);
        // xpPerCharacter = ((xpPerCharacter / 3) + 1); // Clunky fix to this running 3 times for unknown reasons
    }

    void TimeControls()
    {
        deltaTime = (gameSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F))
        {
            gameSpeed *= 2;
            //gameSpeed += timeIncrement;
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

    // Press B to load the boss fight

    void RepeatBattle()
    {
        SceneManager.LoadScene(PlayerProfile.Instance.nextBattleScene);
    }

    //void ManageLayers()
    //{
    //    SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

    //    foreach(SpriteRenderer renderer in renderers)
    //    {
    //        renderer.sortingOrder = (int)(renderer.transform.position.y * -100);
    //    }
    //}
}
