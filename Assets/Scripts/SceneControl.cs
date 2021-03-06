﻿using System.Collections;
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
        if (PlayerProfile.Instance.nextBattleScene >= LevelManager.Instance.levelList.Count)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
        // Old system
        // SceneManager.LoadScene(PlayerProfile.Instance.nextBattleScene);
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
            PlayerProfile.Instance.currentEditingInteger = (PlayerProfile.Instance.UnlockedHeroes.Count - 1);
            SceneManager.LoadScene(1);
        }

    }

    public void NextMenuCharacter()
    {
        // int charListLength = (PlayerProfile.Instance.UnlockedHeroes.Count - 1);
        if (PlayerProfile.Instance.currentEditingInteger < PlayerProfile.Instance.UnlockedHeroes.Count - 1)
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
        PlayerProfile.Instance.nextBattleScene = 0;
        //PlayerProfile.Instance.newGamePlusActive = true;
        //Debug.Log($"New Game Plus Active = {PlayerProfile.Instance.newGamePlusActive}");
        PlayerProfile.Instance.newGamePlusIterator += 1;
        Debug.Log($"New Game Plus level = {PlayerProfile.Instance.newGamePlusIterator}");
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

  
}
