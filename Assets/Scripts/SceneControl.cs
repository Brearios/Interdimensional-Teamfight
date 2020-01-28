using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public int nextScene;

    private void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void AdvanceScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    // Make this go to character selector menu/indexer
    /*
    public void CharScene()
    {
        SceneManager.LoadScene(2);
    }
    */

    public void MageScene()
    {
        SceneManager.LoadScene(2);
    }

    public void PriestScene()
    {
        SceneManager.LoadScene(3);
    }

    public void WarriorScene()
    {
        SceneManager.LoadScene(4);
    }
}
