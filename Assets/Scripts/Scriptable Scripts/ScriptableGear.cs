﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gear Item", menuName = "Gear")]

public class ScriptableGear : MonoBehaviour
{
    public int currentLevel;
    public string itemName;
    public string[] rankNames;
    public ScriptableEnhancement currentEnhancement;

    // Some kind of swappable/moveable upgrade item
    // public PowerCore? currentPowerCore;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
