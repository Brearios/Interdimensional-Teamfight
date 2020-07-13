using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int currentLevel;
    // List of prefabs to instantiate - list for heroes and enemies?

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
    // Start is called before the first frame update
    void Start()
    {
        // Setting level to real number, accounting for Title (0) and Character Menu (1)
        currentLevel = PlayerProfile.Instance.nextBattleScene - 1; 
        foreach (CharacterProfile hero in PlayerProfile.Instance.characterProfiles)
        {
            if (hero.charUnlock)
            {
                SpawnHero(hero.heroName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnHero(string heroName)
    {
        // Instantiate()
    }

    void CharacterUnlock(int levelIndex)
    {
        
    }

    internal void ProcessVictory()
    {
        CharacterUnlock(currentLevel);
    }
}
