using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenText : MonoBehaviour
{
    public static WinScreenText Instance;
    public string WinningTeamString;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Convert winningTeam to String
        WinningTeamString = GameManager.Instance.winningTeam.ToString();

        // Set text to "The Winner is TeamName
        text.text = $"{WinningTeamString} Wins!";

        // Set color to WinningTeam Color
        // text.color = GameManager.Instance.winningTeamColor;

        // Enable the Text
        if (GameManager.Instance.DeclareVictory)
        {
            text.enabled = true;
            text.color = GetColorForTeam(GameManager.Instance.winningTeam);
        }
        else
        {
            text.enabled = false;
        }
    }

    Color GetColorForTeam (Actor.Team team)
    {
        switch (team)
        {
            case Actor.Team.Blue:
                return Color.blue;
            case Actor.Team.Red:
                return Color.red;
            default:
                return Color.black;

        }
    }
}
