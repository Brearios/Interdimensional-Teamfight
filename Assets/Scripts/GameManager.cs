using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        gameSpeed = 1.0f;
        timeIncrement = 1f;
    }

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

        IsBattleOver();
    }

    void IsBattleOver()
    {
        // Identify Team of Actor
        // Look for Actors on !Team
        // If none found, WinningTeam = Actor.Team 

        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

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
        if (DeclareVictory)
        {
            winningTeamColor = winCheckTeam.color;
            // winningTeamColor = allActors[0].GetComponentInChildren<Color>();
            DistributeXP();
            // Load Upgrade Screen?
        }
        

    }

    void DistributeXP()
    {
        // Count heroes in scene
        // Divide XP by Heroes
        // Increase XP by their share
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
}
