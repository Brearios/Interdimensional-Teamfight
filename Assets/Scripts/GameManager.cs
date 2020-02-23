﻿using System.Collections;
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
    public float gameSpeed;
    public float deltaTime;
    public float timeIncrement = .5f;
    public bool DeclareVictory;
    public ScriptableTeam winningTeam;
    public Color winningTeamColor;
    public int winnersReceivingXP;
    public int xpPerCharacter;
    public bool xpCounted;
    public bool xpDistributed;
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
        gameSpeed = 1.0f;
        timeIncrement = .2f;
        earnedBattleXP = 0;
        xpCounted = false;
        xpDistributed = false;
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

        IsBattleOver();
        if (DeclareVictory == true)
        {
            /* if (xpCounted == false)
            {
                TallyXP();
                xpCounted = true;
            } */
            if (xpDistributed == false)
            {
                DistributeXP();
                xpDistributed = true;
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                RepeatBattle();
            }
        }
    }

    void IsBattleOver()
    {
        // Identify Team of Actor
        // Look for Actors on !Team
        // If none found, WinningTeam = Actor.Team 

        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
        // Prevent IsBattleOver from erroring on menu/title scenes
        if (allActors.Length == 0)
        {
            return;
        }

        // Get Team of Any One Unit
        ScriptableTeam winCheckTeam = allActors[0].team;

        // Compare Other Actors to that Team
        foreach (Actor Team in allActors)
            if (Team.team != winCheckTeam)
            {
                DeclareVictory = false;
                break;
            }
            else if (Team.team == winCheckTeam)
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
            if (Actor.isDead == true)
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
        
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
        foreach (Actor Actor in allActors)
            if (Actor.team == winningTeam)
            {
                winnersReceivingXP++;
            }
        xpPerCharacter = (earnedBattleXP / winnersReceivingXP);
        foreach (Actor Actor in allActors)
            if (Actor.isPlayer)
            {
                CharacterProfile xpProfile = PlayerProfile.Instance.GetCharacterProfileForUnit(Actor.unit);
                xpProfile.characterTotalXP += xpPerCharacter;
                xpProfile.characterAvailableXP += xpPerCharacter;
            }
    }

    void TimeControls()
    {
        deltaTime = (gameSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F))
        {
            gameSpeed += timeIncrement;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gameSpeed <= timeIncrement)
            {
                gameSpeed = 0f;
            }

            else
            {
                gameSpeed -= timeIncrement;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            gameSpeed = 1.0f;
        }
    }
    void MenuScreen()
    {
        PlayerProfile.Instance.nextBattleScene++;
        SceneManager.LoadScene(0);
    }

    void NextBattle()
    {
        Debug.Log("You selected the next battle.");
        Debug.Log("Going from Battle " + PlayerProfile.Instance.nextBattleScene + " to " + (PlayerProfile.Instance.nextBattleScene + 1) + ".");
        PlayerProfile.Instance.nextBattleScene++;
        Debug.Log("Next scene updated to " + PlayerProfile.Instance.nextBattleScene + ".");
        Debug.Log("Loading scene number " + PlayerProfile.Instance.nextBattleScene + ".");
        SceneManager.LoadScene(PlayerProfile.Instance.nextBattleScene);
    }

    // Press B to load the boss fight

    void RepeatBattle()
    {
        SceneManager.LoadScene(PlayerProfile.Instance.nextBattleScene);
    }
}
