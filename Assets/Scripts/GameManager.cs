using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // <- reference link to GameManager
    public bool IsRunning = false; // Allows pausing the game

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
