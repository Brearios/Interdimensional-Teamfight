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

    public void CharScene()
    {
        SceneManager.LoadScene(2);
    }
}
