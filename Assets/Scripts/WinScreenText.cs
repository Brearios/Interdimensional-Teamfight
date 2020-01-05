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
        WinningTeamString = GameManager.Instance.winningTeam.name;

        // Set color to WinningTeam Color
        // text.color = GameManager.Instance.winningTeamColor;

        // Enable the Text
        if (GameManager.Instance.DeclareVictory)
        {
            // Set text to "The Winner is TeamName
            text.text = $"{WinningTeamString} Wins! Press M to return to menu.";
            text.color = GameManager.Instance.winningTeamColor;
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }

   /* Color GetColorForTeam (ScriptableTeam.Team team)
    {
        switch (team)
        {
            case ScriptableTeam.Team.Blue:
                return Color.blue;
            case ScriptableTeam.Team.Red:
                return Color.red;
            default:
                return Color.black;

        }
    }
    */
}
