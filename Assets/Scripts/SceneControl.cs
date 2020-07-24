using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    public void StartBattles()
    {
        SceneManager.LoadScene(2);
    }

    public void TitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void SceneManagerNextBattle()
    {
        SceneManager.LoadScene(PlayerProfile.Instance.nextBattleScene);
    }

    //public void NextBattleScene()
    //{
    //    SceneManager.LoadScene(nextBattle[nextBattleScene]); // Load the next scene you haven't beaten
    //}

    //public void LastBattleScene()
    //{
    //    SceneManager.LoadScene(nextBattle[nextBattleScene - 1]); // Load the last scene you did beat to farm XP
    //}

    // Make this go to character selector menu/indexer
    /*
    public void CharScene()
    {
        SceneManager.LoadScene(2);
    }
    */

    public void MenuScene()
    {
        SceneManager.LoadScene(1);
    }

    //public void PriestScene()
    //{
    //    SceneManager.LoadScene(3);
    //}

    //public void WarriorScene()
    //{
    //    SceneManager.LoadScene(4);
    //}

    public void PrevMenuCharacter()
    {
        if (PlayerProfile.Instance.currentEditingInteger > 0)
        {
            PlayerProfile.Instance.currentEditingInteger--;
            SceneManager.LoadScene(1);
            Debug.Log("Currently Editing " + PlayerProfile.Instance.CurrentEditingCharacter);
        }
        else
        {
            PlayerProfile.Instance.currentEditingInteger = (PlayerProfile.Instance.characterProfiles.Count - 1);
            SceneManager.LoadScene(1);
        }

    }

    public void NextMenuCharacter()
    {
        int charListLength = (PlayerProfile.Instance.characterProfiles.Count - 1);
        if (PlayerProfile.Instance.currentEditingInteger < charListLength)
        {
            PlayerProfile.Instance.currentEditingInteger++;
            SceneManager.LoadScene(1);
            Debug.Log("Currently Editing " + PlayerProfile.Instance.CurrentEditingCharacter);
        }
        else
        {
            PlayerProfile.Instance.currentEditingInteger = 0;
            SceneManager.LoadScene(1);
        }
    }
    public void NewGamePlus()
    {
        PlayerProfile.Instance.nextBattleScene = 2;
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

  
}
